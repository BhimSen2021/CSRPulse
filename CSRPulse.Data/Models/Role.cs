using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            ActivityCreatedR = new HashSet<Activity>();
            ActivityUpdatedR = new HashSet<Activity>();
            AuditReviewModuleCreatedR = new HashSet<AuditReviewModule>();
            AuditReviewModuleUpdatedR = new HashSet<AuditReviewModule>();
            AuditReviewParamterCreatedR = new HashSet<AuditReviewParamter>();
            AuditReviewParamterUpdatedR = new HashSet<AuditReviewParamter>();
            AuditorCreatedR = new HashSet<Auditor>();
            AuditorDocumentCreatedR = new HashSet<AuditorDocument>();
            AuditorDocumentUpdatedR = new HashSet<AuditorDocument>();
            AuditorUpdatedR = new HashSet<Auditor>();
            BlockCreatedR = new HashSet<Block>();
            BlockUpdatedR = new HashSet<Block>();
            CustomerCreatedR = new HashSet<Customer>();
            CustomerLicenseActivationCreatedR = new HashSet<CustomerLicenseActivation>();
            CustomerLicenseActivationUpdatedR = new HashSet<CustomerLicenseActivation>();
            CustomerPaymentCreatedR = new HashSet<CustomerPayment>();
            CustomerPaymentUpdatedR = new HashSet<CustomerPayment>();
            CustomerUpdatedR = new HashSet<Customer>();
            DepartmentCreatedR = new HashSet<Department>();
            DepartmentUpdatedR = new HashSet<Department>();
            DesignationCreatedR = new HashSet<Designation>();
            DesignationHistoryCreatedR = new HashSet<DesignationHistory>();
            DesignationHistoryUpdatedR = new HashSet<DesignationHistory>();
            DesignationUpdatedR = new HashSet<Designation>();
            DistrictCreatedR = new HashSet<District>();
            DistrictUpdatedR = new HashSet<District>();
            EmailConfigurationCreatedR = new HashSet<EmailConfiguration>();
            EmailConfigurationUpdatedR = new HashSet<EmailConfiguration>();
            FinancialAuditReportCreatedR = new HashSet<FinancialAuditReport>();
            FinancialAuditReportUpdatedR = new HashSet<FinancialAuditReport>();
            FundingAgencyCreatedR = new HashSet<FundingAgency>();
            FundingAgencyUpdatedR = new HashSet<FundingAgency>();
            FundingSourceCreatedR = new HashSet<FundingSource>();
            FundingSourceUpdatedR = new HashSet<FundingSource>();
            IndicatorCreatedR = new HashSet<Indicator>();
            IndicatorResponseTypeCreatedR = new HashSet<IndicatorResponseType>();
            IndicatorResponseTypeUpdatedR = new HashSet<IndicatorResponseType>();
            IndicatorTypeCreatedR = new HashSet<IndicatorType>();
            IndicatorTypeUpdatedR = new HashSet<IndicatorType>();
            IndicatorUpdatedR = new HashSet<Indicator>();
            MailProcessCreatedR = new HashSet<MailProcess>();
            MailProcessUpdatedR = new HashSet<MailProcess>();
            MailSendStatusCreatedR = new HashSet<MailSendStatus>();
            MailSendStatusUpdatedR = new HashSet<MailSendStatus>();
            MailSubjectCreatedR = new HashSet<MailSubject>();
            MailSubjectUpdatedR = new HashSet<MailSubject>();
            MaintenanceCreatedR = new HashSet<Maintenance>();
            MaintenanceUpdatedR = new HashSet<Maintenance>();
            MenuCreatedR = new HashSet<Menu>();
            MenuUpdatedR = new HashSet<Menu>();
            NgoawardDetailCreatedR = new HashSet<NgoawardDetail>();
            NgoawardDetailUpdatedR = new HashSet<NgoawardDetail>();
            NgochartDocumentCreatedR = new HashSet<NgochartDocument>();
            NgochartDocumentUpdatedR = new HashSet<NgochartDocument>();
            NgocorpusGrantFundCreatedR = new HashSet<NgocorpusGrantFund>();
            NgocorpusGrantFundUpdatedR = new HashSet<NgocorpusGrantFund>();
            NgofundingPartnerCreatedR = new HashSet<NgofundingPartner>();
            NgofundingPartnerUpdatedR = new HashSet<NgofundingPartner>();
            NgokeyProjectsCreatedR = new HashSet<NgokeyProjects>();
            NgokeyProjectsUpdatedR = new HashSet<NgokeyProjects>();
            NgomemberCreatedR = new HashSet<Ngomember>();
            NgomemberUpdatedR = new HashSet<Ngomember>();
            NgoregistrationDetailCreatedR = new HashSet<NgoregistrationDetail>();
            NgoregistrationDetailUpdatedR = new HashSet<NgoregistrationDetail>();
            NgosaturatoryAuditorDetailCreatedR = new HashSet<NgosaturatoryAuditorDetail>();
            NgosaturatoryAuditorDetailUpdatedR = new HashSet<NgosaturatoryAuditorDetail>();
            PartnerCreatedR = new HashSet<Partner>();
            PartnerDocumentCreatedR = new HashSet<PartnerDocument>();
            PartnerDocumentUpdatedR = new HashSet<PartnerDocument>();
            PartnerPolicyCreatedR = new HashSet<PartnerPolicy>();
            PartnerPolicyModuleCreatedR = new HashSet<PartnerPolicyModule>();
            PartnerPolicyModuleUpdatedR = new HashSet<PartnerPolicyModule>();
            PartnerPolicyUpdatedR = new HashSet<PartnerPolicy>();
            PartnerUpdatedR = new HashSet<Partner>();
            PlanCreatedR = new HashSet<Plan>();
            PlanUpdatedR = new HashSet<Plan>();
            ProcessCreatedR = new HashSet<Process>();
            ProcessDocumentCreatedR = new HashSet<ProcessDocument>();
            ProcessDocumentUpdatedR = new HashSet<ProcessDocument>();
            ProcessSetupCreatedR = new HashSet<ProcessSetup>();
            ProcessSetupHistoryCreatedR = new HashSet<ProcessSetupHistory>();
            ProcessSetupHistoryUpdatedR = new HashSet<ProcessSetupHistory>();
            ProcessSetupPrimaryRole = new HashSet<ProcessSetup>();
            ProcessSetupQuaternaryRole = new HashSet<ProcessSetup>();
            ProcessSetupSecondoryRole = new HashSet<ProcessSetup>();
            ProcessSetupTertiaryRole = new HashSet<ProcessSetup>();
            ProcessSetupUpdatedR = new HashSet<ProcessSetup>();
            ProcessUpdatedR = new HashSet<Process>();
            ProcessWorkFlowCreatedR = new HashSet<ProcessWorkFlow>();
            ProcessWorkFlowUpdatedR = new HashSet<ProcessWorkFlow>();
            ProductCreatedR = new HashSet<Product>();
            ProductUpdatedR = new HashSet<Product>();
            ProjectCommunicationCreatedR = new HashSet<ProjectCommunication>();
            ProjectCommunicationUpdatedR = new HashSet<ProjectCommunication>();
            ProjectCreatedR = new HashSet<Project>();
            ProjectDocumentCreatedR = new HashSet<ProjectDocument>();
            ProjectDocumentUpdatedR = new HashSet<ProjectDocument>();
            ProjectFinancialReportCreatedR = new HashSet<ProjectFinancialReport>();
            ProjectFinancialReportUpdatedR = new HashSet<ProjectFinancialReport>();
            ProjectInternalSourceCreatedR = new HashSet<ProjectInternalSource>();
            ProjectInternalSourceUpdatedR = new HashSet<ProjectInternalSource>();
            ProjectInterventionReportCreatedR = new HashSet<ProjectInterventionReport>();
            ProjectInterventionReportUpdatedR = new HashSet<ProjectInterventionReport>();
            ProjectLocationCreatedR = new HashSet<ProjectLocation>();
            ProjectLocationDetailCreatedR = new HashSet<ProjectLocationDetail>();
            ProjectLocationDetailUpdatedR = new HashSet<ProjectLocationDetail>();
            ProjectLocationUpdatedR = new HashSet<ProjectLocation>();
            ProjectOtherSourceCreatedR = new HashSet<ProjectOtherSource>();
            ProjectOtherSourceUpdatedR = new HashSet<ProjectOtherSource>();
            ProjectReportCreatedR = new HashSet<ProjectReport>();
            ProjectReportUpdatedR = new HashSet<ProjectReport>();
            ProjectUpdatedR = new HashSet<Project>();
            StartingNumberCreatedR = new HashSet<StartingNumber>();
            StartingNumberUpdatedR = new HashSet<StartingNumber>();
            StateCreatedR = new HashSet<State>();
            StateUpdatedR = new HashSet<State>();
            SubActivityCreatedR = new HashSet<SubActivity>();
            SubActivityUpdatedR = new HashSet<SubActivity>();
            SubThemeCreatedR = new HashSet<SubTheme>();
            SubThemeUpdatedR = new HashSet<SubTheme>();
            ThemeCreatedR = new HashSet<Theme>();
            ThemeUpdatedR = new HashSet<Theme>();
            UomCreatedR = new HashSet<Uom>();
            UomUpdatedR = new HashSet<Uom>();
            User = new HashSet<User>();
            UserRightsCreatedR = new HashSet<UserRights>();
            UserRightsRole = new HashSet<UserRights>();
            UserRightsUpdatedR = new HashSet<UserRights>();
            UserRole = new HashSet<UserRole>();
            UserTypeCreatedR = new HashSet<UserType>();
            UserTypeUpdatedR = new HashSet<UserType>();
            VillageCreatedR = new HashSet<Village>();
            VillageUpdatedR = new HashSet<Village>();
        }

        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }
        [Required]
        [StringLength(10)]
        public string RoleShortName { get; set; }
        public int? Seniorty { get; set; }
        [StringLength(500)]
        public string Details { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public int? ReportTo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column("CreatedRId")]
        public int CreatedRid { get; set; }
        [Required]
        [Column("CreatedRName")]
        [StringLength(256)]
        public string CreatedRname { get; set; }
        [Column("UpdatedRId")]
        public int? UpdatedRid { get; set; }
        [Column("UpdatedRName")]
        [StringLength(256)]
        public string UpdatedRname { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty("RoleCreatedByNavigation")]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("RoleUpdatedByNavigation")]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Activity.CreatedR))]
        public virtual ICollection<Activity> ActivityCreatedR { get; set; }
        [InverseProperty(nameof(Activity.UpdatedR))]
        public virtual ICollection<Activity> ActivityUpdatedR { get; set; }
        [InverseProperty(nameof(AuditReviewModule.CreatedR))]
        public virtual ICollection<AuditReviewModule> AuditReviewModuleCreatedR { get; set; }
        [InverseProperty(nameof(AuditReviewModule.UpdatedR))]
        public virtual ICollection<AuditReviewModule> AuditReviewModuleUpdatedR { get; set; }
        [InverseProperty(nameof(AuditReviewParamter.CreatedR))]
        public virtual ICollection<AuditReviewParamter> AuditReviewParamterCreatedR { get; set; }
        [InverseProperty(nameof(AuditReviewParamter.UpdatedR))]
        public virtual ICollection<AuditReviewParamter> AuditReviewParamterUpdatedR { get; set; }
        [InverseProperty(nameof(Auditor.CreatedR))]
        public virtual ICollection<Auditor> AuditorCreatedR { get; set; }
        [InverseProperty(nameof(AuditorDocument.CreatedR))]
        public virtual ICollection<AuditorDocument> AuditorDocumentCreatedR { get; set; }
        [InverseProperty(nameof(AuditorDocument.UpdatedR))]
        public virtual ICollection<AuditorDocument> AuditorDocumentUpdatedR { get; set; }
        [InverseProperty(nameof(Auditor.UpdatedR))]
        public virtual ICollection<Auditor> AuditorUpdatedR { get; set; }
        [InverseProperty(nameof(Block.CreatedR))]
        public virtual ICollection<Block> BlockCreatedR { get; set; }
        [InverseProperty(nameof(Block.UpdatedR))]
        public virtual ICollection<Block> BlockUpdatedR { get; set; }
        [InverseProperty(nameof(Customer.CreatedR))]
        public virtual ICollection<Customer> CustomerCreatedR { get; set; }
        [InverseProperty(nameof(CustomerLicenseActivation.CreatedR))]
        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivationCreatedR { get; set; }
        [InverseProperty(nameof(CustomerLicenseActivation.UpdatedR))]
        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivationUpdatedR { get; set; }
        [InverseProperty(nameof(CustomerPayment.CreatedR))]
        public virtual ICollection<CustomerPayment> CustomerPaymentCreatedR { get; set; }
        [InverseProperty(nameof(CustomerPayment.UpdatedR))]
        public virtual ICollection<CustomerPayment> CustomerPaymentUpdatedR { get; set; }
        [InverseProperty(nameof(Customer.UpdatedR))]
        public virtual ICollection<Customer> CustomerUpdatedR { get; set; }
        [InverseProperty(nameof(Department.CreatedR))]
        public virtual ICollection<Department> DepartmentCreatedR { get; set; }
        [InverseProperty(nameof(Department.UpdatedR))]
        public virtual ICollection<Department> DepartmentUpdatedR { get; set; }
        [InverseProperty(nameof(Designation.CreatedR))]
        public virtual ICollection<Designation> DesignationCreatedR { get; set; }
        [InverseProperty(nameof(DesignationHistory.CreatedR))]
        public virtual ICollection<DesignationHistory> DesignationHistoryCreatedR { get; set; }
        [InverseProperty(nameof(DesignationHistory.UpdatedR))]
        public virtual ICollection<DesignationHistory> DesignationHistoryUpdatedR { get; set; }
        [InverseProperty(nameof(Designation.UpdatedR))]
        public virtual ICollection<Designation> DesignationUpdatedR { get; set; }
        [InverseProperty(nameof(District.CreatedR))]
        public virtual ICollection<District> DistrictCreatedR { get; set; }
        [InverseProperty(nameof(District.UpdatedR))]
        public virtual ICollection<District> DistrictUpdatedR { get; set; }
        [InverseProperty(nameof(EmailConfiguration.CreatedR))]
        public virtual ICollection<EmailConfiguration> EmailConfigurationCreatedR { get; set; }
        [InverseProperty(nameof(EmailConfiguration.UpdatedR))]
        public virtual ICollection<EmailConfiguration> EmailConfigurationUpdatedR { get; set; }
        [InverseProperty(nameof(FinancialAuditReport.CreatedR))]
        public virtual ICollection<FinancialAuditReport> FinancialAuditReportCreatedR { get; set; }
        [InverseProperty(nameof(FinancialAuditReport.UpdatedR))]
        public virtual ICollection<FinancialAuditReport> FinancialAuditReportUpdatedR { get; set; }
        [InverseProperty(nameof(FundingAgency.CreatedR))]
        public virtual ICollection<FundingAgency> FundingAgencyCreatedR { get; set; }
        [InverseProperty(nameof(FundingAgency.UpdatedR))]
        public virtual ICollection<FundingAgency> FundingAgencyUpdatedR { get; set; }
        [InverseProperty(nameof(FundingSource.CreatedR))]
        public virtual ICollection<FundingSource> FundingSourceCreatedR { get; set; }
        [InverseProperty(nameof(FundingSource.UpdatedR))]
        public virtual ICollection<FundingSource> FundingSourceUpdatedR { get; set; }
        [InverseProperty(nameof(Indicator.CreatedR))]
        public virtual ICollection<Indicator> IndicatorCreatedR { get; set; }
        [InverseProperty(nameof(IndicatorResponseType.CreatedR))]
        public virtual ICollection<IndicatorResponseType> IndicatorResponseTypeCreatedR { get; set; }
        [InverseProperty(nameof(IndicatorResponseType.UpdatedR))]
        public virtual ICollection<IndicatorResponseType> IndicatorResponseTypeUpdatedR { get; set; }
        [InverseProperty(nameof(IndicatorType.CreatedR))]
        public virtual ICollection<IndicatorType> IndicatorTypeCreatedR { get; set; }
        [InverseProperty(nameof(IndicatorType.UpdatedR))]
        public virtual ICollection<IndicatorType> IndicatorTypeUpdatedR { get; set; }
        [InverseProperty(nameof(Indicator.UpdatedR))]
        public virtual ICollection<Indicator> IndicatorUpdatedR { get; set; }
        [InverseProperty(nameof(MailProcess.CreatedR))]
        public virtual ICollection<MailProcess> MailProcessCreatedR { get; set; }
        [InverseProperty(nameof(MailProcess.UpdatedR))]
        public virtual ICollection<MailProcess> MailProcessUpdatedR { get; set; }
        [InverseProperty(nameof(MailSendStatus.CreatedR))]
        public virtual ICollection<MailSendStatus> MailSendStatusCreatedR { get; set; }
        [InverseProperty(nameof(MailSendStatus.UpdatedR))]
        public virtual ICollection<MailSendStatus> MailSendStatusUpdatedR { get; set; }
        [InverseProperty(nameof(MailSubject.CreatedR))]
        public virtual ICollection<MailSubject> MailSubjectCreatedR { get; set; }
        [InverseProperty(nameof(MailSubject.UpdatedR))]
        public virtual ICollection<MailSubject> MailSubjectUpdatedR { get; set; }
        [InverseProperty(nameof(Maintenance.CreatedR))]
        public virtual ICollection<Maintenance> MaintenanceCreatedR { get; set; }
        [InverseProperty(nameof(Maintenance.UpdatedR))]
        public virtual ICollection<Maintenance> MaintenanceUpdatedR { get; set; }
        [InverseProperty(nameof(Menu.CreatedR))]
        public virtual ICollection<Menu> MenuCreatedR { get; set; }
        [InverseProperty(nameof(Menu.UpdatedR))]
        public virtual ICollection<Menu> MenuUpdatedR { get; set; }
        [InverseProperty(nameof(NgoawardDetail.CreatedR))]
        public virtual ICollection<NgoawardDetail> NgoawardDetailCreatedR { get; set; }
        [InverseProperty(nameof(NgoawardDetail.UpdatedR))]
        public virtual ICollection<NgoawardDetail> NgoawardDetailUpdatedR { get; set; }
        [InverseProperty(nameof(NgochartDocument.CreatedR))]
        public virtual ICollection<NgochartDocument> NgochartDocumentCreatedR { get; set; }
        [InverseProperty(nameof(NgochartDocument.UpdatedR))]
        public virtual ICollection<NgochartDocument> NgochartDocumentUpdatedR { get; set; }
        [InverseProperty(nameof(NgocorpusGrantFund.CreatedR))]
        public virtual ICollection<NgocorpusGrantFund> NgocorpusGrantFundCreatedR { get; set; }
        [InverseProperty(nameof(NgocorpusGrantFund.UpdatedR))]
        public virtual ICollection<NgocorpusGrantFund> NgocorpusGrantFundUpdatedR { get; set; }
        [InverseProperty(nameof(NgofundingPartner.CreatedR))]
        public virtual ICollection<NgofundingPartner> NgofundingPartnerCreatedR { get; set; }
        [InverseProperty(nameof(NgofundingPartner.UpdatedR))]
        public virtual ICollection<NgofundingPartner> NgofundingPartnerUpdatedR { get; set; }
        [InverseProperty(nameof(NgokeyProjects.CreatedR))]
        public virtual ICollection<NgokeyProjects> NgokeyProjectsCreatedR { get; set; }
        [InverseProperty(nameof(NgokeyProjects.UpdatedR))]
        public virtual ICollection<NgokeyProjects> NgokeyProjectsUpdatedR { get; set; }
        [InverseProperty(nameof(Ngomember.CreatedR))]
        public virtual ICollection<Ngomember> NgomemberCreatedR { get; set; }
        [InverseProperty(nameof(Ngomember.UpdatedR))]
        public virtual ICollection<Ngomember> NgomemberUpdatedR { get; set; }
        [InverseProperty(nameof(NgoregistrationDetail.CreatedR))]
        public virtual ICollection<NgoregistrationDetail> NgoregistrationDetailCreatedR { get; set; }
        [InverseProperty(nameof(NgoregistrationDetail.UpdatedR))]
        public virtual ICollection<NgoregistrationDetail> NgoregistrationDetailUpdatedR { get; set; }
        [InverseProperty(nameof(NgosaturatoryAuditorDetail.CreatedR))]
        public virtual ICollection<NgosaturatoryAuditorDetail> NgosaturatoryAuditorDetailCreatedR { get; set; }
        [InverseProperty(nameof(NgosaturatoryAuditorDetail.UpdatedR))]
        public virtual ICollection<NgosaturatoryAuditorDetail> NgosaturatoryAuditorDetailUpdatedR { get; set; }
        [InverseProperty(nameof(Partner.CreatedR))]
        public virtual ICollection<Partner> PartnerCreatedR { get; set; }
        [InverseProperty(nameof(PartnerDocument.CreatedR))]
        public virtual ICollection<PartnerDocument> PartnerDocumentCreatedR { get; set; }
        [InverseProperty(nameof(PartnerDocument.UpdatedR))]
        public virtual ICollection<PartnerDocument> PartnerDocumentUpdatedR { get; set; }
        [InverseProperty(nameof(PartnerPolicy.CreatedR))]
        public virtual ICollection<PartnerPolicy> PartnerPolicyCreatedR { get; set; }
        [InverseProperty(nameof(PartnerPolicyModule.CreatedR))]
        public virtual ICollection<PartnerPolicyModule> PartnerPolicyModuleCreatedR { get; set; }
        [InverseProperty(nameof(PartnerPolicyModule.UpdatedR))]
        public virtual ICollection<PartnerPolicyModule> PartnerPolicyModuleUpdatedR { get; set; }
        [InverseProperty(nameof(PartnerPolicy.UpdatedR))]
        public virtual ICollection<PartnerPolicy> PartnerPolicyUpdatedR { get; set; }
        [InverseProperty(nameof(Partner.UpdatedR))]
        public virtual ICollection<Partner> PartnerUpdatedR { get; set; }
        [InverseProperty(nameof(Plan.CreatedR))]
        public virtual ICollection<Plan> PlanCreatedR { get; set; }
        [InverseProperty(nameof(Plan.UpdatedR))]
        public virtual ICollection<Plan> PlanUpdatedR { get; set; }
        [InverseProperty(nameof(Process.CreatedR))]
        public virtual ICollection<Process> ProcessCreatedR { get; set; }
        [InverseProperty(nameof(ProcessDocument.CreatedR))]
        public virtual ICollection<ProcessDocument> ProcessDocumentCreatedR { get; set; }
        [InverseProperty(nameof(ProcessDocument.UpdatedR))]
        public virtual ICollection<ProcessDocument> ProcessDocumentUpdatedR { get; set; }
        [InverseProperty(nameof(ProcessSetup.CreatedR))]
        public virtual ICollection<ProcessSetup> ProcessSetupCreatedR { get; set; }
        [InverseProperty(nameof(ProcessSetupHistory.CreatedR))]
        public virtual ICollection<ProcessSetupHistory> ProcessSetupHistoryCreatedR { get; set; }
        [InverseProperty(nameof(ProcessSetupHistory.UpdatedR))]
        public virtual ICollection<ProcessSetupHistory> ProcessSetupHistoryUpdatedR { get; set; }
        [InverseProperty(nameof(ProcessSetup.PrimaryRole))]
        public virtual ICollection<ProcessSetup> ProcessSetupPrimaryRole { get; set; }
        [InverseProperty(nameof(ProcessSetup.QuaternaryRole))]
        public virtual ICollection<ProcessSetup> ProcessSetupQuaternaryRole { get; set; }
        [InverseProperty(nameof(ProcessSetup.SecondoryRole))]
        public virtual ICollection<ProcessSetup> ProcessSetupSecondoryRole { get; set; }
        [InverseProperty(nameof(ProcessSetup.TertiaryRole))]
        public virtual ICollection<ProcessSetup> ProcessSetupTertiaryRole { get; set; }
        [InverseProperty(nameof(ProcessSetup.UpdatedR))]
        public virtual ICollection<ProcessSetup> ProcessSetupUpdatedR { get; set; }
        [InverseProperty(nameof(Process.UpdatedR))]
        public virtual ICollection<Process> ProcessUpdatedR { get; set; }
        [InverseProperty(nameof(ProcessWorkFlow.CreatedR))]
        public virtual ICollection<ProcessWorkFlow> ProcessWorkFlowCreatedR { get; set; }
        [InverseProperty(nameof(ProcessWorkFlow.UpdatedR))]
        public virtual ICollection<ProcessWorkFlow> ProcessWorkFlowUpdatedR { get; set; }
        [InverseProperty(nameof(Product.CreatedR))]
        public virtual ICollection<Product> ProductCreatedR { get; set; }
        [InverseProperty(nameof(Product.UpdatedR))]
        public virtual ICollection<Product> ProductUpdatedR { get; set; }
        [InverseProperty(nameof(ProjectCommunication.CreatedR))]
        public virtual ICollection<ProjectCommunication> ProjectCommunicationCreatedR { get; set; }
        [InverseProperty(nameof(ProjectCommunication.UpdatedR))]
        public virtual ICollection<ProjectCommunication> ProjectCommunicationUpdatedR { get; set; }
        [InverseProperty(nameof(Project.CreatedR))]
        public virtual ICollection<Project> ProjectCreatedR { get; set; }
        [InverseProperty(nameof(ProjectDocument.CreatedR))]
        public virtual ICollection<ProjectDocument> ProjectDocumentCreatedR { get; set; }
        [InverseProperty(nameof(ProjectDocument.UpdatedR))]
        public virtual ICollection<ProjectDocument> ProjectDocumentUpdatedR { get; set; }
        [InverseProperty(nameof(ProjectFinancialReport.CreatedR))]
        public virtual ICollection<ProjectFinancialReport> ProjectFinancialReportCreatedR { get; set; }
        [InverseProperty(nameof(ProjectFinancialReport.UpdatedR))]
        public virtual ICollection<ProjectFinancialReport> ProjectFinancialReportUpdatedR { get; set; }
        [InverseProperty(nameof(ProjectInternalSource.CreatedR))]
        public virtual ICollection<ProjectInternalSource> ProjectInternalSourceCreatedR { get; set; }
        [InverseProperty(nameof(ProjectInternalSource.UpdatedR))]
        public virtual ICollection<ProjectInternalSource> ProjectInternalSourceUpdatedR { get; set; }
        [InverseProperty(nameof(ProjectInterventionReport.CreatedR))]
        public virtual ICollection<ProjectInterventionReport> ProjectInterventionReportCreatedR { get; set; }
        [InverseProperty(nameof(ProjectInterventionReport.UpdatedR))]
        public virtual ICollection<ProjectInterventionReport> ProjectInterventionReportUpdatedR { get; set; }
        [InverseProperty(nameof(ProjectLocation.CreatedR))]
        public virtual ICollection<ProjectLocation> ProjectLocationCreatedR { get; set; }
        [InverseProperty(nameof(ProjectLocationDetail.CreatedR))]
        public virtual ICollection<ProjectLocationDetail> ProjectLocationDetailCreatedR { get; set; }
        [InverseProperty(nameof(ProjectLocationDetail.UpdatedR))]
        public virtual ICollection<ProjectLocationDetail> ProjectLocationDetailUpdatedR { get; set; }
        [InverseProperty(nameof(ProjectLocation.UpdatedR))]
        public virtual ICollection<ProjectLocation> ProjectLocationUpdatedR { get; set; }
        [InverseProperty(nameof(ProjectOtherSource.CreatedR))]
        public virtual ICollection<ProjectOtherSource> ProjectOtherSourceCreatedR { get; set; }
        [InverseProperty(nameof(ProjectOtherSource.UpdatedR))]
        public virtual ICollection<ProjectOtherSource> ProjectOtherSourceUpdatedR { get; set; }
        [InverseProperty(nameof(ProjectReport.CreatedR))]
        public virtual ICollection<ProjectReport> ProjectReportCreatedR { get; set; }
        [InverseProperty(nameof(ProjectReport.UpdatedR))]
        public virtual ICollection<ProjectReport> ProjectReportUpdatedR { get; set; }
        [InverseProperty(nameof(Project.UpdatedR))]
        public virtual ICollection<Project> ProjectUpdatedR { get; set; }
        [InverseProperty(nameof(StartingNumber.CreatedR))]
        public virtual ICollection<StartingNumber> StartingNumberCreatedR { get; set; }
        [InverseProperty(nameof(StartingNumber.UpdatedR))]
        public virtual ICollection<StartingNumber> StartingNumberUpdatedR { get; set; }
        [InverseProperty(nameof(State.CreatedR))]
        public virtual ICollection<State> StateCreatedR { get; set; }
        [InverseProperty(nameof(State.UpdatedR))]
        public virtual ICollection<State> StateUpdatedR { get; set; }
        [InverseProperty(nameof(SubActivity.CreatedR))]
        public virtual ICollection<SubActivity> SubActivityCreatedR { get; set; }
        [InverseProperty(nameof(SubActivity.UpdatedR))]
        public virtual ICollection<SubActivity> SubActivityUpdatedR { get; set; }
        [InverseProperty(nameof(SubTheme.CreatedR))]
        public virtual ICollection<SubTheme> SubThemeCreatedR { get; set; }
        [InverseProperty(nameof(SubTheme.UpdatedR))]
        public virtual ICollection<SubTheme> SubThemeUpdatedR { get; set; }
        [InverseProperty(nameof(Theme.CreatedR))]
        public virtual ICollection<Theme> ThemeCreatedR { get; set; }
        [InverseProperty(nameof(Theme.UpdatedR))]
        public virtual ICollection<Theme> ThemeUpdatedR { get; set; }
        [InverseProperty(nameof(Uom.CreatedR))]
        public virtual ICollection<Uom> UomCreatedR { get; set; }
        [InverseProperty(nameof(Uom.UpdatedR))]
        public virtual ICollection<Uom> UomUpdatedR { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<User> User { get; set; }
        [InverseProperty(nameof(UserRights.CreatedR))]
        public virtual ICollection<UserRights> UserRightsCreatedR { get; set; }
        [InverseProperty(nameof(UserRights.Role))]
        public virtual ICollection<UserRights> UserRightsRole { get; set; }
        [InverseProperty(nameof(UserRights.UpdatedR))]
        public virtual ICollection<UserRights> UserRightsUpdatedR { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRole { get; set; }
        [InverseProperty(nameof(UserType.CreatedR))]
        public virtual ICollection<UserType> UserTypeCreatedR { get; set; }
        [InverseProperty(nameof(UserType.UpdatedR))]
        public virtual ICollection<UserType> UserTypeUpdatedR { get; set; }
        [InverseProperty(nameof(Village.CreatedR))]
        public virtual ICollection<Village> VillageCreatedR { get; set; }
        [InverseProperty(nameof(Village.UpdatedR))]
        public virtual ICollection<Village> VillageUpdatedR { get; set; }
    }
}
