using AutoMapper;
using CustomerApiMicroservice.Data.Database;
using CustomerApiMicroservice.Data.Repository;
using CustomerApiMicroservice.Domain.Entities;
using CustomerApiMicroservice.Messaging.Send.Options;
using CustomerApiMicroservice.Messaging.Send.Sender;
using CustomerApiMicroservice.Models;
using CustomerApiMicroservice.Service.Commands;
using CustomerApiMicroservice.Service.Querires;
using CustomerApiMicroservice.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CustomerApiMicroservice {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerApiMicroservice", Version = "v1" });
            });

            services.AddDbContext<CustomerContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMvc().AddFluentValidation();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            services.AddTransient<IValidator<CreateCustomerModel>, CreateCustomerModelValidator>();
            services.AddTransient<IValidator<UpdateCustomerModel>, UpdateCustomerModelValidator>();


            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            if (serviceClientSettings.Enabled) {
                services.AddSingleton<ICustomerUpdateSender, CustomerUpdateSender>();
            } else {
                //services.AddSingleton<ICustomerUpdateSender, CustomerUpdateSenderServiceBus>();
            }


            services.AddTransient<IRequestHandler<CreateCustomerCommand, Customer>, CreateCustomerCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateCustomerCommand, Customer>, UpdateCustomerCommandHandler>();
            services.AddTransient<IRequestHandler<GetCustomerQuery, Customer>, GetCustomerQueryHandler>() ;
            services.AddTransient<IRequestHandler<GetAllCustomersQuery, List<Customer>>, GetAllCustomersQueryHandler>();

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
