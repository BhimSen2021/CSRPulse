using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using DTOModel = CSRPulse.Data.Models;
using CSRPulse.Model;
using CSRPulse.Model.Admin;
namespace CSRPulse.Services
{
    public class AutoMapperServices : Profile
    {
        public AutoMapperServices()
        {
            CreateMap<DTOModel.Plan, PlanModel>();
            CreateMap<PlanModel, DTOModel.Plan>();
            CreateMap<UserTypeModel, DTOModel.UserType>();
            CreateMap<Customer, DTOModel.Customer>();
            CreateMap<StartingNumber, DTOModel.StartingNumber>();
            CreateMap<CustomerPayment, DTOModel.CustomerPayment>().ReverseMap();
            CreateMap<CustomerLicenseActivation, DTOModel.CustomerLicenseActivation>().ReverseMap();
            CreateMap<DTOModel.User, UserDetail>().ReverseMap();
            CreateMap<DTOModel.UserRights, UserRight>().ReverseMap();

            
        }
    }
}
