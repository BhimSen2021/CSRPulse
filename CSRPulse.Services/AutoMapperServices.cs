using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using DTOModel = CSRPulse.Data.Models;
using CSRPulse.Model;
using CSRPulse.Model.Admin;
using System.Collections.ObjectModel;

namespace CSRPulse.Services
{
    public class AutoMapperServices : Profile
    {
        public AutoMapperServices()
        {
            CreateMap<DTOModel.Plan, Plan>().ReverseMap();
            CreateMap<Plan, Plan>();
            CreateMap<UserTypeModel, DTOModel.UserType>();

            CreateMap<DTOModel.Customer, Customer>()
           .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.CustomerId))
           .ForMember(d => d.CustomerCode, o => o.MapFrom(s => s.CustomerCode))
           .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.CustomerName))
           .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
           .ForMember(d => d.Address2, o => o.MapFrom(s => s.Address2))
           .ForMember(d => d.Country, o => o.MapFrom(s => s.Country))
           .ForMember(d => d.City, o => o.MapFrom(s => s.City))
           .ForMember(d => d.PostalCode, o => o.MapFrom(s => s.PostalCode))
           .ForMember(d => d.Telephone, o => o.MapFrom(s => s.Telephone))
           .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
           .ForMember(d => d.Website, o => o.MapFrom(s => s.Website))
           .ForMember(d => d.DataBaseName, o => o.MapFrom(s => s.DataBaseName))
           .ForMember(d => d.IsDeleted, o => o.MapFrom(s => s.IsDeleted))
           .ForMember(d => d.CreatedOn, o => o.MapFrom(s => s.CreatedOn))
           //.ForMember(d => d.CustomerUniqueId, o => o.MapFrom(s => s.CustomerUniqueId))
           .ForMember(d => d.StateId, o => o.MapFrom(s => s.StateId))
           .ReverseMap()
           .ForAllOtherMembers(i => i.Ignore());


            CreateMap<StartingNumber, DTOModel.StartingNumber>();
            CreateMap<CustomerPayment, DTOModel.CustomerPayment>().ReverseMap();
            CreateMap<CustomerLicenseActivation, DTOModel.CustomerLicenseActivation>().ReverseMap();
            CreateMap<DTOModel.User, UserDetail>()
                .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.RoleName)).ReverseMap();
            CreateMap<DTOModel.UserRights, List<UserRight>>().ReverseMap();
            CreateMap<DTOModel.Menu, List<Menu>>();
            CreateMap<DTOModel.User, User>();

            CreateMap<DTOModel.Role, Role>();
            CreateMap<DTOModel.Maintenance, Maintenance>().ReverseMap();

            #region Email Mapper
            CreateMap<Common.EmailMessage, DTOModel.MailSendStatus>()
                .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.CustomerId))
                .ForMember(d => d.ToEmails, o => o.MapFrom(s => s.To))
                .ForMember(d => d.CcEmails, o => o.MapFrom(s => s.CC))
                .ForMember(d => d.MailContent, o => o.MapFrom(s => s.Body))
                .ForMember(d => d.SubjectId, o => o.MapFrom(s => s.SubjectId))
            .ForAllOtherMembers(d => d.Ignore());
            #endregion

            #region Dropdown Mapper
            CreateMap<DTOModel.State, SelectListModel>()
                .ForMember(d => d.id, o => o.MapFrom(s => s.StateId))
                .ForMember(d => d.value, o => o.MapFrom(s => s.StateName));
            #endregion
        }
    }
}
