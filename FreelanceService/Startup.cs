using AutoMapper;
using FreelanceService.BLL.Automapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Interfaces.ValidationServices;
using FreelanceService.BLL.Services;
using FreelanceService.BLL.Services.Validations;
using FreelanceService.BLL.Validation;
using FreelanceService.BLL.Validation.Validator;
using FreelanceService.Common.Validation;
using FreelanceService.DAL.Concrate;
using FreelanceService.DAL.Interfaces;
using FreelanceService.DAL.Repositories;
using FreelanceService.Web.Logger;
using FreelanceService.Web.Models;
using FreelanceService.Web.Validation;
using FreelanceService.Web.Validation.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace FreelanceService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.HostingEnvironment = env;
            this.Configuration = configuration;
        }

        public IHostingEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionStr = Configuration.GetConnectionString("DefaultConnection");
            string gearHostConnection = "Data Source = den1.mssql7.gear.host;Initial Catalog=freelanceservice;Integrated Security=false;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;User Id=freelanceservice; Password=Vy82Y5g8hT-~;";
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });

            services.AddSingleton(config.CreateMapper());
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<IValidaterUser, ValidateUser>();
            services.AddScoped<IValidateJob, ValidateJob>();
            services.AddScoped<IDbContext, DbContext>(provider => new DbContext(connectionStr));
            services.AddScoped<IUnitOfWork, UnitOfWork>(provider => new UnitOfWork(connectionStr, (IDbContext)provider.GetService(typeof(IDbContext))));

            RegisterValidationServices(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }


        private void RegisterValidationServices(IServiceCollection services)
        {
            services.AddSingleton<IDTOValidationService, DTOValidationService>();
            services.AddSingleton<IValidator<UserProfileEditDTO>, UserProfileEditDTOValidator>();
            services.AddSingleton<IViewModelValidationService, ViewModelValidationService>();
            services.AddSingleton<IValidator<ProfileEditViewModel>, ProfileEditViewModelValidator>();
            services.AddSingleton<IValidator<RegisterViewModel>, RegisterViewModelValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Profile}/{action=Profile}/{id?}");
            });

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");

            app.Run(async (context) =>
            {
                logger.LogInformation("Processing request {0}", context.Request.Path);
                await context.Response.WriteAsync(DateTime.Now.ToString());
            });
        }
    }
}
