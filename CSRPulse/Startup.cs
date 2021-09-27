using AutoMapper;
using CSRPulse.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using System;
using Microsoft.AspNetCore.Http;
using CSRPulse.Model;
using DNTCaptcha.Core;
using CSRPulse.ExportImport.Interfaces;
using CSRPulse.ExportImport;
using CSRPulse.IServices;
using System.Globalization;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace CSRPulse
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<CSRPulseDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), mig => mig.MigrationsAssembly("CSRPulse.Data")));

            //services.AddControllersWithViews(options =>
            //{
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //});
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddHttpContextAccessor();

            services.AddMemoryCache();

            services.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(30); });


            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option =>
            {
                option.HtmlHelperOptions.ClientValidationEnabled = true;
            });

            services.AddDNTCaptcha(options => options.UseCookieStorageProvider()
                     .ShowThousandsSeparators(false));

            services.Configure<SMTPConfig>(Configuration.GetSection("MailSettings"));
           

            #region R E G I S T E R  C O M P O N E N T  F O R  D I 

            services.AddAutoMapper(typeof(AutoMapperServices));
            services.AddScoped<IExport, Export>();
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDepartmentServices, DepartmentServices>();
            services.AddScoped<IDesignationServices, DesignationServices>();
            services.AddScoped<IDesignationHistoryService, DesignationHistoryService>();
            services.AddScoped<IDesignationHistoryRepository, DesignationHistoryRepository>();

            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IProcessServices, ProcessServices>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDropdownBindService, DropdownBindService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPrepareDBForCustomer, PrepareBDForCustomer>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<IQuickEmailService, QuickEmailService>();
            services.AddScoped<IRoleServices, RoleServices>();

            services.AddScoped<IUOMService, UOMService>();
            services.AddScoped<IThemeService, ThemeService>();
            services.AddScoped<ISubThemeService, SubThemeService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<ISubActivityService, SubActivityService>();
            services.AddScoped<IIndicatorService, IndicatorService>();

            services.AddScoped<IStateServices, StateServices>();
            services.AddScoped<IDistrictServices, DistrictServices>();
            services.AddScoped<IBlockServices, BlockServices>();
            services.AddScoped<IVillageServices, VillageServices>();
            services.Configure<SMTPConfig>(Configuration.GetSection("SMTPConfig"));

            services.AddScoped<IImport, Import>();
            services.AddScoped<IDistrictServices, DistrictServices>();
            services.AddScoped<IDistrictImportRepository, DistrictImportRepository>();
            services.AddScoped<IBlockServices, BlockServices>();
            services.AddScoped<IBlockImportRepository, BlockImportRepository>();
            services.AddScoped<IVillageServices, VillageServices>();
            services.AddScoped<IVillageImportRepository, VillageImportRepository>();
            services.AddScoped<IExcelService, ExcelService>();

            services.AddScoped<IProcessSetupServices, ProcessSetupServices>();

            services.AddScoped<IProcessSetupRepository, ProcessSetupRepository>();

            services.AddScoped<IAuditReviewModuleServices, AuditReviewModuleServices>();
            services.AddScoped<IAuditReviewParamterServices, AuditReviewParamterServices>();

            services.AddScoped<IProcessDocumentService, ProcessDocumentService>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();

            services.AddScoped<IAuditorServices, AuditorServices>();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IEmailConfigurationServices, EmailConfigurationServices>();
            services.AddScoped<IFinancialAuditReportService, FinancialAuditReportService>();
            services.AddScoped<IFundingSourceService, FundingSourceService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            #region Partner Policy And Module
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddScoped<IPartnerPolicyService, PartnerPolicyService>();

            services.AddScoped<IPartnerPolicyModuleService, PartnerPolicyModuleService>();

            #endregion
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/CSRPulse-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();

                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromHours(24)
                    };
                }
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Landing}/{id?}");

                endpoints.MapControllerRoute(
                   name: "CSRPulseArea",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
