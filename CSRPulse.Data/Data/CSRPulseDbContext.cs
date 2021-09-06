using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CSRPulse.Data.Models;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Data
{
    public partial class CSRPulseDbContext : DbContext
    {
        public CSRPulseDbContext()
        {
        }

        public CSRPulseDbContext(DbContextOptions<CSRPulseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<AuditReviewModule> AuditReviewModule { get; set; }
        public virtual DbSet<AuditReviewParamter> AuditReviewParamter { get; set; }
        public virtual DbSet<Auditor> Auditor { get; set; }
        public virtual DbSet<AuditorDocument> AuditorDocument { get; set; }
        public virtual DbSet<Block> Block { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerLicenseActivation> CustomerLicenseActivation { get; set; }
        public virtual DbSet<CustomerPayment> CustomerPayment { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<DesignationHistory> DesignationHistory { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<EmailConfiguration> EmailConfiguration { get; set; }
        public virtual DbSet<FinancialAuditReport> FinancialAuditReport { get; set; }
        public virtual DbSet<FundingAgency> FundingAgency { get; set; }
        public virtual DbSet<FundingSource> FundingSource { get; set; }
        public virtual DbSet<Indicator> Indicator { get; set; }
        public virtual DbSet<IndicatorResponseType> IndicatorResponseType { get; set; }
        public virtual DbSet<IndicatorType> IndicatorType { get; set; }
        public virtual DbSet<MailProcess> MailProcess { get; set; }
        public virtual DbSet<MailSendStatus> MailSendStatus { get; set; }
        public virtual DbSet<MailSubject> MailSubject { get; set; }
        public virtual DbSet<Maintenance> Maintenance { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<NgoawardDetail> NgoawardDetail { get; set; }
        public virtual DbSet<NgochartDocument> NgochartDocument { get; set; }
        public virtual DbSet<NgocorpusGrantFund> NgocorpusGrantFund { get; set; }
        public virtual DbSet<NgofundingPartner> NgofundingPartner { get; set; }
        public virtual DbSet<NgokeyProjects> NgokeyProjects { get; set; }
        public virtual DbSet<Ngomember> Ngomember { get; set; }
        public virtual DbSet<NgoregistrationDetail> NgoregistrationDetail { get; set; }
        public virtual DbSet<NgosaturatoryAuditorDetail> NgosaturatoryAuditorDetail { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<PartnerDocument> PartnerDocument { get; set; }
        public virtual DbSet<PartnerPolicy> PartnerPolicy { get; set; }
        public virtual DbSet<PartnerPolicyModule> PartnerPolicyModule { get; set; }
        public virtual DbSet<Plan> Plan { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<ProcessDocument> ProcessDocument { get; set; }
        public virtual DbSet<ProcessSetup> ProcessSetup { get; set; }
        public virtual DbSet<ProcessSetupHistory> ProcessSetupHistory { get; set; }
        public virtual DbSet<ProcessWorkFlow> ProcessWorkFlow { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectCommunication> ProjectCommunication { get; set; }
        public virtual DbSet<ProjectDocument> ProjectDocument { get; set; }
        public virtual DbSet<ProjectFinancialReport> ProjectFinancialReport { get; set; }
        public virtual DbSet<ProjectInternalSource> ProjectInternalSource { get; set; }
        public virtual DbSet<ProjectInterventionReport> ProjectInterventionReport { get; set; }
        public virtual DbSet<ProjectLocation> ProjectLocation { get; set; }
        public virtual DbSet<ProjectLocationDetail> ProjectLocationDetail { get; set; }
        public virtual DbSet<ProjectOtherSource> ProjectOtherSource { get; set; }
        public virtual DbSet<ProjectReport> ProjectReport { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<StartingNumber> StartingNumber { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<SubActivity> SubActivity { get; set; }
        public virtual DbSet<SubTheme> SubTheme { get; set; }
        public virtual DbSet<Theme> Theme { get; set; }
        public virtual DbSet<Uom> Uom { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRights> UserRights { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Village> Village { get; set; }

        public static string CustomeDataBase { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            if (!string.IsNullOrEmpty(CustomeDataBase))
            {
                var _Connection = configuration.GetConnectionString("DefaultConnection");
                var _customConnection = _Connection.Replace("CSRPulse", CustomeDataBase);

                optionsBuilder.UseSqlServer(_customConnection);
            }
            else
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityCode).IsUnicode(false);

                entity.Property(e => e.ActivityName).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_Theme");
            });

            modelBuilder.Entity<AuditReviewModule>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AuditReviewParamter>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.AuditReviewParamter)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditReviewParamter_AuditReviewModule");
            });

            modelBuilder.Entity<Auditor>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.AuditOrganization).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Gstno).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Pan).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.Website).IsUnicode(false);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Auditor)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Auditor_State");
            });

            modelBuilder.Entity<AuditorDocument>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.Sdname).IsUnicode(false);

                entity.Property(e => e.Udname).IsUnicode(false);

                entity.HasOne(d => d.Auditor)
                    .WithMany(p => p.AuditorDocument)
                    .HasForeignKey(d => d.AuditorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditorDocument_Auditor");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.AuditorDocument)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditorDocument_ProcessDocument");
            });

            modelBuilder.Entity<Block>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.BlockCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_User");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Block)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_District");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Block)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.BlockUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Block_User1");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataBaseName).IsUnicode(false);
            });

            modelBuilder.Entity<CustomerLicenseActivation>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsTrail).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerLicenseActivation)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLicenseActivation_Customer");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.CustomerLicenseActivation)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLicenseActivation_CustomerPayment");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.CustomerLicenseActivation)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLicenseActivation_Plan");
            });

            modelBuilder.Entity<CustomerPayment>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerPayment)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPayment_CustomerPayment");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentName).IsUnicode(false);
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DesignationName).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<DesignationHistory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Formdate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.DesignationHistory)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DesignationHistory_Designation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DesignationHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DesignationHistory_User");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DistrictCode).IsUnicode(false);

                entity.Property(e => e.DistrictShort).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DistrictCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_User_District_CreatedBy");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DistrictUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_User_District_UpdatedBy");
            });

            modelBuilder.Entity<EmailConfiguration>(entity =>
            {
                entity.Property(e => e.Bcc).IsUnicode(false);

                entity.Property(e => e.CcEmail).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Server).IsUnicode(false);

                entity.Property(e => e.Signature).IsUnicode(false);

                entity.Property(e => e.ToEmail).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.EmailConfigurationCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailConfiguration_CreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.EmailConfigurationUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_EmailConfiguration_UpdatedBy");
            });

            modelBuilder.Entity<FundingAgency>(entity =>
            {
                entity.Property(e => e.AgencyName).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<FundingSource>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Indicator>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IndicatorCode).IsUnicode(false);

                entity.Property(e => e.IndicatorName).IsUnicode(false);

                entity.Property(e => e.IndicatorShortName).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Indicator)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK_Indicator_Activity");

                entity.HasOne(d => d.SubActivity)
                    .WithMany(p => p.Indicator)
                    .HasForeignKey(d => d.SubActivityId)
                    .HasConstraintName("FK_Indicator_SubActivity");

                entity.HasOne(d => d.SubTheme)
                    .WithMany(p => p.Indicator)
                    .HasForeignKey(d => d.SubThemeId)
                    .HasConstraintName("FK_Indicator_SubTheme");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Indicator)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Indicator_Theme");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.Indicator)
                    .HasForeignKey(d => d.Uomid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Indicator_UOM");
            });

            modelBuilder.Entity<IndicatorResponseType>(entity =>
            {
                entity.Property(e => e.ResponseTypeId).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ResponseType).IsUnicode(false);
            });

            modelBuilder.Entity<IndicatorType>(entity =>
            {
                entity.Property(e => e.IndicatorTypeId).ValueGeneratedNever();

                entity.Property(e => e.IndicatorType1).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MailSendStatus>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.MailSendStatus)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_MailSendStatus_Customer");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.MailSendStatus)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_MailSendStatus_MailSubject");
            });

            modelBuilder.Entity<MailSubject>(entity =>
            {
                entity.HasOne(d => d.MailProcess)
                    .WithMany(p => p.MailSubject)
                    .HasForeignKey(d => d.MailProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MailSubject__MailP__15A53433");
            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.Property(e => e.StartDateTime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Area).IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IconClass).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuName).IsUnicode(false);

                entity.Property(e => e.Url).IsUnicode(false);
            });

            modelBuilder.Entity<NgoawardDetail>(entity =>
            {
                entity.Property(e => e.Award).IsUnicode(false);

                entity.Property(e => e.AwardConferrer).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgoawardDetail)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOAwardDetail_Partner");
            });

            modelBuilder.Entity<NgochartDocument>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgochartDocument)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOChartDoccument_Partner");
            });

            modelBuilder.Entity<NgocorpusGrantFund>(entity =>
            {
                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgocorpusGrantFund)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOCorpusGrantFund_Partner");
            });

            modelBuilder.Entity<NgofundingPartner>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgofundingPartnerName).IsUnicode(false);

                entity.HasOne(d => d.FundingAgency)
                    .WithMany(p => p.NgofundingPartner)
                    .HasForeignKey(d => d.FundingAgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOFundingPartner_FundingAgency");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgofundingPartner)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOFundingPartner_Partner");
            });

            modelBuilder.Entity<NgokeyProjects>(entity =>
            {
                entity.Property(e => e.AdditionalInformation).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DonorAgency).IsUnicode(false);

                entity.Property(e => e.ProjectEnd).IsUnicode(false);

                entity.Property(e => e.ProjectLocation).IsUnicode(false);

                entity.Property(e => e.ProjectObjective).IsUnicode(false);

                entity.Property(e => e.ProjectStart).IsUnicode(false);

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgokeyProjects)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOKeyProjects_Partner");
            });

            modelBuilder.Entity<Ngomember>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.AffiliatedOrganisation).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.CrimeDescription).IsUnicode(false);

                entity.Property(e => e.Designation).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Experience).IsUnicode(false);

                entity.Property(e => e.MemberRelatedtoAbfdetail).IsUnicode(false);

                entity.Property(e => e.MemberWillfulDefaulterDetail).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Panno).IsUnicode(false);

                entity.Property(e => e.PassportNo).IsUnicode(false);

                entity.Property(e => e.Qualification).IsUnicode(false);

                entity.Property(e => e.VoterId).IsUnicode(false);

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Ngomember)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOMember_Partner");
            });

            modelBuilder.Entity<NgoregistrationDetail>(entity =>
            {
                entity.Property(e => e.ApprovalNo80G).IsUnicode(false);

                entity.Property(e => e.Pannumber).IsUnicode(false);

                entity.Property(e => e.RegNo).IsUnicode(false);

                entity.Property(e => e.RegNoCertificate).IsUnicode(false);

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgoregistrationDetail)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGORegistrationDetail_Partner");
            });

            modelBuilder.Entity<NgosaturatoryAuditorDetail>(entity =>
            {
                entity.Property(e => e.AuditingFirm).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgosaturatoryAuditorDetail)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOSaturatoryAuditorDetail_Partner");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.Property(e => e.ComCity).IsUnicode(false);

                entity.Property(e => e.CommAddress).IsUnicode(false);

                entity.Property(e => e.CommMobile).IsUnicode(false);

                entity.Property(e => e.CommPhone).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.PartnerName).IsUnicode(false);

                entity.Property(e => e.RegAddress).IsUnicode(false);

                entity.Property(e => e.RegCity).IsUnicode(false);

                entity.Property(e => e.RegMobile).IsUnicode(false);

                entity.Property(e => e.RegPhone).IsUnicode(false);

                entity.Property(e => e.ReligiousPoliticalObjectives).IsUnicode(false);

                entity.Property(e => e.Website).IsUnicode(false);
            });

            modelBuilder.Entity<PartnerDocument>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.ServerDocumentName).IsUnicode(false);

                entity.Property(e => e.UploadedDocumentName).IsUnicode(false);

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.PartnerDocument)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartnerDocument_ProcessDocument");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.PartnerDocument)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartnerDocument_Partner");
            });

            modelBuilder.Entity<PartnerPolicy>(entity =>
            {
                entity.HasKey(e => e.PolicyId)
                    .HasName("PK_PartnerPolicy_PolicyId");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PolicyName).IsUnicode(false);
            });

            modelBuilder.Entity<PartnerPolicyModule>(entity =>
            {
                entity.HasKey(e => e.PolicyModuleId)
                    .HasName("PK_PartnerPolicyModule_PolicyId");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PolicyModuleName).IsUnicode(false);
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.Property(e => e.ProcessName).IsUnicode(false);
            });

            modelBuilder.Entity<ProcessDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentId)
                    .HasName("PK__ProcessD__1ABEEF0F13141462");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DocumentName).IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProcessDocumentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProcessDo__Creat__3DD3211E");

                entity.HasOne(d => d.ParentDocument)
                    .WithMany(p => p.InverseParentDocument)
                    .HasForeignKey(d => d.ParentDocumentId)
                    .HasConstraintName("FK_ProcessDocument_ProcessDocument");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessDocument)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProcessDo__Proce__3EC74557");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProcessDocumentUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK__ProcessDo__Updat__3FBB6990");
            });

            modelBuilder.Entity<ProcessSetup>(entity =>
            {
                entity.HasKey(e => e.SetupId)
                    .HasName("PK_Level");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProcessSetupCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessSetup_User");

                entity.HasOne(d => d.PrimaryRole)
                    .WithMany(p => p.ProcessSetupPrimaryRole)
                    .HasForeignKey(d => d.PrimaryRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessSetup_Role");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessSetup)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessSetup_Process");

                entity.HasOne(d => d.QuaternaryRole)
                    .WithMany(p => p.ProcessSetupQuaternaryRole)
                    .HasForeignKey(d => d.QuaternaryRoleId)
                    .HasConstraintName("FK_ProcessSetup_Role3");

                entity.HasOne(d => d.SecondoryRole)
                    .WithMany(p => p.ProcessSetupSecondoryRole)
                    .HasForeignKey(d => d.SecondoryRoleId)
                    .HasConstraintName("FK_ProcessSetup_Role1");

                entity.HasOne(d => d.TertiaryRole)
                    .WithMany(p => p.ProcessSetupTertiaryRole)
                    .HasForeignKey(d => d.TertiaryRoleId)
                    .HasConstraintName("FK_ProcessSetup_Role2");

                entity.HasOne(d => d.UpdatedbyNavigation)
                    .WithMany(p => p.ProcessSetupUpdatedbyNavigation)
                    .HasForeignKey(d => d.Updatedby)
                    .HasConstraintName("FK_ProcessSetup_User1");
            });

            modelBuilder.Entity<ProcessSetupHistory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Flag).IsUnicode(false);

                entity.Property(e => e.Skip).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ProcessWorkFlow>(entity =>
            {
                entity.HasKey(e => e.WorkflowId)
                    .HasName("PK_tbl_WorkFlowDetail");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Purpose).IsUnicode(false);

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ProcessWorkFlowReceiver)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK_ProcessWorkFlow_User1");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ProcessWorkFlowSender)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK_ProcessWorkFlow_User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ExtendComments).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ProjectName).IsUnicode(false);

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Partner");

                entity.HasOne(d => d.ProgramManager)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.ProgramManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_User");

                entity.HasOne(d => d.SubTheme)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.SubThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_SubTheme");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Theme");
            });

            modelBuilder.Entity<ProjectCommunication>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectCommunication)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCommunication_Project");
            });

            modelBuilder.Entity<ProjectDocument>(entity =>
            {
                entity.HasOne(d => d.Document)
                    .WithMany(p => p.ProjectDocument)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectDocument_ProcessDocument");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectDocument)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectDocument_Project");
            });

            modelBuilder.Entity<ProjectFinancialReport>(entity =>
            {
                entity.Property(e => e.AcceptanceRemark).IsUnicode(false);

                entity.Property(e => e.ProjectYear).IsUnicode(false);

                entity.Property(e => e.ReportName).IsUnicode(false);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectFinancialReport)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectFinancialReport_Project");
            });

            modelBuilder.Entity<ProjectInternalSource>(entity =>
            {
                entity.Property(e => e.RevisionNo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectInternalSource)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectInternalSource_Project");
            });

            modelBuilder.Entity<ProjectInterventionReport>(entity =>
            {
                entity.Property(e => e.AcceptanceRemark).IsUnicode(false);

                entity.Property(e => e.ProjectYear).IsUnicode(false);

                entity.Property(e => e.ReportName).IsUnicode(false);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectInterventionReport)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectInterventionReport_Project");
            });

            modelBuilder.Entity<ProjectLocation>(entity =>
            {
                entity.HasOne(d => d.District)
                    .WithMany(p => p.ProjectLocation)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocation_District");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectLocation)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocation_Project");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.ProjectLocation)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocation_State");
            });

            modelBuilder.Entity<ProjectLocationDetail>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.ProjectLocationDetail)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("FK_ProjectLocationDetail_Block");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.ProjectLocationDetail)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocationDetail_District");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectLocationDetail)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocationDetail_Project");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.ProjectLocationDetail)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocationDetail_State");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.ProjectLocationDetail)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK_ProjectLocationDetail_Village");
            });

            modelBuilder.Entity<ProjectOtherSource>(entity =>
            {
                entity.Property(e => e.RevisionNo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectOtherSource)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectOtherSource_Project");
            });

            modelBuilder.Entity<ProjectReport>(entity =>
            {
                entity.Property(e => e.ReportName).IsUnicode(false);

                entity.Property(e => e.YearName).IsUnicode(false);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectReport)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectReport_Project");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StateCode).IsUnicode(false);

                entity.Property(e => e.StateShort).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.StateCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_User_CreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.StateUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_User_UpdatedBy");
            });

            modelBuilder.Entity<SubActivity>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.SubActivityCode).IsUnicode(false);

                entity.Property(e => e.SubActivityName).IsUnicode(false);

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.SubActivity)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubActivity_Activity");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.SubActivity)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubActivity_Theme");
            });

            modelBuilder.Entity<SubTheme>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.SubThemeCode).IsUnicode(false);

                entity.Property(e => e.SubThemeName).IsUnicode(false);

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.SubTheme)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubTheme_Theme");
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ThemeCode).IsUnicode(false);

                entity.Property(e => e.ThemeName).IsUnicode(false);
            });

            modelBuilder.Entity<Uom>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitName).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId).IsUnicode(false);

                entity.Property(e => e.FullName).IsUnicode(false);

                entity.Property(e => e.ImageName).IsUnicode(false);

                entity.Property(e => e.MobileNo).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Department");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Designation");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UserRights>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.UserRights)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_Menu");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRights)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_User");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => e.UserRoleId)
                    .HasName("PK_UserRole");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Village>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.Village)
                    .HasForeignKey(d => d.BlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Village_Block");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VillageCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Village_User");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Village)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Village_District");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Village)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Village_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.VillageUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Village_User1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
