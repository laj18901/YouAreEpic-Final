using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.IO;
using YouAreEpic.Backend.Configuration;
using YouAreEpic.Backend.MongoDB;
using YouAreEpic.Backend.Repository.Implementations;
using YouAreEpic.Backend.Services;

namespace YouAreEpic.Backend
{
    public class Startup
    {
        private string ConnectionString { get; set; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("mongodb");
            StripeConfiguration.ApiKey = Configuration["Stripe:SecretKey"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<StripeOptions>(options =>
            {
                options.PublishableKey = Configuration["Stripe:PublishableKey"];
                options.SecretKey = Configuration["Stripe:SecretKey"];
                options.WebhookSecret = Configuration["Stripe:WebhookSecret"];
                options.Domain = Configuration["Stripe:Domain"];
            });

            services.AddSingleton<IPostRepository>
            (
                (s) => new PostRepository
                (
                    new MongoSettings(ConnectionString,"youareepic-db")
                )
            );

            services.AddSingleton<ICategoryRepository>
            (
                (s) => new CategoryRepository
                (
                    new MongoSettings(ConnectionString, "youareepic-db")
                )
            );

            services.AddSingleton<INonprofitorganisationRepository>
            (
                (s) => new NonprofitorganisationRepository
                (
                    new MongoSettings(ConnectionString, "youareepic-db")
                )
            );

            services.AddHostedService<DataBaseSeederService>();
            services.AddSingleton<PostService>();
            services.AddHttpClient();

            ConfigureTwitter(services);
        }


        public void ConfigureTwitter(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddTransient<TwitterConfigurationService>();
            services.AddTransient<TwtiterAuthRouteService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => { builder.AllowAnyOrigin(); });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
