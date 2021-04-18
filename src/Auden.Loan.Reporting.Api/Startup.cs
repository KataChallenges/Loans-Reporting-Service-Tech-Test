using Auden.Loan.Reporting.Application.Services;
using Auden.Loan.Reporting.Infrastructure.Database;
using Auden.Loan.Reporting.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Auden.Loan.Reporting.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddLogging();
            services.AddOptions().Configure<DataSourceSettings>(Configuration.GetSection("DataSource"));

            AddDependencies(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auden Loans Reporting Service", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auden Loans Reporting Service v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            serviceProvider.GetService<IDatabaseContext>().Setup();
        }

        private void AddDependencies(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseContext, DatabaseContext>();
            services.AddSingleton<IReportingService, ReportingService>();
            services.AddSingleton<IDataRepository, DataRepository>();
        }

    }
}
