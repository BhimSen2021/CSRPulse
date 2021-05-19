using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Services.IServices;
using CSRPulse.Data.Repositories;
using AutoMapper;
using DTOModel = CSRPulse.Data.Models;
using System.Transactions;
using System.Linq;

namespace CSRPulse.Services
{

    public class RegistrationService : BaseService, IRegistrationService
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPrepareDBForCustomer _dBForCustomer;
        private readonly IGenericRepository _genericRepository;

        private const string _dbPath = @"wwwroot/DB/DefaultDbScript.sql";
        public RegistrationService(IGenericRepository generic, IMapper mapper, IEmailService emailService, IPrepareDBForCustomer dBForCustomer)
        {
            _genericRepository = generic;
            _mapper = mapper;
            _emailService = emailService;
            _dBForCustomer = dBForCustomer;
        }
        public async Task<bool> CustomerExists(Model.Customer customer)
        {
            try
            {
                if (await _genericRepository.ExistsAsync<DTOModel.Customer>(x => x.Email == customer.Email || x.Telephone == customer.Telephone))
                {
                    customer.RecordExist = true;
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Task<int> CustomerRegistrationAsync(Model.Customer customer, out string customerCode)
        {
            using (var transaction = _genericRepository.BeginTransaction())
            {
                try
                {
                    customerCode = string.Empty;
                    if (_genericRepository.Exists<DTOModel.Customer>(x => x.Email == customer.Email || x.Telephone == customer.Telephone))
                    {
                        customer.RecordExist = true;
                        return Task.FromResult(0);
                    }

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

                    dtoCustomer.CustomerCode = GenerateOrGetLatestCode(startingNum, _genericRepository);
                    _genericRepository.Insert(dtoCustomer);
                    transaction.Commit();
                    customerCode = dtoCustomer.CustomerCode;
                    return Task.FromResult(dtoCustomer.CustomerId);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> CustomerPaymentAsync(Model.Customer customer)
        {

            string outputRes = string.Empty;

            using (var transaction = _genericRepository.BeginTransaction())
            {
                try
                {
                    var dbName = "CSRPulse_" + customer.CustomerCode;
                    dbName = dbName.Replace('-', '_');
                    var dtoCustPayment = _mapper.Map<DTOModel.CustomerPayment>(customer.CustomerPayment);
                    await _genericRepository.InsertAsync(dtoCustPayment);
                    customer.CustomerLicense.PaymentId = dtoCustPayment.PaymentId;
                    var dtoCustLicence = _mapper.Map<DTOModel.CustomerLicenseActivation>(customer.CustomerLicense);
                    await _genericRepository.InsertAsync(dtoCustLicence);

                    transaction.Commit();
                    DTOModel.Customer dtoCustomer = new DTOModel.Customer()
                    {
                        CustomerCode = customer.CustomerCode,
                        CustomerName = customer.CustomerName,
                        Email = customer.Email,
                        Telephone = customer.Telephone,
                        DataBaseName = dbName
                    };
                    await _dBForCustomer.CreateBD(dtoCustomer, _dbPath, "Password", out outputRes);

                    var getCustDetail = _genericRepository.Get<DTOModel.Customer>(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
                    if (getCustDetail != null)
                    {
                        getCustDetail.DataBaseName = dbName;
                        _genericRepository.Update(getCustDetail);
                    }
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }


        }

        public string GenerateOTP()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + numbers + small_alphabets;

            int length = 6;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;

        }

        public bool SendOTP(string email, string OTP)
        {
            bool flag = false;
            try
            {
                StringBuilder emailBody = new StringBuilder("");
                Common.EmailMessage message = new Common.EmailMessage();
                message.To = email;
                var mailSubj = _genericRepository.Get<DTOModel.MailSubject>(x => x.MailProcessId == 2).FirstOrDefault();
                if (mailSubj != null)
                {
                    message.Subject = mailSubj.Subject;
                    message.SubjectId = mailSubj.SubjectId;
                }
                else
                    message.Subject = "Default Subject";

                message.PlaceHolders = new List<KeyValuePair<string, string>>();
                message.TemplateName = "TestEmail";
                message.PlaceHolders.Add(
                    new KeyValuePair<string, string>("{$otp}", OTP)
                    );
                _emailService.CustomerRegistrationMail(message);
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }

            return flag;
        }


    }
}
