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
            CreateMap<DTOModel.Plan, Plan>().ReverseMap();            
            CreateMap<UserTypeModel, DTOModel.UserType>();
            CreateMap<Customer, DTOModel.Customer>();

            CreateMap<StartingNumber, DTOModel.StartingNumber>();
            CreateMap<CustomerPayment, DTOModel.CustomerPayment>().ReverseMap();
            CreateMap<CustomerLicenseActivation, DTOModel.CustomerLicenseActivation>().ReverseMap();
            CreateMap<DTOModel.User, UserDetail>().ReverseMap();
            CreateMap<DTOModel.UserRights, List<UserRight>>().ReverseMap();
            CreateMap<DTOModel.Menu, List<Menu>>();
            
            #region Dropdown Mapper
            CreateMap<DTOModel.State, SelectListModel>()
                .ForMember(d => d.id, o => o.MapFrom(s => s.StateId))
                .ForMember(d => d.value, o => o.MapFrom(s => s.StateName));
            #endregion
            
        }
    }
}
