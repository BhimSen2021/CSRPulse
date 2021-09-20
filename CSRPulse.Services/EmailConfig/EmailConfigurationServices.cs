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
using CSRPulse.Model;

namespace CSRPulse.Services
{
    public class EmailConfigurationServices : BaseService, IEmailConfigurationServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public EmailConfigurationServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<EmailConfiguration> GetEmailConfigAsync()
        {
            try
            {
                var result = new EmailConfiguration();
                var emailConfigurations = await _genericRepository.GetAsync<DTOModel.EmailConfiguration>();
                var rs = emailConfigurations.FirstOrDefault();
                result.EmailConfigurationID = rs.EmailConfigurationId;              
                result.UserName = rs.UserName;
                result.Password = rs.Password;
                result.Port = rs.Port;
                result.Server = rs.Server;               
                result.Sslstatus = rs.Sslstatus;
                return result;
                //return stateList;
                //return _mapper.Map<EmailConfiguration>(emailConfigurations);
                //return EmailConfigurationList;
            }
            catch (Exception)
            {
                throw;
            }
            
        }



        public async Task<bool> UpdateEmailConfig(EmailConfiguration emailConfiguration)
        {
            try
            {

                var modelEmail = await _genericRepository.GetByIDAsync<DTOModel.EmailConfiguration>(emailConfiguration.EmailConfigurationID);
                if (modelEmail != null)
                {                    
                    modelEmail.UserName = emailConfiguration.UserName;
                    modelEmail.Password = emailConfiguration.Password;
                    modelEmail.Server = emailConfiguration.Server;
                    modelEmail.Port = emailConfiguration.Port;                   
                    modelEmail.Sslstatus = emailConfiguration.Sslstatus;
                    modelEmail.UpdatedBy = emailConfiguration.UpdatedBy;
                    modelEmail.UpdatedOn = emailConfiguration.UpdatedOn;
                    //modelEmail.Bcc = emailConfiguration.Bcc;
                    _genericRepository.Update(modelEmail);
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
    }
}
