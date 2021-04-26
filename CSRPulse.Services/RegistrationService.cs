using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Services.IServices;
using CSRPulse.Data.Repositories;
using AutoMapper;
using DTOModel = CSRPulse.Data.Models;
using System.Transactions;

namespace CSRPulse.Services
{
    public class RegistrationService : BaseService, IRegistrationService
    {
        // private readonly IGenericRepository _genRepo;
        private readonly IMapper _mapper;
        public RegistrationService(IGenericRepository _generic, IMapper mapper) : base(mapper, _generic)
        {
            //_genRepo = generic;
            _mapper = mapper;
        }
        public async Task<int> CustomerRegistrationAsync(Model.Customer customer)
        {
            try
            {
                var dtoCustomer = _mapper.Map<DTOModel.Customer>(customer);
                Model.StartingNumber startingNum = new Model.StartingNumber
                {
                    TableName = "customer",
                    ColumnName = "CustomerCode",
                    Prefix = "CUST",
                    Number = 1,
                    NumberWidth = 7,
                    CreatedBy = 1,
                    CreatedOn = DateTimeOffset.UtcNow.Date
                };

                dtoCustomer.CustomerCode = GenerateOrGetLatestCode(startingNum);
                 await _genRepo.InsertAsync(dtoCustomer);

                _genRepo.SaveChanges();
                return dtoCustomer.CustomerID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> CustomerPaymentAsync(Model.Customer customer)
        {
            try
            {

                var dtoCustPayment = _mapper.Map<DTOModel.CustomerPayment>(customer.CustomerPayment);
                await _genRepo.InsertAsync(dtoCustPayment);

                var dtoCustLicence = _mapper.Map<DTOModel.CustomerLicenseActivation>(customer.CustomerLicense);
                await _genRepo.InsertAsync(dtoCustLicence);
                _genRepo.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
