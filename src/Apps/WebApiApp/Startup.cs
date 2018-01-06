using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChefsBook.Auth.Security;
using ChefsBook.Core;
using ChefsBook.Core.Repositories;
using ChefsBook.Core.Services;
using ChefsBook.Environment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace ChefsBook.WebApiApp
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
            services.AddCors();

            services
                .AddMvc(options =>
                    options.Filters.Add(new RequireHttpsAttribute())
                )
                .AddJsonOptions(options =>
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                );
            
            services.AddDbContext<CoreDbContext>(opts =>
                opts.UseSqlServer(databaseConnStr, cfg =>
                    cfg.MigrationsAssembly(ProjectConsts.Migrations))
            );

            services.AddTransient<CoreUnitOfWork, CoreUnitOfWork>();
            services.AddScoped<IRecipesRepository, RecipesRepository>();
            services.AddScoped<ITagsRepository, TagsRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IRecipesService, RecipesService>();
            services.AddScoped<ITagsService, TagsService>();
            services.AddScoped<ICartService, CartService>();
            
            ConfigureAuthentication(services);

            services.AddAutoMapper();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ChefsBook API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(Configuration["Swagger:Endpoint"], Configuration["Version"]);
                });
                
            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }

        public void ConfigureAuthentication(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["Services:Auth"];
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.ValidateIssuer = false;
                    
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters.RoleClaimType = KnownClaims.Role;
                });
        }
    }
}
