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
        public virtual DbSet<PartnerPolicyDetails> PartnerPolicyDetails { get; set; }
        public virtual DbSet<PartnerPolicyModule> PartnerPolicyModule { get; set; }
        public virtual DbSet<PartnerPolicyModuleDetails> PartnerPolicyModuleDetails { get; set; }
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
        public virtual DbSet<UserRole> UserRole { get; set; }
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ActivityCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_CreatedRId");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_Theme");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Activity_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ActivityUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Activity_UpdatedRId");
            });

            modelBuilder.Entity<AuditReviewModule>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AuditReviewModuleCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditReviewModule_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.AuditReviewModuleCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditReviewModule_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.AuditReviewModuleUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_AuditReviewModule_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.AuditReviewModuleUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_AuditReviewModule_UpdatedRId");
            });

            modelBuilder.Entity<AuditReviewParamter>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AuditReviewParamterCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditReviewParamter_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.AuditReviewParamterCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditReviewParamter_CreatedRId");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.AuditReviewParamter)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditReviewParamter_AuditReviewModule");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.AuditReviewParamterUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_AuditReviewParamter_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.AuditReviewParamterUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_AuditReviewParamter_UpdatedRId");
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AuditorCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auditor_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.AuditorCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auditor_CreatedRId");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Auditor)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Auditor_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.AuditorUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Auditor_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.AuditorUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Auditor_UpdatedRId");
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AuditorDocumentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditorDocument_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.AuditorDocumentCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditorDocument_CreatedRId");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.AuditorDocument)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditorDocument_ProcessDocument");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.AuditorDocumentUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_AuditorDocument_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.AuditorDocumentUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_AuditorDocument_UpdatedRId");
            });

            modelBuilder.Entity<Block>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.BlockCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_User");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.BlockCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_CreatedRId");

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

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.BlockUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Block_UpdatedRId");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataBaseName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CustomerCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.CustomerCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.CustomerUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Customer_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.CustomerUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Customer_UpdatedRId");
            });

            modelBuilder.Entity<CustomerLicenseActivation>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsTrail).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CustomerLicenseActivationCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLicenseActivation_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.CustomerLicenseActivationCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLicenseActivation_CreatedRId");

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

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.CustomerLicenseActivationUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_CustomerLicenseActivation_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.CustomerLicenseActivationUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_CustomerLicenseActivation_UpdatedRId");
            });

            modelBuilder.Entity<CustomerPayment>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CustomerPaymentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPayment_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.CustomerPaymentCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPayment_CreatedRId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerPayment)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPayment_CustomerPayment");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.CustomerPaymentUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_CustomerPayment_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.CustomerPaymentUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_CustomerPayment_UpdatedRId");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DepartmentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.DepartmentCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DepartmentUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Department_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.DepartmentUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Department_UpdatedRId");
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DesignationName).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DesignationCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Designation_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.DesignationCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Designation_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DesignationUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Designation_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.DesignationUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Designation_UpdatedRId");
            });

            modelBuilder.Entity<DesignationHistory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Formdate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DesignationHistoryCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DesignationHistory_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.DesignationHistoryCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DesignationHistory_CreatedRId");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.DesignationHistory)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DesignationHistory_Designation");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DesignationHistoryUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_DesignationHistory_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.DesignationHistoryUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_DesignationHistory_UpdatedRId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DesignationHistoryUser)
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

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.DistrictCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_CreatedRId");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DistrictUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_User_District_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.DistrictUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_District_UpdatedRId");
            });

            modelBuilder.Entity<EmailConfiguration>(entity =>
            {
                entity.Property(e => e.FriendlyName).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Server).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.EmailConfigurationCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailConfiguration_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.EmailConfigurationCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailConfiguration_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.EmailConfigurationUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_EmailConfiguration_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.EmailConfigurationUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_EmailConfiguration_UpdatedRId");
            });

            modelBuilder.Entity<FinancialAuditReport>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.FinancialAuditReportCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FinancialAuditReport_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.FinancialAuditReportCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FinancialAuditReport_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.FinancialAuditReportUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_FinancialAuditReport_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.FinancialAuditReportUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_FinancialAuditReport_UpdatedRId");
            });

            modelBuilder.Entity<FundingAgency>(entity =>
            {
                entity.Property(e => e.AgencyName).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.FundingAgencyCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingAgency_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.FundingAgencyCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingAgency_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.FundingAgencyUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_FundingAgency_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.FundingAgencyUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_FundingAgency_UpdatedRId");
            });

            modelBuilder.Entity<FundingSource>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.FundingSourceCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingSource_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.FundingSourceCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingSource_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.FundingSourceUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_FundingSource_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.FundingSourceUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_FundingSource_UpdatedRId");
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

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.IndicatorCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Indicator_CreatedRId");

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

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.IndicatorUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Indicator_UpdatedRId");
            });

            modelBuilder.Entity<IndicatorResponseType>(entity =>
            {
                entity.Property(e => e.ResponseTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ResponseType).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.IndicatorResponseTypeCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IndicatorResponseType_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.IndicatorResponseTypeCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IndicatorResponseType_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.IndicatorResponseTypeUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_IndicatorResponseType_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.IndicatorResponseTypeUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_IndicatorResponseType_UpdatedRId");
            });

            modelBuilder.Entity<IndicatorType>(entity =>
            {
                entity.Property(e => e.IndicatorTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IndicatorType1).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.IndicatorTypeCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IndicatorType_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.IndicatorTypeCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IndicatorType_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.IndicatorTypeUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_IndicatorType_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.IndicatorTypeUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_IndicatorType_UpdatedRId");
            });

            modelBuilder.Entity<MailProcess>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MailProcessCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailProcess_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.MailProcessCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailProcess_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MailProcessUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_MailProcess_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.MailProcessUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_MailProcess_UpdatedRId");
            });

            modelBuilder.Entity<MailSendStatus>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MailSendStatusCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailSendStatus_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.MailSendStatusCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailSendStatus_CreatedRId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.MailSendStatus)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_MailSendStatus_Customer");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.MailSendStatus)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_MailSendStatus_MailSubject");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MailSendStatusUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_MailSendStatus_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.MailSendStatusUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_MailSendStatus_UpdatedRId");
            });

            modelBuilder.Entity<MailSubject>(entity =>
            {
                entity.Property(e => e.ContactUs).IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HeaderImage).IsUnicode(false);

                entity.Property(e => e.Signature).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MailSubjectCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailSubject_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.MailSubjectCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailSubject_CreatedRId");

                entity.HasOne(d => d.MailProcess)
                    .WithMany(p => p.MailSubject)
                    .HasForeignKey(d => d.MailProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MailSubject__MailP__15A53433");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MailSubjectUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_MailSubject_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.MailSubjectUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_MailSubject_UpdatedRId");
            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StartDateTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MaintenanceCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Maintenance_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.MaintenanceCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Maintenance_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MaintenanceUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Maintenance_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.MaintenanceUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Maintenance_UpdatedRId");
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MenuCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.MenuCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MenuUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Menu_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.MenuUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Menu_UpdatedRId");
            });

            modelBuilder.Entity<NgoawardDetail>(entity =>
            {
                entity.Property(e => e.Award).IsUnicode(false);

                entity.Property(e => e.AwardConferrer).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NgoawardDetailCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOAwardDetail_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.NgoawardDetailCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOAwardDetail_CreatedRId");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgoawardDetail)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOAwardDetail_Partner");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.NgoawardDetailUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_NGOAwardDetail_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.NgoawardDetailUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_NGOAwardDetail_UpdatedRId");
            });

            modelBuilder.Entity<NgochartDocument>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NgochartDocumentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOChartDocument_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.NgochartDocumentCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOChartDocument_CreatedRId");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgochartDocument)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOChartDoccument_Partner");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.NgochartDocumentUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_NGOChartDocument_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.NgochartDocumentUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_NGOChartDocument_UpdatedRId");
            });

            modelBuilder.Entity<NgocorpusGrantFund>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NgocorpusGrantFundCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOCorpusGrantFund_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.NgocorpusGrantFundCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOCorpusGrantFund_CreatedRId");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgocorpusGrantFund)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOCorpusGrantFund_Partner");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.NgocorpusGrantFundUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_NGOCorpusGrantFund_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.NgocorpusGrantFundUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_NGOCorpusGrantFund_UpdatedRId");
            });

            modelBuilder.Entity<NgofundingPartner>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgofundingPartnerName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NgofundingPartnerCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOFundingPartner_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.NgofundingPartnerCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOFundingPartner_CreatedRId");

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

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.NgofundingPartnerUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_NGOFundingPartner_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.NgofundingPartnerUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_NGOFundingPartner_UpdatedRId");
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NgokeyProjectsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOKeyProjects_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.NgokeyProjectsCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOKeyProjects_CreatedRId");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgokeyProjects)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOKeyProjects_Partner");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.NgokeyProjectsUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_NGOKeyProjects_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.NgokeyProjectsUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_NGOKeyProjects_UpdatedRId");
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NgomemberCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOMember_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.NgomemberCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOMember_CreatedRId");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Ngomember)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOMember_Partner");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.NgomemberUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_NGOMember_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.NgomemberUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_NGOMember_UpdatedRId");
            });

            modelBuilder.Entity<NgoregistrationDetail>(entity =>
            {
                entity.Property(e => e.ApprovalNo80G).IsUnicode(false);

                entity.Property(e => e.Pannumber).IsUnicode(false);

                entity.Property(e => e.RegNo).IsUnicode(false);

                entity.Property(e => e.RegNoCertificate).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NgoregistrationDetailCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGORegistrationDetail_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.NgoregistrationDetailCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGORegistrationDetail_CreatedRId");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgoregistrationDetail)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGORegistrationDetail_Partner");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.NgoregistrationDetailUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_NGORegistrationDetail_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.NgoregistrationDetailUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_NGORegistrationDetail_UpdatedRId");
            });

            modelBuilder.Entity<NgosaturatoryAuditorDetail>(entity =>
            {
                entity.Property(e => e.AuditingFirm).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NgosaturatoryAuditorDetailCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOSaturatoryAuditorDetail_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.NgosaturatoryAuditorDetailCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOSaturatoryAuditorDetail_CreatedRId");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.NgosaturatoryAuditorDetail)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGOSaturatoryAuditorDetail_Partner");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.NgosaturatoryAuditorDetailUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_NGOSaturatoryAuditorDetail_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.NgosaturatoryAuditorDetailUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_NGOSaturatoryAuditorDetail_UpdatedRId");
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PartnerCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.PartnerCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PartnerUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Partner_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.PartnerUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Partner_UpdatedRId");
            });

            modelBuilder.Entity<PartnerDocument>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.ServerDocumentName).IsUnicode(false);

                entity.Property(e => e.UploadedDocumentName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PartnerDocumentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartnerDocument_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.PartnerDocumentCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartnerDocument_CreatedRId");

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

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PartnerDocumentUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_PartnerDocument_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.PartnerDocumentUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_PartnerDocument_UpdatedRId");
            });

            modelBuilder.Entity<PartnerPolicy>(entity =>
            {
                entity.HasKey(e => e.PolicyId)
                    .HasName("PK_PartnerPolicy_PolicyId");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PolicyName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PartnerPolicyCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartnerPolicy_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.PartnerPolicyCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartnerPolicy_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PartnerPolicyUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_PartnerPolicy_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.PartnerPolicyUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_PartnerPolicy_UpdatedRId");
            });

            modelBuilder.Entity<PartnerPolicyDetails>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PartnerPolicyModule>(entity =>
            {
                entity.HasKey(e => e.PolicyModuleId)
                    .HasName("PK_PartnerPolicyModule_PolicyId");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PolicyModuleName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PartnerPolicyModuleCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartnerPolicyModule_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.PartnerPolicyModuleCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartnerPolicyModule_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PartnerPolicyModuleUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_PartnerPolicyModule_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.PartnerPolicyModuleUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_PartnerPolicyModule_UpdatedRId");
            });

            modelBuilder.Entity<PartnerPolicyModuleDetails>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PlanCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Plan_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.PlanCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Plan_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PlanUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Plan_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.PlanUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Plan_UpdatedRId");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProcessName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProcessCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Process_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProcessCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Process_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProcessUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Process_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProcessUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Process_UpdatedRId");
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

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProcessDocumentCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessDocument_CreatedRId");

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

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProcessDocumentUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProcessDocument_UpdatedRId");
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

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProcessSetupCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessSetup_CreatedRId");

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

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProcessSetupUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProcessSetup_UpdatedRId");

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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProcessSetupHistoryCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessSetupHistory_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProcessSetupHistoryCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessSetupHistory_CreatedRId");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProcessSetupHistoryUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProcessSetupHistory_UpdatedRId");

                entity.HasOne(d => d.UpdatedbyNavigation)
                    .WithMany(p => p.ProcessSetupHistoryUpdatedbyNavigation)
                    .HasForeignKey(d => d.Updatedby)
                    .HasConstraintName("FK_ProcessSetupHistory_UpdatedBy");
            });

            modelBuilder.Entity<ProcessWorkFlow>(entity =>
            {
                entity.HasKey(e => e.WorkflowId)
                    .HasName("PK_tbl_WorkFlowDetail");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Purpose).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProcessWorkFlowCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessWorkFlow_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProcessWorkFlowCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessWorkFlow_CreatedRId");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ProcessWorkFlowReceiver)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK_ProcessWorkFlow_User1");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ProcessWorkFlowSender)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK_ProcessWorkFlow_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProcessWorkFlowUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProcessWorkFlow_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProcessWorkFlowUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProcessWorkFlow_UpdatedRId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProductCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProductCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProductUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Product_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProductUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Product_UpdatedRId");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ExtendComments).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ProjectName).IsUnicode(false);

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_CreatedRId");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Partner");

                entity.HasOne(d => d.ProgramManager)
                    .WithMany(p => p.ProjectProgramManager)
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

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Project_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Project_UpdatedRId");
            });

            modelBuilder.Entity<ProjectCommunication>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectCommunicationCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCommunication_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectCommunicationCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCommunication_CreatedRId");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectCommunication)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCommunication_Project");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectCommunicationUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProjectCommunication_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectCommunicationUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProjectCommunication_UpdatedRId");
            });

            modelBuilder.Entity<ProjectDocument>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectDocumentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectDocument_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectDocumentCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectDocument_CreatedRId");

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

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectDocumentUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProjectDocument_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectDocumentUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProjectDocument_UpdatedRId");
            });

            modelBuilder.Entity<ProjectFinancialReport>(entity =>
            {
                entity.Property(e => e.AcceptanceRemark).IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProjectYear).IsUnicode(false);

                entity.Property(e => e.ReportName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectFinancialReportCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectFinancialReport_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectFinancialReportCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectFinancialReport_CreatedRId");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectFinancialReport)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectFinancialReport_Project");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectFinancialReportUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProjectFinancialReport_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectFinancialReportUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProjectFinancialReport_UpdatedRId");
            });

            modelBuilder.Entity<ProjectInternalSource>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RevisionNo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectInternalSourceCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectInternalSource_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectInternalSourceCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectInternalSource_CreatedRId");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectInternalSource)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectInternalSource_Project");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectInternalSourceUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProjectInternalSource_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectInternalSourceUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProjectInternalSource_UpdatedRId");
            });

            modelBuilder.Entity<ProjectInterventionReport>(entity =>
            {
                entity.Property(e => e.AcceptanceRemark).IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProjectYear).IsUnicode(false);

                entity.Property(e => e.ReportName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectInterventionReportCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectInterventionReport_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectInterventionReportCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectInterventionReport_CreatedRId");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectInterventionReport)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectInterventionReport_Project");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectInterventionReportUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProjectInterventionReport_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectInterventionReportUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProjectInterventionReport_UpdatedRId");
            });

            modelBuilder.Entity<ProjectLocation>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectLocationCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocation_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectLocationCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocation_CreatedRId");

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

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectLocationUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProjectLocation_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectLocationUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProjectLocation_UpdatedRId");
            });

            modelBuilder.Entity<ProjectLocationDetail>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.ProjectLocationDetail)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("FK_ProjectLocationDetail_Block");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectLocationDetailCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocationDetail_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectLocationDetailCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectLocationDetail_CreatedRId");

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

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectLocationDetailUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProjectLocationDetail_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectLocationDetailUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProjectLocationDetail_UpdatedRId");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.ProjectLocationDetail)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK_ProjectLocationDetail_Village");
            });

            modelBuilder.Entity<ProjectOtherSource>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RevisionNo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectOtherSourceCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectOtherSource_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectOtherSourceCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectOtherSource_CreatedRId");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectOtherSource)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectOtherSource_Project");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectOtherSourceUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProjectOtherSource_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectOtherSourceUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProjectOtherSource_UpdatedRId");
            });

            modelBuilder.Entity<ProjectReport>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReportName).IsUnicode(false);

                entity.Property(e => e.YearName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectReportCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectReport_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ProjectReportCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectReport_CreatedRId");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectReport)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectReport_Project");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectReportUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ProjectReport_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ProjectReportUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_ProjectReport_UpdatedRId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RoleCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_CreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.RoleUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Role_UpdatedBy");
            });

            modelBuilder.Entity<StartingNumber>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.StartingNumberCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StartingNumber_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.StartingNumberCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StartingNumber_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.StartingNumberUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_StartingNumber_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.StartingNumberUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_StartingNumber_UpdatedRId");
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

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.StateCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_State_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.StateUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_User_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.StateUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_State_UpdatedRId");
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SubActivityCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubActivity_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.SubActivityCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubActivity_CreatedRId");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.SubActivity)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubActivity_Theme");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.SubActivityUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_SubActivity_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.SubActivityUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_SubActivity_UpdatedRId");
            });

            modelBuilder.Entity<SubTheme>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.SubThemeCode).IsUnicode(false);

                entity.Property(e => e.SubThemeName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SubThemeCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubTheme_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.SubThemeCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubTheme_CreatedRId");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.SubTheme)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubTheme_Theme");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.SubThemeUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_SubTheme_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.SubThemeUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_SubTheme_UpdatedRId");
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ThemeCode).IsUnicode(false);

                entity.Property(e => e.ThemeName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ThemeCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Theme_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.ThemeCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Theme_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ThemeUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Theme_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.ThemeUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Theme_UpdatedRId");
            });

            modelBuilder.Entity<Uom>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UomCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UOM_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.UomCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UOM_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.UomUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UOM_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.UomUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_UOM_UpdatedRId");
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserRightsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.UserRightsCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_CreatedRId");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.UserRights)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRightsRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_Role");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.UserRightsUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UserRights_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.UserRightsUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_UserRights_UpdatedRId");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserTypeCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserType_CreatedBy");

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.UserTypeCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserType_CreatedRId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.UserTypeUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UserType_UpdatedBy");

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.UserTypeUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_UserType_UpdatedRId");
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

                entity.HasOne(d => d.CreatedR)
                    .WithMany(p => p.VillageCreatedR)
                    .HasForeignKey(d => d.CreatedRid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Village_CreatedRId");

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

                entity.HasOne(d => d.UpdatedR)
                    .WithMany(p => p.VillageUpdatedR)
                    .HasForeignKey(d => d.UpdatedRid)
                    .HasConstraintName("FK_Village_UpdatedRId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
