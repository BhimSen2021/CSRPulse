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
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            #region R E G I S T E R  C O M P O N E N T  F O R  D I 

            services.AddAutoMapper(typeof(AutoMapperServices));
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IAccountService, AccountService>();
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

            #endregion

            services.AddSession(option =>
                {
                    option.IdleTimeout = TimeSpan.FromMinutes(30);
                });

#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option =>
            {
                option.HtmlHelperOptions.ClientValidationEnabled = true;
            });
#endif

            services.AddDNTCaptcha(options =>
                 options.UseCookieStorageProvider()
                     .ShowThousandsSeparators(false));

            services.Configure<SMTPConfig>(Configuration.GetSection("MailSettings"));
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
            app.UseStaticFiles();

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
