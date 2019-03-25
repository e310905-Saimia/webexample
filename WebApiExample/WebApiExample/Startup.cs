using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiExample.Models;
using WebApiExample.Repositories;
using WebApiExample.Services;

namespace WebApiExample
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
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddDbContext<PersondbContext>(opt =>
                {
                    //opt.UseSqlServer(Configuration.GetConnectionString("LocalPersonDBContext"));
                    opt.UseSqlServer(Configuration.GetConnectionString("AzurePersonDBContect"));
                }
            );

            // Cors 
            //services.AddCors(options =>
            //{
            //    // Todo: change what erery needed in real server
            //    options.AddPolicy("CorsPolicy", builder =>
            //        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            //});
            // ignore json serialization
            services.AddMvc().AddJsonOptions(json =>
                json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Cors
            //app.UseCors("CorsPolicy");

            // keep this last
            app.UseMvc();
        }
    }
}
