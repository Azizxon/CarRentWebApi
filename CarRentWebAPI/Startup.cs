using System;
using CarRent;
using CarRent.Application;
using CarRent.Common;
using CarRent.Infrastructure;
using CarRentWebAPI.Filters;
using CarRentWebAPI.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentWebAPI
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
            var carRepository = new CarFileRepository("cars.json");
            var clientRepository = new ClientRepository("clients.json");
            var idProvider = IdProvider.CreateIdProvider(carRepository, clientRepository);
            var setting = new CarRentSettings(Configuration.GetValue<int>("MaxRentsWithoutCheckup"),
                Configuration.GetValue<TimeSpan>("CheckupTime"));
            var administratorCarRental = new AdministratorCarRental(carRepository, idProvider);
            var clientCarRental=new ClientCarRental(carRepository,clientRepository,idProvider,setting);

            services.AddSingleton<IAdministratorCarRental>(administratorCarRental);
            services.AddSingleton<IClientCarRental>(clientCarRental);
            
            ConfigureSecurity(services);

            services.AddMvc(o=>o.Filters.Add(new ExceptionFilter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseMvc();
        }

        private void ConfigureSecurity(IServiceCollection services)
        {
            var sercurityConfiguration = Configuration.GetSection("Security");
            var securitySetting = new SecuritySettings(
                sercurityConfiguration["Issuer"],
                sercurityConfiguration.GetValue<TimeSpan>("ExpirationPeriod"),
                Credentials.FromRawData(sercurityConfiguration["AdminEmail"], sercurityConfiguration["AdminPassword"]),
                sercurityConfiguration["EncryptionKey"]
                );

            var jwtIssuer=new JwtIssuer(securitySetting);

            services.AddSingleton(securitySetting);
            services.AddSingleton<IJwIssuer>(jwtIssuer);
        }
    }
}
