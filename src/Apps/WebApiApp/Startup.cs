using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChefsBook.Core;
using ChefsBook.Core.Repositories;
using ChefsBook.Environment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChefsBook.WebApiApp
{
    public class Startup
    {
        private readonly string databaseConnStr;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true);
            var configuration = builder.Build();

            this.databaseConnStr = configuration.GetConnectionString("Database");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CoreDbContext>(opts =>
                opts.UseSqlServer(databaseConnStr, cfg =>
                    cfg.MigrationsAssembly(ProjectConsts.Migrations))
            );

            services.AddTransient<CoreUnitOfWork, CoreUnitOfWork>();
            services.AddScoped<IRecipesRepository, RecipesRepository>();
            services.AddMvc();
            services.AddAutoMapper();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
