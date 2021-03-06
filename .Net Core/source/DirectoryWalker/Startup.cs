﻿using System;
using Autofac.Extensions.DependencyInjection;
using DirectoryWalker.Database.Contexts;
using DirectoryWalker.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DirectoryWalker
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            CreateLogger();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql();
            services.AddMvc();
            services.AddMemoryCache();

            var applicationContainer = services.AddAutofac(configuration);
            return new AutofacServiceProvider(applicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
           
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "hierarchy",
                    template: "{*enteredPath}",
                    defaults: new { controller = "Home", action = "BrowseHierarchyTree" });
            });

            ConfigureLogger(loggerFactory);
        }

        private void CreateLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private void ConfigureLogger(ILoggerFactory loggerFactory)
        {
            var loggerConfigSection = configuration.GetSection("Logging");

            loggerFactory.AddConsole(loggerConfigSection);
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
        }
    }
}