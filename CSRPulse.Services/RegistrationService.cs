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
            Guid guid = Guid.NewGuid();
            var uniqId = guid.ToString().Substring(0, 8).ToUpper();
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
                        //Prefix = customer.CustomerName.Substring(0, 4).ToUpper(),
                        Prefix = "CUST",
                        Number = 1,
                        NumberWidth = 7,
                        CreatedBy = 1,
                        CreatedOn = DateTimeOffset.UtcNow.Date
                    };

                    dtoCustomer.CustomerCode = GenerateOrGetLatestCode(startingNum, _genericRepository);
                    dtoCustomer.CustomerUniqueId = uniqId;
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
            var getCustomer = _genericRepository.GetIQueryable<DTOModel.Customer>(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
            if (getCustomer != null)
            {
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
                        getCustomer.DataBaseName = dbName;
                        await _dBForCustomer.CreateBD(getCustomer, _dbPath, "Password", out outputRes);
                        _genericRepository.Update(getCustomer);
                        await SendRegistrationMail(getCustomer, "Password");
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return false;
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

        public bool SendOTP(string email, string OTP, string companyName)
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
                    message.Subject = "CSRPulse Mail";

                message.PlaceHolders = new List<KeyValuePair<string, string>>();
                message.TemplateName = "CustomerOTP";
                message.PlaceHolders.Add(new KeyValuePair<string, string>("{$otp}", OTP));
                message.PlaceHolders.Add(new KeyValuePair<string, string>("{$company}", companyName));
                _emailService.CustomerRelatedMails(message);
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }

            return flag;
        }

        Task<bool> SendRegistrationMail(DTOModel.Customer customer, string password)
        {
            bool flag = false;
            try
            {
                StringBuilder emailBody = new StringBuilder("");
                Common.EmailMessage message = new Common.EmailMessage();
                message.To = customer.Email;
                message.CustomerId = customer.CustomerId;
                var mailSubj = _genericRepository.Get<DTOModel.MailSubject>(x => x.MailProcessId == 2).FirstOrDefault();
                if (mailSubj != null)
                {
                    message.Subject = mailSubj.Subject;
                    message.SubjectId = mailSubj.SubjectId;
                }
                else
                    message.Subject = "CSRPulse Mail";

                message.PlaceHolders = new List<KeyValuePair<string, string>>();
                message.TemplateName = "CustomerRegs";
                message.PlaceHolders.Add(new KeyValuePair<string, string>("{$company}", customer.CustomerName));
                message.PlaceHolders.Add(new KeyValuePair<string, string>("{$accesskey}", customer.CustomerUniqueId));
                message.PlaceHolders.Add(new KeyValuePair<string, string>("{$emailId}", customer.Email));
                message.PlaceHolders.Add(new KeyValuePair<string, string>("{$user}", customer.CustomerCode));
                message.PlaceHolders.Add(new KeyValuePair<string, string>("{$password}", password));
                _emailService.CustomerRelatedMails(message);
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }

            return Task.FromResult(flag);
        }

    }
}
