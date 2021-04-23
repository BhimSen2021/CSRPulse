using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Services.Admin.Plan;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using System;

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

            #region Register component for DI
            services.AddAutoMapper(typeof(AutoMapperServices));
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<ISignupService, SignupService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            #endregion

            services.AddSession(option =>
                {
                    option.IdleTimeout = TimeSpan.FromMinutes(30);
                });

#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option =>
            {
                option.HtmlHelperOptions.ClientValidationEnabled = false;
            });
#endif

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
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "CSRPulseArea",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
