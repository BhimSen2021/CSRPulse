using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.ExportImport;
using CSRPulse.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
using static CSRPulse.Common.DataValidation;
using System.Dynamic;

namespace CSRPulse.Services
{
    public class VillageServices : BaseService, IVillageServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly IVillageImportRepository _villageImport;
        private readonly IExcelService _excelService;
        public VillageServices(IGenericRepository generic, IMapper mapper, IVillageImportRepository villageImport, IExcelService excelService)
        {
            _genericRepository = generic;
            _mapper = mapper;
            _villageImport = villageImport;
            _excelService = excelService;
        }
        public async Task<bool> CreateVillage(Model.Village village)
        {
            try
            {
                var dtoVillage = _mapper.Map<DTOModel.Village>(village);

                var res = await _genericRepository.InsertAsync(dtoVillage);
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

        public async Task<List<Model.Village>> GetVillageList(Model.Village village)
        {
            try
            {
                var getVillages = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.Village>(x => x.StateId == village.StateId && x.DistrictId == village.DistrictId && x.BlockId == village.BlockId && (village.IsActiveFilter.HasValue ? x.IsActive == village.IsActiveFilter.Value : 1 > 0)).Include(s => s.State).Include(d => d.District).Include(b => b.Block));
                var disList = _mapper.Map<List<Model.Village>>(getVillages);
                return disList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Model.Village> GetVillageById(int villageId)
        {
            try
            {
                var getVillages = await _genericRepository.GetByIDAsync<DTOModel.Village>(villageId);
                if (getVillages != null)
                    return _mapper.Map<Model.Village>(getVillages);
                else
                    return new Model.Village();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateVillage(Model.Village village)
        {
            try
            {

                var getVillages = await _genericRepository.GetByIDAsync<DTOModel.Village>(village.VillageId);
                if (getVillages != null)
                {
                    if (getVillages.VillageName == village.VillageName && getVillages.VillageCode == village.VillageCode && getVillages.StateId == village.StateId && getVillages.DistrictId == village.DistrictId && getVillages.BlockId == village.BlockId)
                        return true;

                    getVillages.VillageName = village.VillageName;
                    getVillages.VillageCode = village.VillageCode;
                    getVillages.StateId = village.StateId;
                    getVillages.DistrictId = village.DistrictId;
                    getVillages.BlockId = village.BlockId;
                    getVillages.LocationType = (int)village.LocationType;
                    getVillages.UpdatedBy = village.UpdatedBy;
                    getVillages.UpdatedOn = village.UpdatedOn;
                    _genericRepository.Update(getVillages);
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

        public async Task<bool> RecordExist(Model.Village village)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.Village>(x => x.VillageName == village.VillageName || x.VillageCode == village.VillageCode);
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
                var model = _genericRepository.GetByID<DTOModel.Village>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<dynamic> ReadVillageExcelData(string fileFullPath, bool isHeader, out string message, out int error, out int warning, out List<Model.VillageImport> importVillageSave, out List<string> missingHeaders, out List<string> columnName)
        {
            List<dynamic> objModel = new List<dynamic>();
            try
            {
                message = string.Empty;
                error = 0; warning = 0;
                bool isValidHeaderList;
                importVillageSave = new List<Model.VillageImport>();
                missingHeaders = new List<string>();
                columnName = new List<string>();
                DataTable dtExcelResult = new DataTable();
                Import ob = new Import();
                dtExcelResult = ob.ReadExcelWithNoHeader(fileFullPath);
                var headerList = new List<string>();

                string[] header = { "StateId", "DistrictId", "BlockId", "VillageCode", "VillageName" };

                headerList.AddRange(header);

                DataTable dtExcel = ReadAndValidateHeader(fileFullPath, headerList, isHeader, out isValidHeaderList, out missingHeaders);

                if (isValidHeaderList)
                {
                    DeleteTempFile(fileFullPath);

                    if (dtExcel.Rows.Count>10000)
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
                        DataTable dtVillage = new DataTable();

                        Model.VillageColValues colWithVillageValues = GetVillageValuesOfImportData(dataWithHeader);

                        if (colWithVillageValues.State != null && colWithVillageValues.State.Count() > 0)
                        {
                            var state = (from x in colWithVillageValues.State select new { State = x }).ToList();
                            dtStates = Common.ExtensionMethods.ToDataTable(state);
                        }
                        if (colWithVillageValues.District != null && colWithVillageValues.District.Count() > 0)
                        {
                            var district = (from x in colWithVillageValues.District select new { District = x }).ToList();
                            dtDistrict = Common.ExtensionMethods.ToDataTable(district);
                        }

                        if (colWithVillageValues.Block != null && colWithVillageValues.Block.Count() > 0)
                        {
                            var block = (from x in colWithVillageValues.Block select new { Block = x }).ToList();
                            dtBlock = Common.ExtensionMethods.ToDataTable(block);
                        }

                        if (colWithVillageValues.Village != null && colWithVillageValues.Village.Count() > 0)
                        {
                            var village = (from x in colWithVillageValues.Village select new { Village = x }).ToList();
                            dtVillage = Common.ExtensionMethods.ToDataTable(village);
                        }
                        DataSet dsImportFieldValueDB = new DataSet();
                        try
                        {
                            dsImportFieldValueDB = _villageImport.GetImportFieldValues(dtStates, dtDistrict, dtBlock, dtVillage);

                            MergeVillageSheetDataWithDBValues(dataWithHeader, dsImportFieldValueDB.Tables[0], dsImportFieldValueDB.Tables[1], dsImportFieldValueDB.Tables[2], dsImportFieldValueDB.Tables[3]);
                        }
                        catch (Exception)
                        {
                            dataWithHeader.Columns.Add("StateId", typeof(int));
                            dataWithHeader.Columns.Add("DictrictId", typeof(int));
                            dataWithHeader.Columns.Add("BlockId", typeof(int));
                            dataWithHeader.Columns.Add("VillageCode", typeof(int));
                            dataWithHeader.Columns.Add("error", typeof(string));
                            dataWithHeader.Columns.Add("warning", typeof(string));
                            dataWithHeader.Columns.Add("rowCount", typeof(int));
                        }

                        ValidateVillageImportSheetData(dataWithHeader, out error, out warning);

                        columnName = colum.Columns.Cast<DataColumn>()
                              .Select(x => x.ColumnName).ToList();

                        columnName.Add("State");
                        columnName.Add("District");
                        columnName.Add("Block");
                        columnName.Add("Village");
                        columnName.Add("error");
                        columnName.Add("warning");

                        for (int i = 0; i < dataWithHeader.Rows.Count; i++)
                        {
                            Model.VillageImport dI = new Model.VillageImport();
                            for (int j = 0; j < dataWithHeader.Columns.Count; j++)
                            {
                                try
                                {
                                    string sColName = dataWithHeader.Columns[j].ColumnName.ToString().Trim();

                                    if (sColName.Equals("StateId", StringComparison.OrdinalIgnoreCase))
                                        dI.StateId = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else if (sColName.Equals("DistrictId", StringComparison.OrdinalIgnoreCase))
                                        dI.DistrictId = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else if (sColName.Equals("BlockId", StringComparison.OrdinalIgnoreCase))
                                        dI.BlockId = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else if (sColName.Equals("VillageCode", StringComparison.OrdinalIgnoreCase))
                                        dI.VillageCode = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else if (sColName.Equals("VillageName", StringComparison.OrdinalIgnoreCase))
                                        dI.VillageName = dataWithHeader.Rows[i][sColName].ToString().Trim();
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
                            importVillageSave.Add(dI);
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
        private Model.VillageColValues GetVillageValuesOfImportData(DataTable dtImportData)
        {
            try
            {
                Model.VillageColValues colWithDistinctValues = new Model.VillageColValues();

                colWithDistinctValues.State = Common.ExtensionMethods.GetDistinctCellValues<string>(dtImportData, "StateId").Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                colWithDistinctValues.District = Common.ExtensionMethods.GetDistinctCellValues<string>(dtImportData, "DistrictId").Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                colWithDistinctValues.Block = Common.ExtensionMethods.GetDistinctCellValues<string>(dtImportData, "BlockId").Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                colWithDistinctValues.Village = Common.ExtensionMethods.GetDistinctCellValues<string>(dtImportData, "VillageCode").Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                return colWithDistinctValues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void MergeVillageSheetDataWithDBValues(DataTable excelData, DataTable dtState, DataTable dtDistrict, DataTable dtBlock, DataTable dtVillageCode)
        {
            try
            {
                excelData.Columns.Add("State", typeof(int));
                excelData.Columns.Add("District", typeof(int));
                excelData.Columns.Add("Block", typeof(int));
                excelData.Columns.Add("Village", typeof(int));
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

                    int blockId = int.Parse(row["BlockId"].ToString());
                    DataRow[] arrBlock = dtBlock.AsEnumerable().Where(x => x.Field<int>("BlockId") == blockId).ToArray();
                    if (arrBlock.Length > 0)
                    {
                        row["Block"] = arrBlock[0]["BlockId1"].ToString().Equals("") ? -1 : int.Parse(arrBlock[0]["BlockId1"].ToString());
                    }
                    else
                        row["Block"] = -1;


                    string villageCode = row["VillageCode"].ToString();
                    DataRow[] arrVillage = dtVillageCode.AsEnumerable().Where(x => x.Field<string>("VillageCode").Equals(villageCode, StringComparison.OrdinalIgnoreCase)).ToArray();
                    if (arrVillage.Length > 0)
                    {
                        row["Village"] = arrVillage[0]["VillageCode1"].ToString().Equals("") ? int.Parse(arrVillage[0]["VillageCode"].ToString()) : -1;

                    }
                    else
                        row["Village"] = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ValidateVillageImportSheetData(DataTable dtSource, out int iError, out int iWarning)
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
                             sColName.ToLower().Equals("rowcount") || sColName.ToLower().Equals("warning") || sColName.ToLower().Equals("district") || sColName.ToLower().Equals("block") || sColName.ToLower().Equals("village"))
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
                        else if (sColName.Equals("BlockId"))
                        {
                            #region
                            if (IsBlank(sColVal))
                            {
                                sError = sError + "B|";
                                iError++;
                            }
                            else
                            {
                                int block = int.Parse(dtSource.Rows[i]["Block"].ToString());
                                if (!(block > 0))
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
                        else if (sColName.Equals("VillageCode", StringComparison.OrdinalIgnoreCase))
                        {
                            #region
                            if (IsBlank(sColVal))
                            {
                                sError = sError + "B|";
                                iError++;
                            }
                            else
                            {
                                if (!IsBlank(sColVal) && (sColVal.Length != 6))
                                {
                                    sError = sError + "MAXMIN6|";
                                    iError++;
                                }
                                else
                                {
                                    int village = int.Parse(dtSource.Rows[i]["Village"].ToString());
                                    if (!(village > 0))
                                    {
                                        sError = sError + "EX|";
                                        iError++;
                                    }
                                    else
                                        sError = sError + "|";
                                }
                            }
                            sWarning = sWarning + "|";
                            #endregion
                        }
                        else if (sColName.Equals("VillageName"))
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

        public Model.VillageImport AddCellValue(Model.VillageImport input, string keyName, string cellValue)
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

        public dynamic RemoveProperty(Model.VillageImport model)
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
        public bool ImportVillageData(List<Model.VillageImport> villageData)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("StateId", typeof(int));
                table.Columns.Add("DistrictId", typeof(int));
                table.Columns.Add("BlockId", typeof(int));
                table.Columns.Add("VillageCode", typeof(string));
                table.Columns.Add("VillageName", typeof(string));


                foreach (var item in villageData)
                {
                    var row = table.NewRow();
                    row["StateId"] = Convert.ToInt32(item.StateId);
                    row["DistrictId"] = Convert.ToInt32(item.DistrictId);
                    row["BlockId"] = Convert.ToInt32(item.BlockId);
                    row["VillageCode"] = Convert.ToString(item.VillageCode);
                    row["VillageName"] = Convert.ToString(item.VillageName);
                    table.Rows.Add(row);
                }
                bool res = _villageImport.ImportVillageData(table);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
