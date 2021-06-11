using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using DTOModel = CSRPulse.Data.Models;
using System.Threading.Tasks;
using System.Linq;

namespace CSRPulse.Services
{
    public class MaintenanceService : BaseService, IMaintenanceService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IEmailService _emailService;

        public MaintenanceService(IMapper mapper, IGenericRepository genericRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _emailService = emailService;
        }

        public Maintenance GetMaintenanceDetails()
        {
            Maintenance maintenance = new Maintenance();
            try
            {
                var result = _genericRepository.Get<DTOModel.Maintenance>().FirstOrDefault();

                maintenance = _mapper.Map<Maintenance>(result);
                return maintenance;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> GoUnderMaintenance(Maintenance maintenance)
        {
            try
            {
                var mData = _genericRepository.Get<DTOModel.Maintenance>().FirstOrDefault();
                if (mData != null)
                {
                    mData.IsMaintenance = maintenance.IsMaintenance;
                    mData.StartDateTime = maintenance.StartDateTime;
                    mData.EndDateTime = maintenance.EndDateTime;
                    mData.Message = maintenance.Message;
                    _genericRepository.Update(mData);
                    return true;
                }
                else
                {
                    var model = _mapper.Map<DTOModel.Maintenance>(maintenance);
                    await _genericRepository.InsertAsync(model);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsUnderMaintenance()
        {
            var mData = _genericRepository.Get<DTOModel.Maintenance>().FirstOrDefault();
            if (mData != null)
            {

                return mData.IsMaintenance;
            }
            return false;
        }

        public bool SendEmail(string Message)
        {
            bool flag = false;
            try
            {
                StringBuilder emailBody = new StringBuilder("");
                Common.EmailMessage message = new Common.EmailMessage();
                string to = string.Empty;
                string cc = string.Empty;
                var uData = _genericRepository.Get<DTOModel.User>(u => u.IsActive == true && u.IsDeleted == false).ToList();
                var cData = _genericRepository.Get<DTOModel.Customer>(u => u.IsDeleted == false).ToList();
                if (uData != null)
                {
                    foreach (var item in uData)
                    {
                        to += item.EmailId + ";";
                    }
                }
                if (cData != null)
                {
                    foreach (var item in cData)
                    {
                        cc += item.Email + ";";
                    }
                }
                message.To = to.TrimEnd(';');
                message.CC = cc.TrimEnd(';');

                var mailSubj = _genericRepository.Get<DTOModel.MailSubject>(x => x.MailProcessId == 4).FirstOrDefault();
                if (mailSubj != null)
                {
                    message.Subject = mailSubj.Subject;
                    message.SubjectId = mailSubj.SubjectId;
                }
                else
                    message.Subject = "CSRPulse Mail";

                message.PlaceHolders = new List<KeyValuePair<string, string>>();
                message.TemplateName = "Maintenance";
                message.PlaceHolders.Add(new KeyValuePair<string, string>("{$message}", Message));
                _emailService.CustomerRelatedMails(message);
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }

            return flag;
        }

        public void UpdateMaintenance(bool IsMaintenance)
        {
            try
            {
                var mData = _genericRepository.Get<DTOModel.Maintenance>().FirstOrDefault();
                if (mData != null)
                {
                    mData.IsMaintenance = IsMaintenance;
                    _genericRepository.Update(mData);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
