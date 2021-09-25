using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.ExportImport;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
using static CSRPulse.Common.DataValidation;
using CSRPulse.Services.IServices;
using CSRPulse.Model;

namespace CSRPulse.Services
{
    public class BlockServices : BaseService, IBlockServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly IBlockImportRepository _blockImport;
        private readonly IExcelService _excelService;

        public BlockServices(IGenericRepository generic, IMapper mapper, IBlockImportRepository blockImport, IExcelService excelService)
        {
            _genericRepository = generic;
            _mapper = mapper;
            _excelService = excelService;
            _blockImport = blockImport;
        }
        public async Task<bool> CreateBlock(Model.Block block)
        {
            try
            {
                var dtoBlock = _mapper.Map<DTOModel.Block>(block);

                var res = await _genericRepository.InsertAsync(dtoBlock);
                if (res > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Model.Block>> GetBlockList(Model.Block block)
        {
            try
            {
                var getBlocks = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.Block>(x => (block.StateId.HasValue ? x.StateId == block.StateId : (1 > 0))
                && (block.DistrictId.HasValue ? x.DistrictId == block.DistrictId : (1 > 0)) && (block.IsActiveFilter.HasValue ? x.IsActive == block.IsActiveFilter.Value : 1 > 0)
                ).Include(s => s.State).Include(d => d.District));
                var disList = _mapper.Map<List<Model.Block>>(getBlocks);
                return disList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Model.Block> GetBlockById(int blockId)
        {
            try
            {
                var getBlocks = await _genericRepository.GetByIDAsync<DTOModel.Block>(blockId);
                if (getBlocks != null)
                    return _mapper.Map<Model.Block>(getBlocks);
                else
                    return new Model.Block();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateBlock(Model.Block block)
        {
            try
            {

                var getBlocks = await _genericRepository.GetByIDAsync<DTOModel.Block>(block.BlockId);
                if (getBlocks != null)
                {
                    if (getBlocks.BlockName == block.BlockName && getBlocks.BlockCode == block.BlockCode && getBlocks.StateId == block.StateId && getBlocks.DistrictId == block.DistrictId)
                        return true;

                    getBlocks.BlockName = block.BlockName;
                    getBlocks.BlockCode = block.BlockCode;
                    getBlocks.StateId = block.StateId.Value;
                    getBlocks.DistrictId = block.DistrictId.Value;
                    getBlocks.UpdatedBy = block.UpdatedBy;
                    getBlocks.UpdatedOn = block.UpdatedOn;                    
                    getBlocks.UpdatedRid = block.UpdatedRid;                    
                    getBlocks.UpdatedRname = block.UpdatedRname;                    
                    _genericRepository.Update(getBlocks);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RecordExist(Model.Block block)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.Block>(x => x.BlockName == block.BlockName || x.BlockCode == block.BlockCode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ActiveDeActive(int id, bool IsActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.Block>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Import Block

        public IEnumerable<dynamic> ReadBlockExcelData(string fileFullPath, bool isHeader, out string message, out int error, out int warning, out List<Model.BlockImport> importBlockSave, out List<string> missingHeaders, out List<string> columnName)
        {
            List<dynamic> objModel = new List<dynamic>();
            try
            {
                message = string.Empty;
                error = 0; warning = 0;
                bool isValidHeaderList;
                importBlockSave = new List<Model.BlockImport>();
                missingHeaders = new List<string>();
                columnName = new List<string>();
                DataTable dtExcelResult = new DataTable();
                Import ob = new Import();
                dtExcelResult = ob.ReadExcelWithNoHeader(fileFullPath);
                var headerList = new List<string>();

                string[] header = { "StateId", "DistrictId", "BlockCode", "BlockName" };

                headerList.AddRange(header);

                DataTable dtExcel = ReadAndValidateHeader(fileFullPath, headerList, isHeader, out isValidHeaderList, out missingHeaders);

                if (isValidHeaderList)
                {
                    DeleteTempFile(fileFullPath);
                    if (dtExcel.Rows.Count > 10000)
                    {
                        error = error + 1;
                        message = "MAXROW";
                        return objModel;
                    }
                    DataTable dt = dtExcel.Copy();
                    var colum = GetHeader(dt);

                    DataTable dataWithHeader = _excelService.GetDataWithHeader(dtExcel);
                    if (dataWithHeader.Rows.Count > 0)
                    {
                        DataTable dtStates = new DataTable();
                        DataTable dtDistrict = new DataTable();
                        DataTable dtBlock = new DataTable();

                        Model.BlockColValues colWithBlockValues = GetBlockValuesOfImportData(dataWithHeader);

                        if (colWithBlockValues.State != null && colWithBlockValues.State.Count() > 0)
                        {
                            var state = (from x in colWithBlockValues.State select new { State = x }).ToList();
                            dtStates = Common.ExtensionMethods.ToDataTable(state);
                        }
                        if (colWithBlockValues.District != null && colWithBlockValues.District.Count() > 0)
                        {
                            var district = (from x in colWithBlockValues.District select new { District = x }).ToList();
                            dtDistrict = Common.ExtensionMethods.ToDataTable(district);
                        }

                        if (colWithBlockValues.Block != null && colWithBlockValues.Block.Count() > 0)
                        {
                            var block = (from x in colWithBlockValues.Block select new { Block = x }).ToList();
                            dtBlock = Common.ExtensionMethods.ToDataTable(block);
                        }

                        DataSet dsImportFieldValueDB = new DataSet();
                        try
                        {
                            dsImportFieldValueDB = _blockImport.GetImportFieldValues(dtStates, dtDistrict, dtBlock);

                            MergeBlockSheetDataWithDBValues(dataWithHeader, dsImportFieldValueDB.Tables[0], dsImportFieldValueDB.Tables[1], dsImportFieldValueDB.Tables[2]);
                        }
                        catch (Exception)
                        {
                            dataWithHeader.Columns.Add("StateId", typeof(int));
                            dataWithHeader.Columns.Add("DictrictId", typeof(int));
                            dataWithHeader.Columns.Add("BlockCode", typeof(int));
                            dataWithHeader.Columns.Add("error", typeof(string));
                            dataWithHeader.Columns.Add("warning", typeof(string));
                            dataWithHeader.Columns.Add("rowCount", typeof(int));
                        }

                        ValidateBlockImportSheetData(dataWithHeader, out error, out warning);

                        columnName = colum.Columns.Cast<DataColumn>()
                              .Select(x => x.ColumnName).ToList();

                        columnName.Add("State");
                        columnName.Add("District");
                        columnName.Add("Block");
                        columnName.Add("error");
                        columnName.Add("warning");

                        for (int i = 0; i < dataWithHeader.Rows.Count; i++)
                        {
                            Model.BlockImport dI = new Model.BlockImport();
                            for (int j = 0; j < dataWithHeader.Columns.Count; j++)
                            {
                                try
                                {
                                    string sColName = dataWithHeader.Columns[j].ColumnName.ToString().Trim();

                                    if (sColName.Equals("StateId", StringComparison.OrdinalIgnoreCase))
                                        dI.StateId = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else if (sColName.Equals("DistrictId", StringComparison.OrdinalIgnoreCase))
                                        dI.DistrictId = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else if (sColName.Equals("BlockCode", StringComparison.OrdinalIgnoreCase))
                                        dI.BlockCode = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else if (sColName.Equals("BlockName", StringComparison.OrdinalIgnoreCase))
                                        dI.BlockName = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else
                                    {
                                        if (sColName != null && sColName != "rowCount")
                                            AddCellValue(dI, sColName, dataWithHeader.Rows[i][j].ToString().Trim());
                                    }

                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                /////==== end =======
                            }
                            var dynamic = RemoveProperty(dI);
                            importBlockSave.Add(dI);
                            objModel.Add(dynamic);
                        }
                        message = "success";
                        return objModel;
                    }
                }
                else
                {
                    error = error + 1;
                    message = "Headers";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objModel;
        }

        public DataTable GetHeader(DataTable output)
        {
            string SelectorBlankRowfilter = string.Empty;
            if (output.Rows.Count >= 1)
            {
                DataRow rowFirst = output.Rows[0];
                for (int icol = output.Columns.Count - 1; icol >= 0; icol--)
                {
                    DataColumn col = output.Columns[icol];
                    if (rowFirst[col].ToString().Trim() == "")
                        output.Columns.Remove(col);
                    else
                    {
                        if (output.Columns.Contains(rowFirst[col].ToString().Trim()))
                        {
                            output.Columns.Remove(rowFirst[col].ToString().Trim());
                            col.ColumnName = rowFirst[col].ToString().Trim();
                        }
                        else
                        {
                            col.ColumnName = rowFirst[col].ToString().Trim();
                            SelectorBlankRowfilter = SelectorBlankRowfilter == "" ? "([" + col.ColumnName + "]='' OR [" + col.ColumnName + "] IS NULL)" : SelectorBlankRowfilter + " AND ([" + col.ColumnName + "]='' OR [" + col.ColumnName + "] IS NULL)";
                        }
                    }
                }
                output.Rows.Remove(rowFirst);
                DataRow[] rowsBlankToDelete = output.Select(SelectorBlankRowfilter);
                if (rowsBlankToDelete.Length > 0)
                {
                    foreach (DataRow rowDelete in rowsBlankToDelete)
                        output.Rows.Remove(rowDelete);
                }
            }
            return output;
        }

        private DataTable ReadAndValidateHeader(string sFilePath, List<string> headerList, bool IsHeader, out bool isValid, out List<string> sMissingHeader)
        {

            isValid = false; sMissingHeader = new List<string>();
            DataTable dtExcel = new DataTable();
            List<string> invalidColumn = new List<string>();
            try
            {
                Import ob = new Import();
                using (dtExcel = ob.ReadExcelWithNoHeader(sFilePath))
                {
                    RemoveBlankRowAndColumns(ref dtExcel, true);
                    int iExcelRowCount = dtExcel.Rows.Count;
                    if (iExcelRowCount > 0)
                    {
                        List<string> strList = dtExcel.Rows[0].ItemArray.Select(o => o == null ? String.Empty : o.ToString()).ToList();
                        isValid = ValidateHeader(strList, headerList, out sMissingHeader, out invalidColumn);
                        return dtExcel;
                    }
                    else
                        return dtExcel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Model.BlockColValues GetBlockValuesOfImportData(DataTable dtImportData)
        {
            try
            {
                Model.BlockColValues colWithDistinctValues = new Model.BlockColValues();

                colWithDistinctValues.State = Common.ExtensionMethods.GetDistinctCellValues<string>(dtImportData, "StateId").Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                colWithDistinctValues.District = Common.ExtensionMethods.GetDistinctCellValues<string>(dtImportData, "DistrictId").Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                colWithDistinctValues.Block = Common.ExtensionMethods.GetDistinctCellValues<string>(dtImportData, "BlockCode").Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                return colWithDistinctValues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void MergeBlockSheetDataWithDBValues(DataTable excelData, DataTable dtState, DataTable dtDistrict, DataTable dtBlockCode)
        {
            try
            {
                excelData.Columns.Add("State", typeof(int));
                excelData.Columns.Add("District", typeof(int));
                excelData.Columns.Add("Block", typeof(int));
                excelData.Columns.Add("error", typeof(string));
                excelData.Columns.Add("warning", typeof(string));
                excelData.Columns.Add("rowCount", typeof(int));
                foreach (DataRow row in excelData.Rows)
                {
                    int stateId = int.Parse(row["StateId"].ToString());
                    DataRow[] arrState = dtState.AsEnumerable().Where(x => x.Field<int>("StateId") == stateId).ToArray();
                    if (arrState.Length > 0)
                    {
                        row["State"] = arrState[0]["StateId1"].ToString().Equals("") ? -1 : int.Parse(arrState[0]["StateId1"].ToString());
                    }
                    else
                        row["State"] = -1;

                    int districtId = int.Parse(row["DistrictId"].ToString());
                    DataRow[] arrDistrict = dtDistrict.AsEnumerable().Where(x => x.Field<int>("DistrictId") == districtId).ToArray();
                    if (arrDistrict.Length > 0)
                    {
                        row["District"] = arrDistrict[0]["DistrictId1"].ToString().Equals("") ? -1 : int.Parse(arrDistrict[0]["DistrictId1"].ToString());
                    }
                    else
                        row["District"] = -1;

                    string blockCode = row["BlockCode"].ToString();
                    DataRow[] arrBlock = dtBlockCode.AsEnumerable().Where(x => x.Field<string>("BlockCode").Equals(blockCode, StringComparison.OrdinalIgnoreCase)).ToArray();
                    if (arrBlock.Length > 0)
                    {
                        row["Block"] = arrBlock[0]["BlockCode1"].ToString().Equals("") ? int.Parse(arrBlock[0]["BlockCode"].ToString()) : -1;

                    }
                    else
                        row["Block"] = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ValidateBlockImportSheetData(DataTable dtSource, out int iError, out int iWarning)
        {
            iError = 0; iWarning = 0;
            try
            {
                iError = 0; iWarning = 0;
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    DataRow cDrow = dtSource.Rows[i];
                    string sError = string.Empty;
                    string sWarning = string.Empty;

                    for (int j = 0; j < dtSource.Columns.Count; j++)
                    {
                        string sColName = dtSource.Columns[j].ColumnName.ToString().Trim()/*.ToLower()*/;

                        if (sColName.ToLower().Equals("state") || sColName.ToLower().Equals("error") ||
                             sColName.ToLower().Equals("rowcount") || sColName.ToLower().Equals("warning") || sColName.ToLower().Equals("district") || sColName.ToLower().Equals("block"))
                        {
                            continue;
                        }
                        string sColVal = dtSource.Rows[i][j].ToString().Trim();

                        if (sColName.Equals("StateId"))
                        {
                            #region
                            if (IsBlank(sColVal))
                            {
                                sError = sError + "B|";
                                iError++;
                            }
                            else
                            {
                                int state = int.Parse(dtSource.Rows[i]["State"].ToString());
                                if (!(state > 0))
                                {
                                    sError = sError + "NF|";
                                    iError++;
                                }
                                else
                                    sError = sError + "|";
                            }
                            sWarning = sWarning + "|";
                            #endregion
                        }

                        else if (sColName.Equals("DistrictId"))
                        {
                            #region
                            if (IsBlank(sColVal))
                            {
                                sError = sError + "B|";
                                iError++;
                            }
                            else
                            {
                                int district = int.Parse(dtSource.Rows[i]["District"].ToString());
                                if (!(district > 0))
                                {
                                    sError = sError + "NF|";
                                    iError++;
                                }
                                else
                                    sError = sError + "|";
                            }
                            sWarning = sWarning + "|";
                            #endregion
                        }
                        else if (sColName.Equals("BlockCode", StringComparison.OrdinalIgnoreCase))
                        {
                            #region
                            if (IsBlank(sColVal))
                            {
                                sError = sError + "B|";
                                iError++;
                            }
                            else
                            {
                                if (!IsBlank(sColVal) && (sColVal.Length != 5))
                                {
                                    sError = sError + "MAXMIN5|";
                                    iError++;
                                }
                                else
                                {
                                    //int block = int.Parse(dtSource.Rows[i]["Block"].ToString());
                                    int block;
                                    if (int.TryParse(dtSource.Rows[i]["Block"].ToString(), out block))
                                    {
                                        if (!(block > 0))
                                        {
                                            sError = sError + "EX|";
                                            iError++;
                                        }
                                        else
                                            sError = sError + "|";
                                    }
                                    else
                                    {
                                        sError = sError + "IP|";
                                        iError++;
                                    }
                                }
                            }
                            sWarning = sWarning + "|";
                            #endregion
                        }
                        else if (sColName.Equals("BlockName"))
                        {
                            if (IsBlank(sColVal))
                            {
                                sError = sError + "B|";
                                iError++;
                            }
                            else
                            {
                                if (!IsBlank(sColVal) && (sColVal.Length < 2 || sColVal.Length > 50))
                                {
                                    sError = sError + "MAXLEN50|";
                                    iError++;
                                }
                                else
                                    sError = sError + "|";
                            }
                        }

                        else
                        {
                            sError = sError + "|";
                            sWarning = sWarning + "|";
                        }
                    }
                    dtSource.Rows[i]["error"] = sError;
                    dtSource.Rows[i]["warning"] = sWarning;
                    dtSource.Rows[i]["rowCount"] = i + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Model.BlockImport AddCellValue(Model.BlockImport input, string keyName, string cellValue)
        {
            try
            {
                var prop = input.GetType().GetProperty(keyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                prop.SetValue(input, cellValue, null);
                var returnClass = new ExpandoObject() as IDictionary<string, object>;
                returnClass.Add(keyName, cellValue);
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic RemoveProperty(Model.BlockImport model)
        {
            try
            {
                var returnClass = new ExpandoObject() as IDictionary<string, object>;
                var pprop = model.GetType().GetProperties();
                foreach (var pr in pprop)
                {
                    var val = pr.GetValue(model);
                    if (val is string && string.IsNullOrWhiteSpace(val.ToString()))
                    {
                        returnClass.Add(pr.Name, val);
                    }
                    else if (val == null)
                    {
                    }
                    else
                    {
                        returnClass.Add(pr.Name, val);
                    }
                }
                return returnClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ImportBlockData(List<Model.BlockImport> blockData, int createdBy, int createdRId, string createdRName)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("StateId", typeof(int));
                table.Columns.Add("DistrictId", typeof(int));
                table.Columns.Add("BlockCode", typeof(string));
                table.Columns.Add("BlockName", typeof(string));
                table.Columns.Add("CreatedBy", typeof(int));
                table.Columns.Add("CreatedRid", typeof(int));
                table.Columns.Add("CreatedRname", typeof(string));

                foreach (var item in blockData)
                {
                    var row = table.NewRow();
                    row["StateId"] = Convert.ToInt32(item.StateId);
                    row["DistrictId"] = Convert.ToString(item.DistrictId);
                    row["BlockCode"] = Convert.ToString(item.BlockCode);
                    row["BlockName"] = Convert.ToString(item.BlockName);
                    row["CreatedBy"] = createdBy;
                    row["CreatedRid"] = createdRId;
                    row["CreatedRname"] = createdRName;
                    table.Rows.Add(row);
                }
                bool res = _blockImport.ImportBlockData(table);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
