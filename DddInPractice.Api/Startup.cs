using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using DddInPractice.Api.Infrastructure.AutofacModules;
using DddInPractice.CommandHandlers.Commands;
using DddInPractice.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;

namespace DddInPractice.Api
{
    public class Startup
    {
        public string AppName => "SnackApp";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvc()
                .AddCustomDbContext(Configuration)
                .AddCustomSwagger(appName: AppName);

            services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly,
                typeof(InitializeSnakMachineCommand).GetTypeInfo().Assembly);

            var container = new ContainerBuilder();
            container.Populate(services);
            
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));
            container.RegisterModule(new MediatorModule());

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();          
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var basePath = Environment.GetEnvironmentVariable("ASPNETCORE_APPL_PATH") ?? "/";
                if (!basePath.EndsWith("/"))
                {
                    basePath += "/";
                }

                c.SwaggerEndpoint($"{basePath}swagger/{AppName}/swagger.json", $"{AppName}");
            });
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                    .AddJsonOptions(opt =>
                    {
                        opt.SerializerSettings.ContractResolver = 
                            new CamelCasePropertyNamesContractResolver();
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddControllersAsServices();    //Injecting Controllers themselves thru DI
                                                    //For further info see: http://docs.autofac.org/en/latest/integration/aspnetcore.html#controllers-as-services

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<DefaultDbContext>(options =>
                   {
                       options.UseSqlServer(configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string appName)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc(appName, new Info
                {
                    Title = $"{appName} HTTP API",
                    Version = appName
                });
            });

            return services;
        }
    }
}
