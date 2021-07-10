using AutoMapper;
using CSRPulse.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
using CSRPulse.ExportImport;
using static CSRPulse.Common.DataValidation;
using System.Dynamic;
using CSRPulse.Services.IServices;

namespace CSRPulse.Services
{
    public class DistrictServices : BaseService, IDistrictServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly IDistrictImportRepository districtImport;
        private readonly IExcelService excelService;

        public DistrictServices(IGenericRepository generic, IMapper mapper, IDistrictImportRepository districtImport, IExcelService excelService)
        {
            _genericRepository = generic;
            _mapper = mapper;
            this.districtImport = districtImport;
            this.excelService = excelService;
        }
        public async Task<bool> CreateDistrict(Model.District district)
        {
            try
            {
                var dtoDistrict = _mapper.Map<DTOModel.District>(district);

                var res = await _genericRepository.InsertAsync(dtoDistrict);
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

        public async Task<List<Model.District>> GetDistrictList(Model.District district)
        {
            try
            {
                var getDistricts = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.District>(x => (district.IsActiveFilter.HasValue ? x.IsActive == district.IsActiveFilter.Value : 1 > 0) && (district.StateId.HasValue ? x.StateId == district.StateId.Value : 1 > 0)).Include(s => s.State));

                var disList = _mapper.Map<List<Model.District>>(getDistricts);
                return disList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Model.District> GetDistrictById(int districtId)
        {
            try
            {
                var getDistricts = await _genericRepository.GetByIDAsync<DTOModel.District>(districtId);
                if (getDistricts != null)
                    return _mapper.Map<Model.District>(getDistricts);
                else
                    return new Model.District();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateDistrict(Model.District district)
        {
            try
            {

                var getDistricts = await _genericRepository.GetByIDAsync<DTOModel.District>(district.DistrictId);
                if (getDistricts != null)
                {
                    if (getDistricts.DistrictName == district.DistrictName && getDistricts.DistrictCode == district.DistrictCode && getDistricts.DistrictShort == district.DistrictShort && getDistricts.StateId == district.StateId)
                        return true;

                    getDistricts.DistrictName = district.DistrictName;
                    getDistricts.DistrictShort = district.DistrictShort;
                    getDistricts.DistrictCode = district.DistrictCode;
                    getDistricts.StateId = district.StateId.Value;
                    getDistricts.UpdatedBy = district.UpdatedBy;
                    getDistricts.UpdatedOn = district.UpdatedOn;
                    _genericRepository.Update(getDistricts);
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

        public async Task<bool> RecordExist(Model.District district)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.District>(x => x.DistrictName == district.DistrictName || x.DistrictCode == district.DistrictCode);
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
                var model = _genericRepository.GetByID<DTOModel.District>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Import District        

        public IEnumerable<dynamic> ReadDistrictExcelData(string fileFullPath, bool isHeader, out string message, out int error, out int warning, out List<Model.DistrictImport> importDistrictSave, out List<string> missingHeaders, out List<string> columnName)
        {
            List<dynamic> objModel = new List<dynamic>();
            try
            {
                message = string.Empty;
                error = 0; warning = 0;
                bool isValidHeaderList;
                importDistrictSave = new List<Model.DistrictImport>();
                missingHeaders = new List<string>();
                columnName = new List<string>();
                DataTable dtExcelResult = new DataTable();
                Import ob = new Import();
                dtExcelResult = ob.ReadExcelWithNoHeader(fileFullPath);
                var headerList = new List<string>();

                string[] header = { "StateId", "DistrictCode", "DistrictName", "DistrictShort" };

                headerList.AddRange(header);

                DataTable dtExcel = ReadAndValidateHeader(fileFullPath, headerList, isHeader, out isValidHeaderList, out missingHeaders);

                if (isValidHeaderList)
                {
                    DeleteTempFile(fileFullPath);
                    DataTable dt = dtExcel.Copy();
                    var colum = GetHeader(dt);

                    DataTable dataWithHeader = excelService.GetDataWithHeader(dtExcel);
                    if (dataWithHeader.Rows.Count > 0)
                    {
                        DataTable dtStates = new DataTable();
                        DataTable dtDistrict = new DataTable();

                        Model.DistrictColValues colWithDistinctValues = GetDistinctValuesOfImportData(dataWithHeader);

                        if (colWithDistinctValues.State != null && colWithDistinctValues.State.Count() > 0)
                        {
                            var state = (from x in colWithDistinctValues.State select new { State = x }).ToList();
                            dtStates = Common.ExtensionMethods.ToDataTable(state);
                        }
                        if (colWithDistinctValues.District != null && colWithDistinctValues.District.Count() > 0)
                        {
                            var district = (from x in colWithDistinctValues.District select new { District = x }).ToList();
                            dtDistrict = Common.ExtensionMethods.ToDataTable(district);
                        }
                        DataSet dsImportFieldValueDB = new DataSet();
                        try
                        {
                            dsImportFieldValueDB = districtImport.GetImportFieldValues(dtStates, dtDistrict);

                            MergeDistrictSheetDataWithDBValues(dataWithHeader, dsImportFieldValueDB.Tables[0], dsImportFieldValueDB.Tables[1]);
                        }
                        catch (Exception ex)
                        {
                            dataWithHeader.Columns.Add("StateId", typeof(int));
                            dataWithHeader.Columns.Add("DistrictCode", typeof(int));
                            dataWithHeader.Columns.Add("error", typeof(string));
                            dataWithHeader.Columns.Add("warning", typeof(string));
                            dataWithHeader.Columns.Add("rowCount", typeof(int));
                        }

                        ValidateDistrictImportSheetData(dataWithHeader, out error, out warning);

                        columnName = colum.Columns.Cast<DataColumn>()
                              .Select(x => x.ColumnName).ToList();

                        columnName.Add("State");
                        columnName.Add("District");
                        columnName.Add("error");
                        columnName.Add("warning");

                        for (int i = 0; i < dataWithHeader.Rows.Count; i++)
                        {
                            Model.DistrictImport dI = new Model.DistrictImport();
                            for (int j = 0; j < dataWithHeader.Columns.Count; j++)
                            {
                                try
                                {
                                    string sColName = dataWithHeader.Columns[j].ColumnName.ToString().Trim();

                                    if (sColName.Equals("StateId", StringComparison.OrdinalIgnoreCase))
                                        dI.StateId = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    if (sColName.Equals("DistrictCode", StringComparison.OrdinalIgnoreCase))
                                        dI.DistrictCode = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else if (sColName.Equals("DistrictName", StringComparison.OrdinalIgnoreCase))
                                        dI.DistrictName = dataWithHeader.Rows[i][sColName].ToString().Trim();
                                    else if (sColName.Equals("DistrictShort", StringComparison.OrdinalIgnoreCase))
                                        dI.DistrictShort = dataWithHeader.Rows[i][sColName].ToString().Trim();
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
                            importDistrictSave.Add(dI);
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

        private Model.DistrictColValues GetDistinctValuesOfImportData(DataTable dtImportData)
        {
            try
            {
                Model.DistrictColValues colWithDistinctValues = new Model.DistrictColValues();

                colWithDistinctValues.State = Common.ExtensionMethods.GetDistinctCellValues<string>(dtImportData, "StateId").Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                colWithDistinctValues.District = Common.ExtensionMethods.GetDistinctCellValues<string>(dtImportData, "DistrictCode").Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                return colWithDistinctValues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void MergeDistrictSheetDataWithDBValues(DataTable excelData, DataTable dtState, DataTable dtDistrictCode)
        {
            try
            {
                excelData.Columns.Add("State", typeof(int));
                excelData.Columns.Add("District", typeof(int));
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

                    string districtCode = row["DistrictCode"].ToString();
                    DataRow[] arrDistrict = dtDistrictCode.AsEnumerable().Where(x => x.Field<string>("DistrictCode").Equals(districtCode, StringComparison.OrdinalIgnoreCase)).ToArray();
                    if (arrDistrict.Length > 0)
                    {
                        row["District"] = arrDistrict[0]["DistrictCode1"].ToString().Equals("") ? int.Parse(arrDistrict[0]["DistrictCode"].ToString()) : -1;

                    }
                    else
                        row["District"] = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ValidateDistrictImportSheetData(DataTable dtSource, out int iError, out int iWarning)
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

                        if (sColName.ToLower().Equals("State") || sColName.ToLower().Equals("error") ||
                             sColName.ToLower().Equals("rowcount") || sColName.ToLower().Equals("warning") || sColName.ToLower().Equals("District"))
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
                        else if (sColName.Equals("DistrictCode", StringComparison.OrdinalIgnoreCase))
                        {
                            #region
                            if (IsBlank(sColVal))
                            {
                                sError = sError + "B|";
                                iError++;
                            }
                            else
                            {
                                if (!IsBlank(sColVal) && (sColVal.Length < 3 || sColVal.Length > 3))
                                {
                                    sError = sError + "MAXMIN3|";
                                    iError++;
                                }
                                else
                                {
                                    int district = int.Parse(dtSource.Rows[i]["District"].ToString());
                                    if (!(district > 0))
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
                        else if (sColName.Equals("DistrictName"))
                        {
                            if (IsBlank(sColVal))
                            {
                                sError = sError + "B|";
                                iError++;
                            }
                            else
                            {
                                if (!IsBlank(sColVal) && (sColVal.Length < 3 || sColVal.Length > 200))
                                {
                                    sError = sError + "MAXMIN200|";
                                    iError++;
                                }
                                else
                                    sError = sError + "|";
                            }
                        }
                        else if (sColName.Equals("DistrictShort"))
                        {
                            if (!IsBlank(sColVal) && (sColVal.Length < 3 || sColVal.Length > 4))
                            {
                                sError = sError + "MAXMIN4";
                                iError++;
                            }
                            else
                            {
                                if (sColVal.Length > 4)
                                {
                                    sError = sError + "MAXMIN4";
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

        public Model.DistrictImport AddCellValue(Model.DistrictImport input, string keyName, string cellValue)
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

        public dynamic RemoveProperty(Model.DistrictImport model)
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
        #endregion

        public bool ImportDistrictData(List<Model.DistrictImport> districtData)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("StateId", typeof(int));
                table.Columns.Add("DistrictCode", typeof(string));
                table.Columns.Add("DistrictName", typeof(string));
                table.Columns.Add("DistrictShort", typeof(string));

                foreach (var item in districtData)
                {
                    var row = table.NewRow();
                    row["StateId"] = Convert.ToInt32(item.StateId);
                    row["DistrictCode"] = Convert.ToString(item.DistrictCode);
                    row["DistrictName"] = Convert.ToString(item.DistrictName);
                    row["DistrictShort"] = Convert.ToString(item.DistrictShort);
                    table.Rows.Add(row);
                }
                bool res = districtImport.ImportDistrictData(table);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}