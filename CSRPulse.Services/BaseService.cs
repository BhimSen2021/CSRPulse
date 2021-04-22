using CSRPulse.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;
using CSRPulse.Model;
using CSRPulse.Data.Repositories;
using DTOModel = CSRPulse.Data.Models;
using System.Linq;
using AutoMapper;

namespace CSRPulse.Services
{
    public class BaseService
    {
        private readonly IMapper _mapper;
        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// This function is will refresh dbcontext class after database has been changed to switch new database
        /// </summary>
        /// <param name="_connection"></param>
        public static void SetConnectionString(ref string _connection)
        {
            CSRPulseDbContext.CustomeConnectionString = _connection;
            CSRPulseDbContext context = new CSRPulseDbContext();
        }
        /// <summary>
        /// 
        /// </summary>
        public string GenerateOrGetLatestCode(StartingNumber startingNumber)
        {
            try
            {
                IGenericRepository genericRepo = new GenericRepository();
                if (genericRepo.Exists<DTOModel.StartingNumber>(x => x.TableName == startingNumber.TableName))
                {
                    var getData = genericRepo.Get<DTOModel.StartingNumber>(x => x.TableName == startingNumber.TableName).FirstOrDefault();
                    if (getData != null)
                    {
                        return GenerateCode(getData);
                    }
                }
                else
                {
                    var dtoNumber = _mapper.Map<DTOModel.StartingNumber>(startingNumber);
                    genericRepo.Insert(dtoNumber);
                    return GenerateCode(dtoNumber);
                }
                return string.Empty;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GenerateCode(DTOModel.StartingNumber startingNumber)
        {
            string NewCode = string.Empty;
            if (startingNumber != null)
                NewCode = startingNumber.Prefix + '-' + startingNumber.Number;

            return NewCode;

        }
    }
}
