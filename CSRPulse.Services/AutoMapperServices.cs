﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using DTOModel = CSRPulse.Data.Models;
using CSRPulse.Model;
using CSRPulse.Model.Admin;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSRPulse.Services
{
    public class AutoMapperServices : Profile
    {
        public AutoMapperServices()
        {

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
           .ForMember(d => d.CustomerUniqueId, o => o.MapFrom(s => s.CustomerUniqueId))
           .ForMember(d => d.StateId, o => o.MapFrom(s => s.StateId))
           .ReverseMap().ForAllOtherMembers(i => i.Ignore());

            CreateMap<DTOModel.CustomerPayment, CustomerPayment>().ReverseMap();
            CreateMap<DTOModel.CustomerLicenseActivation, CustomerLicenseActivation>().ReverseMap();

            CreateMap<DTOModel.User, UserDetail>()
                .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.RoleName)).ReverseMap();
            CreateMap<DTOModel.UserRights, List<UserRight>>().ReverseMap();
            CreateMap<DTOModel.Menu, List<Menu>>();


            CreateMap<DTOModel.User, User>().ForMember(d => d.RoleId, o => o.MapFrom(s => s.RoleId))
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.DepartmentName))
                .ForMember(d => d.DepartmentId, o => o.MapFrom(s => s.Department.DepartmentId))
                .ForMember(r => r.HasMultipelRole, o => o.MapFrom(r => r.UserRole.Count > 0 ? true : false))
                .ReverseMap();

            CreateMap<DTOModel.Role, Model.Role>()
                .ForMember(d => d.RoleId, o => o.MapFrom(s => s.RoleId))
                .ForMember(d => d.RoleName, o => o.MapFrom(s => s.RoleName))
                .ForMember(d => d.RoleShortName, o => o.MapFrom(s => s.RoleShortName))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive))
                .ForMember(d => d.Seniorty, o => o.MapFrom(s => s.Seniorty))
                .ForMember(d => d.ReportTo, o => o.MapFrom(s => s.ReportTo))
                .ReverseMap().ForAllOtherMembers(d => d.Ignore());

            CreateMap<DTOModel.Maintenance, Maintenance>().ReverseMap();
            CreateMap<DTOModel.EmailConfiguration, EmailConfiguration>().ReverseMap();
            CreateMap<DTOModel.FinancialAuditReport, FinancialAuditReport>().ReverseMap();
            CreateMap<DTOModel.FundingSource, FundingSource>().ReverseMap();
            CreateMap<DTOModel.PartnerPolicy, PartnerPolicy>().ReverseMap();
            CreateMap<DTOModel.PartnerPolicyModule, PartnerPolicyModule>().ReverseMap();
            CreateMap<DTOModel.PartnerPolicyDetails, PartnerPolicyDetails>().ReverseMap();
            CreateMap<DTOModel.PartnerPolicyModuleDetails, PartnerPolicyModuleDetails>().ReverseMap();


            CreateMap<DTOModel.UserRole, UserRole>().ReverseMap();


            #region M A S T E R S

            #region Thematic Mapper
            CreateMap<DTOModel.Uom, Uom>().ReverseMap();
            CreateMap<DTOModel.Activity, Activity>().ReverseMap();
            CreateMap<DTOModel.SubActivity, SubActivity>().ReverseMap();
            CreateMap<DTOModel.Theme, Theme>().ReverseMap();
            CreateMap<DTOModel.SubTheme, SubTheme>().ReverseMap();
            CreateMap<DTOModel.Indicator, Indicator>().ReverseMap();
            #endregion

            #region Location Mapper

            CreateMap<DTOModel.State, State>().ReverseMap();

            CreateMap<DTOModel.District, District>()
                .ForMember(d => d.StateName, o => o.MapFrom(s => s.State.StateName));
            CreateMap<District, DTOModel.District>();


            CreateMap<DTOModel.Block, Block>()
              .ForMember(d => d.StateName, o => o.MapFrom(s => s.State.StateName))
             .ForMember(d => d.DistrictName, o => o.MapFrom(s => s.District.DistrictName));
            CreateMap<Block, DTOModel.Block>();

            CreateMap<DTOModel.Village, Village>()
            .ForMember(d => d.StateName, o => o.MapFrom(s => s.State.StateName))
           .ForMember(d => d.DistrictName, o => o.MapFrom(s => s.District.DistrictName))
            .ForMember(d => d.BlockName, o => o.MapFrom(s => s.Block.BlockName));

            CreateMap<Village, DTOModel.Village>()
                .ForMember(d => d.LocationType, o => o.MapFrom(s => (int)s.LocationType));
            #endregion

            CreateMap<DTOModel.Plan, Plan>().ReverseMap();
            CreateMap<Plan, Plan>();
            CreateMap<UserTypeModel, DTOModel.UserType>();
            CreateMap<StartingNumber, DTOModel.StartingNumber>();
            CreateMap<DTOModel.Department, Department>().ReverseMap();

            CreateMap<DTOModel.Process, Process>()
                .ForMember(d => d.ProcessId, o => o.MapFrom(s => s.ProcessId))
                .ForMember(d => d.ProcessName, o => o.MapFrom(s => s.ProcessName))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive))
                .ForMember(d => d.FinalStatus, o => o.MapFrom(s => s.FinalStatus))
                .ReverseMap().ForAllOtherMembers(d => d.Ignore());

            CreateMap<DTOModel.Designation, Model.Designation>()
                .ForMember(d => d.DesignationId, o => o.MapFrom(s => s.DesignationId))
                .ForMember(d => d.DesignationName, o => o.MapFrom(s => s.DesignationName))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(s => s.CreatedBy))
                .ForMember(d => d.CreatedOn, o => o.MapFrom(s => s.CreatedOn))
                .ForMember(d => d.UpdatedBy, o => o.MapFrom(s => s.UpdatedBy))
                .ForMember(d => d.UpdatedOn, o => o.MapFrom(s => s.UpdatedOn))
                .ReverseMap().ForAllOtherMembers(d => d.Ignore());


            CreateMap<DTOModel.DesignationHistory, DesignationHistory>().ReverseMap();

            CreateMap<DTOModel.DesignationHistory, DesignationHistory>()
               .ForMember(d => d.Designation, o => o.MapFrom(s => s.Designation.DesignationName));
            CreateMap<DesignationHistory, DTOModel.DesignationHistory>();

            CreateMap<DTOModel.ProcessSetup, ProcessSetup>().ReverseMap();

            CreateMap<DTOModel.ProcessSetup, DTOModel.ProcessSetupHistory>()
            .ForMember(d => d.ProcessId, o => o.MapFrom(s => s.ProcessId))
            .ForMember(d => d.RevisionNo, o => o.MapFrom(s => s.RevisionNo))
            .ForMember(d => d.PrimaryRoleId, o => o.MapFrom(s => s.PrimaryRoleId))
            .ForMember(d => d.SecondoryRoleId, o => o.MapFrom(s => s.SecondoryRoleId))
            .ForMember(d => d.TertiaryRoleId, o => o.MapFrom(s => s.TertiaryRoleId))
            .ForMember(d => d.QuaternaryRoleId, o => o.MapFrom(s => s.QuaternaryRoleId))
            .ForMember(d => d.LevelId, o => o.MapFrom(s => s.LevelId))
            .ForMember(d => d.Sequence, o => o.MapFrom(s => s.Sequence))
            .ForMember(d => d.Skip, o => o.MapFrom(s => s.Skip))
            .ForMember(d => d.CreatedBy, o => o.MapFrom(s => s.CreatedBy));

            CreateMap<DTOModel.Partner, Partner>().ReverseMap();

            CreateMap<DTOModel.NgoawardDetail, NgoawardDetail>().ReverseMap();
            CreateMap<DTOModel.NgosaturatoryAuditorDetail, NgosaturatoryAuditorDetail>().ReverseMap();
            CreateMap<DTOModel.NgokeyProjects, NGOKeyProjects>().ReverseMap();
            CreateMap<DTOModel.NgocorpusGrantFund, NGOCorpusGrantFund>().ReverseMap();
            CreateMap<DTOModel.NgoregistrationDetail, NGORegistrationDetail>().ReverseMap();
            CreateMap<DTOModel.NgochartDocument, NGOChartDocument>().ReverseMap();
            CreateMap<DTOModel.Ngomember, NGOMember>().ReverseMap();
            CreateMap<DTOModel.FundingAgency, FundingAgency>().ReverseMap();

            CreateMap<DTOModel.NgofundingPartner, NGOFundingPartner>()
            .ForMember(d => d.FundingAgency, o => o.MapFrom(s => s.FundingAgency.AgencyName));
            CreateMap<NGOFundingPartner, DTOModel.NgofundingPartner>();
            CreateMap<DTOModel.PartnerDocument, PartnerDocument>().ReverseMap();

            CreateMap<DTOModel.ProcessDocument, ProcessDocument>().ReverseMap();
            CreateMap<DTOModel.NarrativeQuestion, NarrativeQuestion>().ReverseMap();
            CreateMap<DTOModel.ProjectNarrativeQuestion, ProjectNarrativeQuestion>().ReverseMap();
            CreateMap<DTOModel.ProjectNarrativeAnswer, ProjectNarrativeAnswer>().ReverseMap();

            #region A u d i t

            CreateMap<DTOModel.AuditReviewModule, AuditReviewModule>().ReverseMap();
            CreateMap<DTOModel.AuditReviewParamter, AuditReviewParamter>().ReverseMap();

            #endregion

            #region A u d i t o r

            CreateMap<DTOModel.Auditor, Auditor>()
               .ForMember(d => d.StateName, o => o.MapFrom(s => s.State.StateName));

            CreateMap<DTOModel.Auditor, Auditor>().ReverseMap();
            CreateMap<DTOModel.AuditorDocument, AuditorDocument>()
                .ForMember(d => d.DocumentName, o => o.MapFrom(s => s.Document.DocumentName));

            CreateMap<DTOModel.AuditorDocument, AuditorDocument>().ReverseMap();
            #endregion


            #endregion

            #region Project
            CreateMap<DTOModel.Project, Project>().ReverseMap();
            CreateMap<DTOModel.ProjectReport, ProjectReport>().ReverseMap();
            CreateMap<DTOModel.ProjectOtherSource, ProjectOtherSource>().ReverseMap();
            CreateMap<DTOModel.ProjectInternalSource, ProjectInternalSource>().ReverseMap();
            CreateMap<DTOModel.ProjectInterventionReport, ProjectInterventionReport>().ReverseMap();
            CreateMap<DTOModel.ProjectFinancialReport, ProjectFinancialReport>().ReverseMap();
            CreateMap<DTOModel.ProjectLocation, ProjectLocation>().ReverseMap();

            CreateMap<DTOModel.ProjectLocationDetail, ProjectLocationDetail>()
            .ForMember(d => d.StateName, o => o.MapFrom(s => s.State.StateName))
            .ForMember(d => d.DistrictName, o => o.MapFrom(s => s.District.DistrictName))
            .ForMember(d => d.BlockName, o => o.MapFrom(s => s.Block.BlockName));

            CreateMap<ProjectLocationDetail, DTOModel.ProjectLocationDetail>();

            CreateMap<DTOModel.ProjectDocument, ProjectDocument>().ReverseMap();
            CreateMap<DTOModel.ProjectCommunication, ProjectCommunication>().ReverseMap();
            CreateMap<DTOModel.ProjectTeamDetail, ProjectTeamDetail>().ReverseMap();

            CreateMap<DTOModel.ProjectNarrativeAnswer, ProjectOverviewModule>()
                .ForMember(d => d.ProcessId, o => o.MapFrom(s => s.ProcessId))
                .ForMember(d => d.ProjectId, o => o.MapFrom(s => s.ProjectId))
                .ForMember(d => d.ProjectQuestionId, o => o.MapFrom(s => s.ProjectQuestionId))
                .ForMember(d => d.Projectanswer, o => o.MapFrom(s => s.Answer))
                .ForMember(d => d.CreatedOn, o => o.MapFrom(s => s.CreatedOn))
                .ForMember(d => d.CreatedRid, o => o.MapFrom(s => s.CreatedRname))
                .ForMember(d => d.CreatedRname, o => o.MapFrom(s => s.CreatedRname))
                .ReverseMap();


            #endregion


            #region Role Access Rights
            CreateMap<DTOModel.Menu, RoleAccessRights>()
           .ForMember(d => d.MenuId, o => o.MapFrom(s => s.MenuId))
           .ForMember(d => d.MenuName, o => o.MapFrom(s => s.MenuName))
           .ForMember(d => d.ParentMenuId, o => o.MapFrom(s => s.ParentMenuId))
           .ForMember(c => c.ViewRight, c => c.MapFrom(m => m.UserRights.Where(v => v.MenuId == m.MenuId)
           .FirstOrDefault().ViewRight))
           .ForMember(c => c.EditRight, c => c.MapFrom(m => m.UserRights.Where(v => v.MenuId == m.MenuId)
           .FirstOrDefault().EditRight))
           .ForMember(c => c.CreateRight, c => c.MapFrom(m => m.UserRights.Where(v => v.MenuId == m.MenuId)
           .FirstOrDefault().CreateRight))
           .ForMember(c => c.DeleteRight, c => c.MapFrom(m => m.UserRights.Where(v => v.MenuId == m.MenuId)
           .FirstOrDefault().DeleteRight))
           .ReverseMap().ForAllOtherMembers(d => d.Ignore());

            #endregion
            #region Email Mapper
            CreateMap<Common.EmailMessage, DTOModel.MailSendStatus>()
                .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.CustomerId))
                .ForMember(d => d.ToEmails, o => o.MapFrom(s => s.To))
                .ForMember(d => d.CcEmails, o => o.MapFrom(s => s.CC))
                 .ForMember(d => d.BccEmails, o => o.MapFrom(s => s.Bcc))
                .ForMember(d => d.MailContent, o => o.MapFrom(s => s.Body))
                .ForMember(d => d.SubjectId, o => o.MapFrom(s => s.SubjectId))
            .ForAllOtherMembers(d => d.Ignore());
            #endregion

            #region Dropdown Mapper
            CreateMap<DTOModel.State, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.StateId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.StateName));

            CreateMap<DTOModel.Role, SelectListModel>()
              .ForMember(d => d.id, o => o.MapFrom(s => s.RoleId))
              .ForMember(d => d.value, o => o.MapFrom(s => s.RoleName));

            CreateMap<DTOModel.District, SelectListModel>()
             .ForMember(d => d.id, o => o.MapFrom(s => s.DistrictId))
             .ForMember(d => d.value, o => o.MapFrom(s => s.DistrictName));

            CreateMap<DTOModel.Block, SelectListModel>()
             .ForMember(d => d.id, o => o.MapFrom(s => s.BlockId))
             .ForMember(d => d.value, o => o.MapFrom(s => s.BlockName));


            CreateMap<DTOModel.Theme, SelectListModel>()
             .ForMember(d => d.id, o => o.MapFrom(s => s.ThemeId))
             .ForMember(d => d.value, o => o.MapFrom(s => s.ThemeName));

            CreateMap<DTOModel.SubTheme, SelectListModel>()
             .ForMember(d => d.id, o => o.MapFrom(s => s.SubThemeId))
             .ForMember(d => d.value, o => o.MapFrom(s => s.SubThemeName));

            CreateMap<DTOModel.Activity, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.ActivityId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.ActivityName));

            CreateMap<DTOModel.SubActivity, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.SubActivityId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.SubActivityName));

            CreateMap<DTOModel.Uom, SelectListModel>()
           .ForMember(d => d.id, o => o.MapFrom(s => s.Uomid))
           .ForMember(d => d.value, o => o.MapFrom(s => s.UnitName));

            CreateMap<DTOModel.IndicatorResponseType, SelectListModel>()
           .ForMember(d => d.id, o => o.MapFrom(s => s.ResponseTypeId))
           .ForMember(d => d.value, o => o.MapFrom(s => s.ResponseType));

            CreateMap<DTOModel.IndicatorType, SelectListModel>()
          .ForMember(d => d.id, o => o.MapFrom(s => s.IndicatorTypeId))
          .ForMember(d => d.value, o => o.MapFrom(s => s.IndicatorType1));

            CreateMap<DTOModel.Department, SelectListModel>()
         .ForMember(d => d.id, o => o.MapFrom(s => s.DepartmentId))
         .ForMember(d => d.value, o => o.MapFrom(s => s.DepartmentName));

            CreateMap<DTOModel.Designation, SelectListModel>()
          .ForMember(d => d.id, o => o.MapFrom(s => s.DesignationId))
          .ForMember(d => d.value, o => o.MapFrom(s => s.DesignationName));

            CreateMap<DTOModel.Partner, SelectListModel>()
          .ForMember(d => d.id, o => o.MapFrom(s => s.PartnerId))
          .ForMember(d => d.value, o => o.MapFrom(s => s.PartnerName));

            CreateMap<DTOModel.Process, SelectListModel>()
                .ForMember(d => d.id, o => o.MapFrom(s => s.ProcessId))
                .ForMember(d => d.value, o => o.MapFrom(s => s.ProcessName));

            CreateMap<DTOModel.AuditReviewModule, SelectListModel>()
               .ForMember(d => d.id, o => o.MapFrom(s => s.ModuleId))
               .ForMember(d => d.value, o => o.MapFrom(s => s.Module));

            CreateMap<DTOModel.FundingAgency, SelectListModel>()
              .ForMember(d => d.id, o => o.MapFrom(s => s.FundingAgencyId))
              .ForMember(d => d.value, o => o.MapFrom(s => s.AgencyName));

            CreateMap<DTOModel.ProcessDocument, SelectListModel>()
             .ForMember(d => d.id, o => o.MapFrom(s => s.DocumentId))
             .ForMember(d => d.value, o => o.MapFrom(s => s.DocumentName));

            CreateMap<DTOModel.User, SelectListModel>()
             .ForMember(d => d.id, o => o.MapFrom(s => s.UserId))
             .ForMember(d => d.value, o => o.MapFrom(s => s.FullName));

            CreateMap<DTOModel.State, ProjectLocationDetail>()
             .ForMember(d => d.StateId, o => o.MapFrom(s => s.StateId))
             .ForMember(d => d.StateName, o => o.MapFrom(s => s.StateName));

            CreateMap<DTOModel.District, ProjectLocationDetail>()
             .ForMember(d => d.StateId, o => o.MapFrom(s => s.StateId))
             .ForMember(d => d.DistrictId, o => o.MapFrom(s => s.DistrictId))
             .ForMember(d => d.DistrictName, o => o.MapFrom(s => s.DistrictName));

            CreateMap<DTOModel.Project, SelectListModel>()
             .ForMember(d => d.id, o => o.MapFrom(s => s.ProjectId))
             .ForMember(d => d.value, o => o.MapFrom(s => s.ProjectName));

            CreateMap<DTOModel.Auditor, SelectListModel>()
             .ForMember(d => d.id, o => o.MapFrom(s => s.AuditorId))
             .ForMember(d => d.value, o => o.MapFrom(s => s.AuditOrganization));

            CreateMap<DTOModel.PartnerPolicy, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.PolicyId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.PolicyName));

            CreateMap<DTOModel.FundingSource, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.SourceId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.SourceName));

            #endregion


        }
    }
}
