using CSRPulse.Model;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class PartnerController : BaseController<PartnerController>
    {
        private readonly IPartnerService _partnerService;
        private readonly IDropdownBindService _ddlService;
        public PartnerController(IPartnerService partnerService, IDropdownBindService dropdownBindService)
        {
            _partnerService = partnerService;
            _ddlService = dropdownBindService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("PartnerController/Index");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public async Task<PartialViewResult> GetPartnerList(Partner partner)
        {
            _logger.LogInformation("PartnerController/GetPartnerList");
            try
            {
                var result = await _partnerService.GetPartnerAsync(partner);
                return PartialView("_PartnerList", result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            BindDropdowns();
            return View(new Partner());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Partner partner)
        {
            try
            {
                _logger.LogInformation("PartnerController/Create");
                if (ModelState.IsValid)
                {
                    if (await _partnerService.RecordExist(partner))
                    {
                        ModelState.AddModelError("", "Partner already exists");
                    }
                    else
                    {
                        partner.IsActive = true;
                        partner.CreatedBy = userDetail.UserID;
                        var result = await _partnerService.CreatePartner(partner);

                        if (result)
                        {
                            TempData["Message"] = "Partner Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                BindDropdowns();
                return View(partner);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int partnerId)
        {
            try
            {
                BindDropdowns();
                var partner = await _partnerService.GetPartnerById(partnerId);
                if (partner.PartnerType == (int)Common.PartnerType.NGO)
                {
                    BindFYYear();
                    return View("NGOTypeEdit", partner);
                }
                else
                    return View(partner);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Partner partner)
        {
            try
            {
                _logger.LogInformation("PartnerController/Edit");
                if (ModelState.IsValid)
                {
                    var result = await _partnerService.UpdatePartner(partner);
                    if (result)
                    {
                        TempData["Message"] = "Partner Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Partner Updation Failed.";
                    }

                }
                return View(partner);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("Partner/ActiveDeActive");
            var result = _partnerService.ActiveDeActive(id, isChecked);
            return Json(result);
        }

        [NonAction]
        void BindDropdowns()
        {
            var stateList = _ddlService.GetStateAsync(null, null);
            ViewBag.ddlState = new SelectList(stateList, "id", "value");
        }

        [NonAction]
        void BindFYYear()
        {
            var fyYears = _ddlService.GetFYYear(startYear:2018);
            ViewBag.fyYears = new SelectList(fyYears, "id", "value");
        }

        private List<NgoawardDetail> MakeEmpityRow(List<NgoawardDetail> model)
        {
            if (model == null)
                model = new List<NgoawardDetail>();

            var newItem = new NgoawardDetail();
            model.Add(newItem);
            return model;
        }

        #region List of Awards/Recognitions given to the Organization
        public async Task<IActionResult> SaveAwardDetails(Partner partner, string ButtonType)
        {
            try
            {
                int flag = 0;
                if (ButtonType == "SaveAwardDetails")
                {
                    var listAD = partner.NgoawardDetail.Where(s => s.Award != null && s.AwardConferrer != null && s.YearOfReceiving > 0).ToList();

                    RevoveModelState(ModelState);
                    if (TryValidateModel(partner.NgosaturatoryAuditorDetail))
                    {
                        var CreatedOn = DateTime.Now;
                        listAD.ToList().ForEach(h =>
                        {
                            h.PartnerId = partner.PartnerId;
                            h.NgoawardDetailId = 0;
                            h.CreatedOn = CreatedOn;
                            h.CreatedBy = userDetail.UserID;
                        });

                        partner.NgoawardDetail = listAD;
                        var ngoawardDetails = await _partnerService.GetUpdateNGOAwardDetails(partner);
                        partner.NgoawardDetail = ngoawardDetails;
                        flag = 1;
                    }
                }
                else if (ButtonType == "AddAwardDetails")
                {
                    var award = new NgoawardDetail() { PartnerId = partner.PartnerId };
                    if (partner.NgoawardDetail == null)
                        partner.NgoawardDetail = new List<NgoawardDetail>();

                    partner.NgoawardDetail.Add(award);
                    flag = 2;
                }
                return Json(new { flag = flag, htmlData = ConvertViewToString("_NGOAwardDetails", partner, true) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        #endregion

        #region Details of the Statutory Auditor
        public async Task<IActionResult> SaveSaturatoryAuditorDetails(Partner partner, string ButtonType)
        {
            try
            {
                int flag = 0;
                if (ButtonType == "SaveSaturatoryAuditorDetails")
                {
                    var listAD = partner.NgosaturatoryAuditorDetail.Where(s => s.AuditingFirm != null && s.Association != null).ToList();

                    RevoveModelState(ModelState);
                    if (TryValidateModel(partner.NgosaturatoryAuditorDetail))
                    {
                        var CreatedOn = DateTime.Now;
                        listAD.ToList().ForEach(h =>
                        {
                            h.PartnerId = partner.PartnerId;
                            h.NgosaturatoryAuditorDetailId = 0;
                            h.CreatedOn = CreatedOn;
                            h.CreatedBy = userDetail.UserID;
                        });

                        partner.NgosaturatoryAuditorDetail = listAD;
                        var Details = await _partnerService.GetUpdateSaturatoryAuditorDetails(partner);
                        partner.NgosaturatoryAuditorDetail = Details;
                        flag = 1;
                    }
                }
                else if (ButtonType == "AddSaturatoryAuditorDetails")
                {
                    var model = new NgosaturatoryAuditorDetail() { PartnerId = partner.PartnerId };
                    if (partner.NgosaturatoryAuditorDetail == null)
                        partner.NgosaturatoryAuditorDetail = new List<NgosaturatoryAuditorDetail>();

                    partner.NgosaturatoryAuditorDetail.Add(model);
                    flag = 2;
                }

                return Json(new { flag = flag, htmlData = ConvertViewToString("_NGOSaturatoryAuditorDetail", partner, true) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }

        #endregion

        #region Details of current key projects of the Organization
        public async Task<IActionResult> SaveNGOKeyProjects(Partner partner, string ButtonType)
        {
            try
            {
                int flag = 0;
                if (ButtonType == "SaveNGOKeyProjects")
                {
                    var listAD = partner.NGOKeyProjects.Where(s => s.DonorAgency != null && s.ProjectLocation != null).ToList();

                    RevoveModelState(ModelState);
                    if (TryValidateModel(partner.NGOKeyProjects))
                    {
                        var CreatedOn = DateTime.Now;
                        listAD.ToList().ForEach(h =>
                        {
                            h.PartnerId = partner.PartnerId;
                            h.NgokeyProjectId = 0;
                            h.CreatedOn = CreatedOn;
                            h.CreatedBy = userDetail.UserID;
                        });

                        partner.NGOKeyProjects = listAD;
                        var Details = await _partnerService.GetUpdateNGOKeyProjects(partner);
                        partner.NGOKeyProjects = Details;
                        flag = 1;
                    }
                }
                else if (ButtonType == "AddNGOKeyProjects")
                {
                    var model = new NGOKeyProjects() { PartnerId = partner.PartnerId };
                    if (partner.NGOKeyProjects == null)
                        partner.NGOKeyProjects = new List<NGOKeyProjects>();

                    partner.NGOKeyProjects.Add(model);
                    flag = 2;
                }

                return Json(new { flag = flag, htmlData = ConvertViewToString("_NGOKeyProjects", partner, true) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }
        #endregion


        #region Corpus fund and Grant Inflow
        public async Task<IActionResult> SaveCorpusFund(Partner partner, string ButtonType)
        {
            try
            {
                int flag = 0;
                if (ButtonType == "SaveCorpusFund")
                {
                    var listAD = partner.NgocorpusGrantFund.Where(s => s.FundsAmount != null).ToList();

                    RevoveModelState(ModelState);
                    if (TryValidateModel(partner.NgocorpusGrantFund))
                    {
                        var CreatedOn = DateTime.Now;
                        listAD.ToList().ForEach(h =>
                        {
                            h.PartnerId = partner.PartnerId;
                            h.NgocorpusGrantFundId = 0;
                            h.FundType = (int)Common.CorpusGrantFund.CorpusFund;
                            h.CreatedOn = CreatedOn;
                            h.CreatedBy = userDetail.UserID;
                        });

                        partner.NgocorpusGrantFund = listAD;
                        var Details = await _partnerService.GetUpdateNGOCorpusGrantFund(partner, (int)Common.CorpusGrantFund.CorpusFund);
                        partner.NgocorpusGrantFund = Details;
                        flag = 1;
                    }
                }
                else if (ButtonType == "AddCorpusFund")
                {
                   
                    var model = new NGOCorpusGrantFund() { PartnerId = partner.PartnerId, FundType = (int)Common.CorpusGrantFund.CorpusFund };
                    if (partner.NgocorpusGrantFund == null)
                        partner.NgocorpusGrantFund = new List<NGOCorpusGrantFund>();

                    partner.NgocorpusGrantFund.Add(model);
                    flag = 2;
                }

                BindFYYear();
                return Json(new { flag = flag, htmlData = ConvertViewToString("_NGOCorpusFund", partner, true) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }

        public async Task<IActionResult> SaveGrantInflow(Partner partner, string ButtonType)
        {
            try
            {
                int flag = 0;
                if (ButtonType == "SaveGrantInflow")
                {
                    var listAD = partner.NgocorpusGrantFund.Where(s => s.FundsAmount != null).ToList();

                    RevoveModelState(ModelState);
                    if (TryValidateModel(partner.NgocorpusGrantFund))
                    {
                        var CreatedOn = DateTime.Now;
                        listAD.ToList().ForEach(h =>
                        {
                            h.PartnerId = partner.PartnerId;
                            h.NgocorpusGrantFundId = 0;
                            h.FundType = (int)Common.CorpusGrantFund.GrantInflow;
                            h.CreatedOn = CreatedOn;
                            h.CreatedBy = userDetail.UserID;
                        });

                        partner.NgocorpusGrantFund = listAD;
                        var Details = await _partnerService.GetUpdateNGOCorpusGrantFund(partner, (int)Common.CorpusGrantFund.GrantInflow);
                        partner.NgocorpusGrantFund = Details;
                        flag = 1;
                    }
                }
                else if (ButtonType == "AddGrantInflow")
                {
                    
                    var model = new NGOCorpusGrantFund() { PartnerId = partner.PartnerId, FundType = (int)Common.CorpusGrantFund.GrantInflow };
                    if (partner.NgocorpusGrantFund == null)
                        partner.NgocorpusGrantFund = new List<NGOCorpusGrantFund>();

                    partner.NgocorpusGrantFund.Add(model);
                    flag = 2;
                }

                BindFYYear();
                return Json(new { flag = flag, htmlData = ConvertViewToString("_NGOGrantInflow", partner, true) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }
        #endregion
        private void RevoveModelState(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            modelState.Remove("PartnerName");
            modelState.Remove("PartnerType");
            modelState.Remove("Email");
            modelState.Remove("RegAddress");
            modelState.Remove("RegCity");
            modelState.Remove("RegPin");
            modelState.Remove("RegState");
            modelState.Remove("CommAddress");
            modelState.Remove("ComCity");
            modelState.Remove("ComPin");
            modelState.Remove("ComState");
        }

    }
}
