using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VideoGameDBServices.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using VideoGameDBServices.Interfaces;
using VideoGameDBServices.Repositories;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Hosting;

namespace VideoGameDBServices
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
            services.AddControllers();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IYearRepository,YearRepository>();
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<ISystemRepository, SystemRepository>();
            services.AddMvc();
            services.AddODataQueryFilter();

            services.AddResponseCaching();

            //Setting it up so we can read settings from the appsettings.json, mainly for the db path
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var connection = Configuration["database"];
            services.AddDbContext<VideogamesContext>(options=>options.UseSqlite(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseMvc();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

            //app.UseMvc(routes =>
            //{
            //   //routes.Select().Count().Filter().OrderBy().Expand().MaxTop(null);
            //    //routes.EnableDependencyInjection();
            //});

            app.UseResponseCaching();
        }

    }
}
