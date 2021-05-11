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
            CreateMap<DTOModel.Customer, Customer>();

            CreateMap<StartingNumber, DTOModel.StartingNumber>();
            CreateMap<CustomerPayment, DTOModel.CustomerPayment>().ReverseMap();
            CreateMap<CustomerLicenseActivation, DTOModel.CustomerLicenseActivation>().ReverseMap();
            CreateMap<DTOModel.User, UserDetail>().ReverseMap();
            CreateMap<DTOModel.UserRights, List<UserRight>>().ReverseMap();
            CreateMap<DTOModel.Menu, List<Menu>>();

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
