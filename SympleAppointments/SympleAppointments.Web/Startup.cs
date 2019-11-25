using AutoMapper;
using FluentValidation.AspNetCore;
using McMaster.AspNetCore.LetsEncrypt;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SympleAppointments.Application.Reviews;
using SympleAppointments.Domain;
using SympleAppointments.Persistence;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace SympleAppointments.Web
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
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("SympleAppointments.Web")
                    ));

            services.AddDefaultIdentity<AppUser>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<AppUser, DataContext>();
            services.AddMediatR(typeof(Create).Assembly);
            services.AddAutoMapper(typeof(Create));

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services
                .AddControllersWithViews(cfg =>
                {
                    cfg.RespectBrowserAcceptHeader = true;

                })
                .AddXmlSerializerFormatters();
            services.AddRazorPages()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Create>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            })

                ;
            // Register the Swagger services
            services.AddSwaggerDocument();
            services.AddLetsEncrypt(o =>
            {
                o.DomainNames = new[] { "d134ae77.ngrok.io" };
                o.UseStagingServer = false; // <--- use staging

                o.AcceptTermsOfService = true;
                o.EmailAddress = "carre85@gmail.com";
            })
                //.PersistCertificatesToDirectory(new DirectoryInfo( Assembly.GetExecutingAssembly().Location).Parent, "test");
                .PersistCertificatesToLocalX509Store(StoreName.My, StoreLocation.CurrentUser);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
