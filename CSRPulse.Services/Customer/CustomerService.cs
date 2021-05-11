using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSRPulse.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public CustomerService(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;

        }
        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            try
            {
                var customerList = new List<Customer>();
                var result = await _genericRepository.GetAsync<DTOModel.Customer>();

                foreach (var c in result)
                {
                    customerList.Add(new Customer
                    {
                        CustomerId = c.CustomerId,
                        CustomerCode = c.CustomerCode,
                        CustomerName = c.CustomerName,
                        Email = c.Email,
                        Website = c.Website,
                        DataBaseName = c.DataBaseName,
                        CreatedOn = c.CreatedOn
                    });
                }

                return customerList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Customer> GetCustomerDetailsAsync(int customerId)
        {
            try
            {
                var customer = new Customer();
                var result = await _genericRepository.GetIQueryable<DTOModel.Customer>(c => c.CustomerId == customerId).Include(a => a.CustomerLicenseActivation).Include(p => p.CustomerPayment).FirstOrDefaultAsync();

                customer = _mapper.Map<Customer>(result);
                return customer;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Task<Customer> GetCustomerByIdAsync()
        {
            throw new NotImplementedException();
        }


    }
}
