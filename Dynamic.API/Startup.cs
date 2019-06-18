using System;
using System.IO;
using System.Reflection;
using Dynamic.Domain.DynamicContext.Handlers;
using Dynamic.Domain.DynamicContext.Repositories;
using Dynamic.Infra.DynamicContext.DataContext;
using Dynamic.Infra.DynamicContext.Repositories;
using Dynamic.Shared.DynamicContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Dynamic.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public static IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment hostingEnvironment) => _hostingEnvironment = hostingEnvironment;

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_hostingEnvironment.IsProduction() ? "appsettings.json" : $"appsettings.{_hostingEnvironment.EnvironmentName}.json");

            Configuration = builder.Build();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddResponseCompression();

            Settings.ConnectionString = $"{Configuration["ConnectionString"]}";

            services.AddDbContext<DynamicDataContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Settings.ConnectionString));

            services.AddTransient<UserCommandHandler, UserCommandHandler>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddDistributedMemoryCache();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Dynamic API",
                    Description = "Dynamic Project",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Fernando Velloso Borges de Mélo Gomes",
                        Email = "fernandovbmgomes@hotmail.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

                x.DescribeAllEnumsAsStrings();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
            app.UseResponseCompression();

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.DocumentTitle = "Dynamic";

                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Dynamic - V1");
                x.RoutePrefix = string.Empty;

                x.DefaultModelsExpandDepth(-1);
                x.DocExpansion(DocExpansion.None);
            });
        }
    }
}
