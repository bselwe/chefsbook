using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuthApp.Configuration;
using ChefsBook.Auth;
using ChefsBook.Auth.Services;
using ChefsBook.Environment;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChefsBook.AuthApp
{
    public class Startup
    {
        private readonly string databaseConnStr;
        
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true);
            Configuration = builder.Build();

            this.databaseConnStr = Configuration.GetConnectionString("Database");
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddDbContext<AuthDbContext>(opts =>
                opts.UseSqlServer(databaseConnStr, cfg =>
                    cfg.MigrationsAssembly(ProjectConsts.Migrations))
            );

            services.AddScoped<IAccountService, AccountService>();
            
            services.AddIdentity<AuthUser, AuthRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            ConfigureIdentityServer(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseMvc();
        }

        private void ConfigureIdentityServer(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryApiResources(ISConfiguration.GetApiResources())
                .AddInMemoryIdentityResources(ISConfiguration.GetIdentityResources())
                .AddInMemoryClients(ISConfiguration.GetClients())
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<AuthUser>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;                
            });
        }
    }
}
