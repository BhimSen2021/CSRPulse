using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class User
    {
        public User()
        {
            ActivityCreatedByNavigation = new HashSet<Activity>();
            ActivityUpdatedByNavigation = new HashSet<Activity>();
            AuditReviewModuleCreatedByNavigation = new HashSet<AuditReviewModule>();
            AuditReviewModuleUpdatedByNavigation = new HashSet<AuditReviewModule>();
            AuditReviewParamterCreatedByNavigation = new HashSet<AuditReviewParamter>();
            AuditReviewParamterUpdatedByNavigation = new HashSet<AuditReviewParamter>();
            AuditorCreatedByNavigation = new HashSet<Auditor>();
            AuditorDocumentCreatedByNavigation = new HashSet<AuditorDocument>();
            AuditorDocumentUpdatedByNavigation = new HashSet<AuditorDocument>();
            AuditorUpdatedByNavigation = new HashSet<Auditor>();
            BlockCreatedByNavigation = new HashSet<Block>();
            BlockUpdatedByNavigation = new HashSet<Block>();
            CustomerCreatedByNavigation = new HashSet<Customer>();
            CustomerLicenseActivationCreatedByNavigation = new HashSet<CustomerLicenseActivation>();
            CustomerLicenseActivationUpdatedByNavigation = new HashSet<CustomerLicenseActivation>();
            CustomerPaymentCreatedByNavigation = new HashSet<CustomerPayment>();
            CustomerPaymentUpdatedByNavigation = new HashSet<CustomerPayment>();
            CustomerUpdatedByNavigation = new HashSet<Customer>();
            DepartmentCreatedByNavigation = new HashSet<Department>();
            DepartmentUpdatedByNavigation = new HashSet<Department>();
            DesignationCreatedByNavigation = new HashSet<Designation>();
            DesignationHistoryCreatedByNavigation = new HashSet<DesignationHistory>();
            DesignationHistoryUpdatedByNavigation = new HashSet<DesignationHistory>();
            DesignationHistoryUser = new HashSet<DesignationHistory>();
            DesignationUpdatedByNavigation = new HashSet<Designation>();
            DistrictCreatedByNavigation = new HashSet<District>();
            DistrictUpdatedByNavigation = new HashSet<District>();
            EmailConfigurationCreatedByNavigation = new HashSet<EmailConfiguration>();
            EmailConfigurationUpdatedByNavigation = new HashSet<EmailConfiguration>();
            FinancialAuditReportCreatedByNavigation = new HashSet<FinancialAuditReport>();
            FinancialAuditReportUpdatedByNavigation = new HashSet<FinancialAuditReport>();
            FundingAgencyCreatedByNavigation = new HashSet<FundingAgency>();
            FundingAgencyUpdatedByNavigation = new HashSet<FundingAgency>();
            FundingSourceCreatedByNavigation = new HashSet<FundingSource>();
            FundingSourceUpdatedByNavigation = new HashSet<FundingSource>();
            IndicatorResponseTypeCreatedByNavigation = new HashSet<IndicatorResponseType>();
            IndicatorResponseTypeUpdatedByNavigation = new HashSet<IndicatorResponseType>();
            IndicatorTypeCreatedByNavigation = new HashSet<IndicatorType>();
            IndicatorTypeUpdatedByNavigation = new HashSet<IndicatorType>();
            MailProcessCreatedByNavigation = new HashSet<MailProcess>();
            MailProcessUpdatedByNavigation = new HashSet<MailProcess>();
            MailSendStatusCreatedByNavigation = new HashSet<MailSendStatus>();
            MailSendStatusUpdatedByNavigation = new HashSet<MailSendStatus>();
            MailSubjectCreatedByNavigation = new HashSet<MailSubject>();
            MailSubjectUpdatedByNavigation = new HashSet<MailSubject>();
            MaintenanceCreatedByNavigation = new HashSet<Maintenance>();
            MaintenanceUpdatedByNavigation = new HashSet<Maintenance>();
            MenuCreatedByNavigation = new HashSet<Menu>();
            MenuUpdatedByNavigation = new HashSet<Menu>();
            NgoawardDetailCreatedByNavigation = new HashSet<NgoawardDetail>();
            NgoawardDetailUpdatedByNavigation = new HashSet<NgoawardDetail>();
            NgochartDocumentCreatedByNavigation = new HashSet<NgochartDocument>();
            NgochartDocumentUpdatedByNavigation = new HashSet<NgochartDocument>();
            NgocorpusGrantFundCreatedByNavigation = new HashSet<NgocorpusGrantFund>();
            NgocorpusGrantFundUpdatedByNavigation = new HashSet<NgocorpusGrantFund>();
            NgofundingPartnerCreatedByNavigation = new HashSet<NgofundingPartner>();
            NgofundingPartnerUpdatedByNavigation = new HashSet<NgofundingPartner>();
            NgokeyProjectsCreatedByNavigation = new HashSet<NgokeyProjects>();
            NgokeyProjectsUpdatedByNavigation = new HashSet<NgokeyProjects>();
            NgomemberCreatedByNavigation = new HashSet<Ngomember>();
            NgomemberUpdatedByNavigation = new HashSet<Ngomember>();
            NgoregistrationDetailCreatedByNavigation = new HashSet<NgoregistrationDetail>();
            NgoregistrationDetailUpdatedByNavigation = new HashSet<NgoregistrationDetail>();
            NgosaturatoryAuditorDetailCreatedByNavigation = new HashSet<NgosaturatoryAuditorDetail>();
            NgosaturatoryAuditorDetailUpdatedByNavigation = new HashSet<NgosaturatoryAuditorDetail>();
            PartnerCreatedByNavigation = new HashSet<Partner>();
            PartnerDocumentCreatedByNavigation = new HashSet<PartnerDocument>();
            PartnerDocumentUpdatedByNavigation = new HashSet<PartnerDocument>();
            PartnerPolicyCreatedByNavigation = new HashSet<PartnerPolicy>();
            PartnerPolicyModuleCreatedByNavigation = new HashSet<PartnerPolicyModule>();
            PartnerPolicyModuleUpdatedByNavigation = new HashSet<PartnerPolicyModule>();
            PartnerPolicyUpdatedByNavigation = new HashSet<PartnerPolicy>();
            PartnerUpdatedByNavigation = new HashSet<Partner>();
            PlanCreatedByNavigation = new HashSet<Plan>();
            PlanUpdatedByNavigation = new HashSet<Plan>();
            ProcessCreatedByNavigation = new HashSet<Process>();
            ProcessDocumentCreatedByNavigation = new HashSet<ProcessDocument>();
            ProcessDocumentUpdatedByNavigation = new HashSet<ProcessDocument>();
            ProcessSetupCreatedByNavigation = new HashSet<ProcessSetup>();
            ProcessSetupHistoryCreatedByNavigation = new HashSet<ProcessSetupHistory>();
            ProcessSetupHistoryUpdatedbyNavigation = new HashSet<ProcessSetupHistory>();
            ProcessSetupUpdatedbyNavigation = new HashSet<ProcessSetup>();
            ProcessUpdatedByNavigation = new HashSet<Process>();
            ProcessWorkFlowCreatedByNavigation = new HashSet<ProcessWorkFlow>();
            ProcessWorkFlowReceiver = new HashSet<ProcessWorkFlow>();
            ProcessWorkFlowSender = new HashSet<ProcessWorkFlow>();
            ProcessWorkFlowUpdatedByNavigation = new HashSet<ProcessWorkFlow>();
            ProductCreatedByNavigation = new HashSet<Product>();
            ProductUpdatedByNavigation = new HashSet<Product>();
            ProjectCommunicationCreatedByNavigation = new HashSet<ProjectCommunication>();
            ProjectCommunicationUpdatedByNavigation = new HashSet<ProjectCommunication>();
            ProjectCreatedByNavigation = new HashSet<Project>();
            ProjectDocumentCreatedByNavigation = new HashSet<ProjectDocument>();
            ProjectDocumentUpdatedByNavigation = new HashSet<ProjectDocument>();
            ProjectFinancialReportCreatedByNavigation = new HashSet<ProjectFinancialReport>();
            ProjectFinancialReportUpdatedByNavigation = new HashSet<ProjectFinancialReport>();
            ProjectInternalSourceCreatedByNavigation = new HashSet<ProjectInternalSource>();
            ProjectInternalSourceUpdatedByNavigation = new HashSet<ProjectInternalSource>();
            ProjectInterventionReportCreatedByNavigation = new HashSet<ProjectInterventionReport>();
            ProjectInterventionReportUpdatedByNavigation = new HashSet<ProjectInterventionReport>();
            ProjectLocationCreatedByNavigation = new HashSet<ProjectLocation>();
            ProjectLocationDetailCreatedByNavigation = new HashSet<ProjectLocationDetail>();
            ProjectLocationDetailUpdatedByNavigation = new HashSet<ProjectLocationDetail>();
            ProjectLocationUpdatedByNavigation = new HashSet<ProjectLocation>();
            ProjectOtherSourceCreatedByNavigation = new HashSet<ProjectOtherSource>();
            ProjectOtherSourceUpdatedByNavigation = new HashSet<ProjectOtherSource>();
            ProjectProgramManager = new HashSet<Project>();
            ProjectReportCreatedByNavigation = new HashSet<ProjectReport>();
            ProjectReportUpdatedByNavigation = new HashSet<ProjectReport>();
            ProjectUpdatedByNavigation = new HashSet<Project>();
            RoleCreatedByNavigation = new HashSet<Role>();
            RoleUpdatedByNavigation = new HashSet<Role>();
            StartingNumberCreatedByNavigation = new HashSet<StartingNumber>();
            StartingNumberUpdatedByNavigation = new HashSet<StartingNumber>();
            StateCreatedByNavigation = new HashSet<State>();
            StateUpdatedByNavigation = new HashSet<State>();
            SubActivityCreatedByNavigation = new HashSet<SubActivity>();
            SubActivityUpdatedByNavigation = new HashSet<SubActivity>();
            SubThemeCreatedByNavigation = new HashSet<SubTheme>();
            SubThemeUpdatedByNavigation = new HashSet<SubTheme>();
            ThemeCreatedByNavigation = new HashSet<Theme>();
            ThemeUpdatedByNavigation = new HashSet<Theme>();
            UomCreatedByNavigation = new HashSet<Uom>();
            UomUpdatedByNavigation = new HashSet<Uom>();
            UserRightsCreatedByNavigation = new HashSet<UserRights>();
            UserRightsUpdatedByNavigation = new HashSet<UserRights>();
            UserRole = new HashSet<UserRole>();
            UserTypeCreatedByNavigation = new HashSet<UserType>();
            UserTypeUpdatedByNavigation = new HashSet<UserType>();
            VillageCreatedByNavigation = new HashSet<Village>();
            VillageUpdatedByNavigation = new HashSet<Village>();
        }

        [Key]
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int RoleId { get; set; }
        public int? PartnerId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(200)]
        public string Password { get; set; }
        [StringLength(10)]
        public string MobileNo { get; set; }
        [Required]
        [Column("EmailID")]
        [StringLength(50)]
        public string EmailId { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }
        public int? DefaultMenuId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public byte? WrongAttemp { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LockDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }
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

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("User")]
        public virtual Department Department { get; set; }
        [ForeignKey(nameof(DesignationId))]
        [InverseProperty("User")]
        public virtual Designation Designation { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("User")]
        public virtual Role Role { get; set; }
        [ForeignKey(nameof(UserTypeId))]
        [InverseProperty("User")]
        public virtual UserType UserType { get; set; }
        [InverseProperty(nameof(Activity.CreatedByNavigation))]
        public virtual ICollection<Activity> ActivityCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Activity.UpdatedByNavigation))]
        public virtual ICollection<Activity> ActivityUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(AuditReviewModule.CreatedByNavigation))]
        public virtual ICollection<AuditReviewModule> AuditReviewModuleCreatedByNavigation { get; set; }
        [InverseProperty(nameof(AuditReviewModule.UpdatedByNavigation))]
        public virtual ICollection<AuditReviewModule> AuditReviewModuleUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(AuditReviewParamter.CreatedByNavigation))]
        public virtual ICollection<AuditReviewParamter> AuditReviewParamterCreatedByNavigation { get; set; }
        [InverseProperty(nameof(AuditReviewParamter.UpdatedByNavigation))]
        public virtual ICollection<AuditReviewParamter> AuditReviewParamterUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Auditor.CreatedByNavigation))]
        public virtual ICollection<Auditor> AuditorCreatedByNavigation { get; set; }
        [InverseProperty(nameof(AuditorDocument.CreatedByNavigation))]
        public virtual ICollection<AuditorDocument> AuditorDocumentCreatedByNavigation { get; set; }
        [InverseProperty(nameof(AuditorDocument.UpdatedByNavigation))]
        public virtual ICollection<AuditorDocument> AuditorDocumentUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Auditor.UpdatedByNavigation))]
        public virtual ICollection<Auditor> AuditorUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Block.CreatedByNavigation))]
        public virtual ICollection<Block> BlockCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Block.UpdatedByNavigation))]
        public virtual ICollection<Block> BlockUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Customer.CreatedByNavigation))]
        public virtual ICollection<Customer> CustomerCreatedByNavigation { get; set; }
        [InverseProperty(nameof(CustomerLicenseActivation.CreatedByNavigation))]
        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivationCreatedByNavigation { get; set; }
        [InverseProperty(nameof(CustomerLicenseActivation.UpdatedByNavigation))]
        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivationUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(CustomerPayment.CreatedByNavigation))]
        public virtual ICollection<CustomerPayment> CustomerPaymentCreatedByNavigation { get; set; }
        [InverseProperty(nameof(CustomerPayment.UpdatedByNavigation))]
        public virtual ICollection<CustomerPayment> CustomerPaymentUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Customer.UpdatedByNavigation))]
        public virtual ICollection<Customer> CustomerUpdatedByNavigation { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Department> DepartmentCreatedByNavigation { get; set; }
        [InverseProperty("UpdatedByNavigation")]
        public virtual ICollection<Department> DepartmentUpdatedByNavigation { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Designation> DesignationCreatedByNavigation { get; set; }
        [InverseProperty(nameof(DesignationHistory.CreatedByNavigation))]
        public virtual ICollection<DesignationHistory> DesignationHistoryCreatedByNavigation { get; set; }
        [InverseProperty(nameof(DesignationHistory.UpdatedByNavigation))]
        public virtual ICollection<DesignationHistory> DesignationHistoryUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(DesignationHistory.User))]
        public virtual ICollection<DesignationHistory> DesignationHistoryUser { get; set; }
        [InverseProperty("UpdatedByNavigation")]
        public virtual ICollection<Designation> DesignationUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(District.CreatedByNavigation))]
        public virtual ICollection<District> DistrictCreatedByNavigation { get; set; }
        [InverseProperty(nameof(District.UpdatedByNavigation))]
        public virtual ICollection<District> DistrictUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(EmailConfiguration.CreatedByNavigation))]
        public virtual ICollection<EmailConfiguration> EmailConfigurationCreatedByNavigation { get; set; }
        [InverseProperty(nameof(EmailConfiguration.UpdatedByNavigation))]
        public virtual ICollection<EmailConfiguration> EmailConfigurationUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(FinancialAuditReport.CreatedByNavigation))]
        public virtual ICollection<FinancialAuditReport> FinancialAuditReportCreatedByNavigation { get; set; }
        [InverseProperty(nameof(FinancialAuditReport.UpdatedByNavigation))]
        public virtual ICollection<FinancialAuditReport> FinancialAuditReportUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(FundingAgency.CreatedByNavigation))]
        public virtual ICollection<FundingAgency> FundingAgencyCreatedByNavigation { get; set; }
        [InverseProperty(nameof(FundingAgency.UpdatedByNavigation))]
        public virtual ICollection<FundingAgency> FundingAgencyUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(FundingSource.CreatedByNavigation))]
        public virtual ICollection<FundingSource> FundingSourceCreatedByNavigation { get; set; }
        [InverseProperty(nameof(FundingSource.UpdatedByNavigation))]
        public virtual ICollection<FundingSource> FundingSourceUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(IndicatorResponseType.CreatedByNavigation))]
        public virtual ICollection<IndicatorResponseType> IndicatorResponseTypeCreatedByNavigation { get; set; }
        [InverseProperty(nameof(IndicatorResponseType.UpdatedByNavigation))]
        public virtual ICollection<IndicatorResponseType> IndicatorResponseTypeUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(IndicatorType.CreatedByNavigation))]
        public virtual ICollection<IndicatorType> IndicatorTypeCreatedByNavigation { get; set; }
        [InverseProperty(nameof(IndicatorType.UpdatedByNavigation))]
        public virtual ICollection<IndicatorType> IndicatorTypeUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(MailProcess.CreatedByNavigation))]
        public virtual ICollection<MailProcess> MailProcessCreatedByNavigation { get; set; }
        [InverseProperty(nameof(MailProcess.UpdatedByNavigation))]
        public virtual ICollection<MailProcess> MailProcessUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(MailSendStatus.CreatedByNavigation))]
        public virtual ICollection<MailSendStatus> MailSendStatusCreatedByNavigation { get; set; }
        [InverseProperty(nameof(MailSendStatus.UpdatedByNavigation))]
        public virtual ICollection<MailSendStatus> MailSendStatusUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(MailSubject.CreatedByNavigation))]
        public virtual ICollection<MailSubject> MailSubjectCreatedByNavigation { get; set; }
        [InverseProperty(nameof(MailSubject.UpdatedByNavigation))]
        public virtual ICollection<MailSubject> MailSubjectUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Maintenance.CreatedByNavigation))]
        public virtual ICollection<Maintenance> MaintenanceCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Maintenance.UpdatedByNavigation))]
        public virtual ICollection<Maintenance> MaintenanceUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Menu.CreatedByNavigation))]
        public virtual ICollection<Menu> MenuCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Menu.UpdatedByNavigation))]
        public virtual ICollection<Menu> MenuUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(NgoawardDetail.CreatedByNavigation))]
        public virtual ICollection<NgoawardDetail> NgoawardDetailCreatedByNavigation { get; set; }
        [InverseProperty(nameof(NgoawardDetail.UpdatedByNavigation))]
        public virtual ICollection<NgoawardDetail> NgoawardDetailUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(NgochartDocument.CreatedByNavigation))]
        public virtual ICollection<NgochartDocument> NgochartDocumentCreatedByNavigation { get; set; }
        [InverseProperty(nameof(NgochartDocument.UpdatedByNavigation))]
        public virtual ICollection<NgochartDocument> NgochartDocumentUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(NgocorpusGrantFund.CreatedByNavigation))]
        public virtual ICollection<NgocorpusGrantFund> NgocorpusGrantFundCreatedByNavigation { get; set; }
        [InverseProperty(nameof(NgocorpusGrantFund.UpdatedByNavigation))]
        public virtual ICollection<NgocorpusGrantFund> NgocorpusGrantFundUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(NgofundingPartner.CreatedByNavigation))]
        public virtual ICollection<NgofundingPartner> NgofundingPartnerCreatedByNavigation { get; set; }
        [InverseProperty(nameof(NgofundingPartner.UpdatedByNavigation))]
        public virtual ICollection<NgofundingPartner> NgofundingPartnerUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(NgokeyProjects.CreatedByNavigation))]
        public virtual ICollection<NgokeyProjects> NgokeyProjectsCreatedByNavigation { get; set; }
        [InverseProperty(nameof(NgokeyProjects.UpdatedByNavigation))]
        public virtual ICollection<NgokeyProjects> NgokeyProjectsUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Ngomember.CreatedByNavigation))]
        public virtual ICollection<Ngomember> NgomemberCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Ngomember.UpdatedByNavigation))]
        public virtual ICollection<Ngomember> NgomemberUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(NgoregistrationDetail.CreatedByNavigation))]
        public virtual ICollection<NgoregistrationDetail> NgoregistrationDetailCreatedByNavigation { get; set; }
        [InverseProperty(nameof(NgoregistrationDetail.UpdatedByNavigation))]
        public virtual ICollection<NgoregistrationDetail> NgoregistrationDetailUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(NgosaturatoryAuditorDetail.CreatedByNavigation))]
        public virtual ICollection<NgosaturatoryAuditorDetail> NgosaturatoryAuditorDetailCreatedByNavigation { get; set; }
        [InverseProperty(nameof(NgosaturatoryAuditorDetail.UpdatedByNavigation))]
        public virtual ICollection<NgosaturatoryAuditorDetail> NgosaturatoryAuditorDetailUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Partner.CreatedByNavigation))]
        public virtual ICollection<Partner> PartnerCreatedByNavigation { get; set; }
        [InverseProperty(nameof(PartnerDocument.CreatedByNavigation))]
        public virtual ICollection<PartnerDocument> PartnerDocumentCreatedByNavigation { get; set; }
        [InverseProperty(nameof(PartnerDocument.UpdatedByNavigation))]
        public virtual ICollection<PartnerDocument> PartnerDocumentUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(PartnerPolicy.CreatedByNavigation))]
        public virtual ICollection<PartnerPolicy> PartnerPolicyCreatedByNavigation { get; set; }
        [InverseProperty(nameof(PartnerPolicyModule.CreatedByNavigation))]
        public virtual ICollection<PartnerPolicyModule> PartnerPolicyModuleCreatedByNavigation { get; set; }
        [InverseProperty(nameof(PartnerPolicyModule.UpdatedByNavigation))]
        public virtual ICollection<PartnerPolicyModule> PartnerPolicyModuleUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(PartnerPolicy.UpdatedByNavigation))]
        public virtual ICollection<PartnerPolicy> PartnerPolicyUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Partner.UpdatedByNavigation))]
        public virtual ICollection<Partner> PartnerUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Plan.CreatedByNavigation))]
        public virtual ICollection<Plan> PlanCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Plan.UpdatedByNavigation))]
        public virtual ICollection<Plan> PlanUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Process.CreatedByNavigation))]
        public virtual ICollection<Process> ProcessCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProcessDocument.CreatedByNavigation))]
        public virtual ICollection<ProcessDocument> ProcessDocumentCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProcessDocument.UpdatedByNavigation))]
        public virtual ICollection<ProcessDocument> ProcessDocumentUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProcessSetup.CreatedByNavigation))]
        public virtual ICollection<ProcessSetup> ProcessSetupCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProcessSetupHistory.CreatedByNavigation))]
        public virtual ICollection<ProcessSetupHistory> ProcessSetupHistoryCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProcessSetupHistory.UpdatedbyNavigation))]
        public virtual ICollection<ProcessSetupHistory> ProcessSetupHistoryUpdatedbyNavigation { get; set; }
        [InverseProperty(nameof(ProcessSetup.UpdatedbyNavigation))]
        public virtual ICollection<ProcessSetup> ProcessSetupUpdatedbyNavigation { get; set; }
        [InverseProperty(nameof(Process.UpdatedByNavigation))]
        public virtual ICollection<Process> ProcessUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProcessWorkFlow.CreatedByNavigation))]
        public virtual ICollection<ProcessWorkFlow> ProcessWorkFlowCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProcessWorkFlow.Receiver))]
        public virtual ICollection<ProcessWorkFlow> ProcessWorkFlowReceiver { get; set; }
        [InverseProperty(nameof(ProcessWorkFlow.Sender))]
        public virtual ICollection<ProcessWorkFlow> ProcessWorkFlowSender { get; set; }
        [InverseProperty(nameof(ProcessWorkFlow.UpdatedByNavigation))]
        public virtual ICollection<ProcessWorkFlow> ProcessWorkFlowUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Product.CreatedByNavigation))]
        public virtual ICollection<Product> ProductCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Product.UpdatedByNavigation))]
        public virtual ICollection<Product> ProductUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectCommunication.CreatedByNavigation))]
        public virtual ICollection<ProjectCommunication> ProjectCommunicationCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectCommunication.UpdatedByNavigation))]
        public virtual ICollection<ProjectCommunication> ProjectCommunicationUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Project.CreatedByNavigation))]
        public virtual ICollection<Project> ProjectCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectDocument.CreatedByNavigation))]
        public virtual ICollection<ProjectDocument> ProjectDocumentCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectDocument.UpdatedByNavigation))]
        public virtual ICollection<ProjectDocument> ProjectDocumentUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectFinancialReport.CreatedByNavigation))]
        public virtual ICollection<ProjectFinancialReport> ProjectFinancialReportCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectFinancialReport.UpdatedByNavigation))]
        public virtual ICollection<ProjectFinancialReport> ProjectFinancialReportUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectInternalSource.CreatedByNavigation))]
        public virtual ICollection<ProjectInternalSource> ProjectInternalSourceCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectInternalSource.UpdatedByNavigation))]
        public virtual ICollection<ProjectInternalSource> ProjectInternalSourceUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectInterventionReport.CreatedByNavigation))]
        public virtual ICollection<ProjectInterventionReport> ProjectInterventionReportCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectInterventionReport.UpdatedByNavigation))]
        public virtual ICollection<ProjectInterventionReport> ProjectInterventionReportUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectLocation.CreatedByNavigation))]
        public virtual ICollection<ProjectLocation> ProjectLocationCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectLocationDetail.CreatedByNavigation))]
        public virtual ICollection<ProjectLocationDetail> ProjectLocationDetailCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectLocationDetail.UpdatedByNavigation))]
        public virtual ICollection<ProjectLocationDetail> ProjectLocationDetailUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectLocation.UpdatedByNavigation))]
        public virtual ICollection<ProjectLocation> ProjectLocationUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectOtherSource.CreatedByNavigation))]
        public virtual ICollection<ProjectOtherSource> ProjectOtherSourceCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectOtherSource.UpdatedByNavigation))]
        public virtual ICollection<ProjectOtherSource> ProjectOtherSourceUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Project.ProgramManager))]
        public virtual ICollection<Project> ProjectProgramManager { get; set; }
        [InverseProperty(nameof(ProjectReport.CreatedByNavigation))]
        public virtual ICollection<ProjectReport> ProjectReportCreatedByNavigation { get; set; }
        [InverseProperty(nameof(ProjectReport.UpdatedByNavigation))]
        public virtual ICollection<ProjectReport> ProjectReportUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Project.UpdatedByNavigation))]
        public virtual ICollection<Project> ProjectUpdatedByNavigation { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Role> RoleCreatedByNavigation { get; set; }
        [InverseProperty("UpdatedByNavigation")]
        public virtual ICollection<Role> RoleUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(StartingNumber.CreatedByNavigation))]
        public virtual ICollection<StartingNumber> StartingNumberCreatedByNavigation { get; set; }
        [InverseProperty(nameof(StartingNumber.UpdatedByNavigation))]
        public virtual ICollection<StartingNumber> StartingNumberUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(State.CreatedByNavigation))]
        public virtual ICollection<State> StateCreatedByNavigation { get; set; }
        [InverseProperty(nameof(State.UpdatedByNavigation))]
        public virtual ICollection<State> StateUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(SubActivity.CreatedByNavigation))]
        public virtual ICollection<SubActivity> SubActivityCreatedByNavigation { get; set; }
        [InverseProperty(nameof(SubActivity.UpdatedByNavigation))]
        public virtual ICollection<SubActivity> SubActivityUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(SubTheme.CreatedByNavigation))]
        public virtual ICollection<SubTheme> SubThemeCreatedByNavigation { get; set; }
        [InverseProperty(nameof(SubTheme.UpdatedByNavigation))]
        public virtual ICollection<SubTheme> SubThemeUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Theme.CreatedByNavigation))]
        public virtual ICollection<Theme> ThemeCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Theme.UpdatedByNavigation))]
        public virtual ICollection<Theme> ThemeUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Uom.CreatedByNavigation))]
        public virtual ICollection<Uom> UomCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Uom.UpdatedByNavigation))]
        public virtual ICollection<Uom> UomUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(UserRights.CreatedByNavigation))]
        public virtual ICollection<UserRights> UserRightsCreatedByNavigation { get; set; }
        [InverseProperty(nameof(UserRights.UpdatedByNavigation))]
        public virtual ICollection<UserRights> UserRightsUpdatedByNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRole { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<UserType> UserTypeCreatedByNavigation { get; set; }
        [InverseProperty("UpdatedByNavigation")]
        public virtual ICollection<UserType> UserTypeUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(Village.CreatedByNavigation))]
        public virtual ICollection<Village> VillageCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Village.UpdatedByNavigation))]
        public virtual ICollection<Village> VillageUpdatedByNavigation { get; set; }
    }
}
