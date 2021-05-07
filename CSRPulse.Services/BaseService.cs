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
        //internal readonly IMapper _mapper;
        //internal readonly IGenericRepository _genericRepository = null;
        //public BaseService(IMapper mapper, IGenericRepository genericRepository)
        //{
        //    _mapper = mapper;
        //    _genericRepository = genericRepository;
        //}
   

        /// <summary>
        /// This function is will refresh dbcontext class after database has been changed to switch new database
        /// </summary>
        /// <param name="_connection"></param>
        public static void SetConnectionString(ref string _connection)
        {
            CSRPulseDbContext.CustomeConnectionString = _connection;
            CSRPulseDbContext context = new CSRPulseDbContext();
            //_ = new CSRPulseDbContext();
        }
        /// <summary>
        ///  Generate Custom Code based on table 
        /// </summary>
        public string GenerateOrGetLatestCode(StartingNumber startingNumber, IGenericRepository _genericRepository)
        {
            try
            {
                //  IGenericRepository _genericRepository = new GenericRepository();
              
                if (_genericRepository.Exists<DTOModel.StartingNumber>(x => x.TableName == startingNumber.TableName))
                {
                    var getData = _genericRepository.Get<DTOModel.StartingNumber>(x => x.TableName == startingNumber.TableName).FirstOrDefault();
                    if (getData != null)
                    {
                        getData.Number++;
                        _genericRepository.Update(getData);

                        return GenerateCode(getData);
                    }
                }
                else
                {

                    //var dtoNumber = _mapper.Map<DTOModel.StartingNumber>(startingNumber);
                    var dtoNumber = new DTOModel.StartingNumber
                    {
                        ColumnName = startingNumber.ColumnName,
                        CreatedBy = startingNumber.CreatedBy,
                        IsDeleted = startingNumber.IsDeleted,
                        Number = startingNumber.Number,
                        NumberWidth = startingNumber.NumberWidth,
                        Prefix = startingNumber.Prefix,
                        StartNumberId = startingNumber.StartNumberID,
                        TableName = startingNumber.TableName,
                        UpdatedBy = startingNumber.UpdatedBy,
                        UpdatedOn = startingNumber.UpdatedOn
                    };
                    

                        
                    
                    _genericRepository.Insert(dtoNumber);
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
            string newCode = string.Empty;
            if (startingNumber != null)
            {
                int maxNumber = startingNumber.NumberWidth;


                newCode = startingNumber.Prefix + "-" + startingNumber.Number;

            }
            return newCode;

        }
    }
}
