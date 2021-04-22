using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Services.IServices;
using CSRPulse.Data.Repositories;
using AutoMapper;
using DTOModel = CSRPulse.Data.Models;
namespace CSRPulse.Services
{
    public class RegistrationService : BaseService, IRegistrationService
    {
        private readonly IGenericRepository _genRepo;
        private readonly IMapper _mapper;
        public RegistrationService(IGenericRepository generic, IMapper mapper) : base(mapper)
        {
            _genRepo = generic;
            _mapper = mapper;
        }
        public async Task<string> CustomerRegistrationAsync(Model.Customer customer)
        {
            var dtoCustomer = _mapper.Map<DTOModel.Customer>(customer);
            Model.StartingNumber startingNum = new Model.StartingNumber
            {
                TableName = "customer",
                ColumnName = "CustomerCode",
                Prefix = "CUST-",
                Number = 1,
                NumberWidth = 7,
                CreatedBy = 1,
                CreatedOn = DateTimeOffset.UtcNow.Date
            };

            dtoCustomer.CustomerCode = GenerateOrGetLatestCode(startingNum);
            await _genRepo.InsertAsync(dtoCustomer);
            return dtoCustomer.CustomerCode;
        }
    }
}
