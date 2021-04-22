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
            CreateMap<UserModel, DTOModel.User>();
        }
    }
}
