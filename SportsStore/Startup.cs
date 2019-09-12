using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;//teste second

namespace SportsStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration) => Configuration  = configuration;

        public IConfiguration Configuration{get;}
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext> (
                    
                options => options.UseSqlServer(Configuration["Data:SportsStoreProducts:ConnectionStringHome"])
            );
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));/* >>>
         >> The AddScoped method specifies that the same object should be used to satisfy related requests for Cart
            instances. How requests are related can be configured, but by default,
            it means that any Cart required by components handling the same HTTP request will receive the same object.
            Rather than provide the AddScoped method with a type mapping, as I did for the repository, I have
            specified a lambda expression that will be invoked to satisfy Cart requests.
            The expression receives the collection of services that have been registered
            and passes the collection to the GetCart method of the SessionCart class. 
            The result is that requests for the Cart service will be handled by creating
            SessionCart objects, which will serialize themselves as session data when they are modified.
            */

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //This service is required so I can access the current session in the SessionCart class
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc( routes => {
                routes.MapRoute(
                    name: null,
                    template:"{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                    );

                routes.MapRoute(
                    name: null,
                    template:"Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1}
                    );
                routes.MapRoute(
                    name: null,
                    template:"{category}",
                    defaults: new { controller = "Product", action ="List", productPage = 1}
                    );
                routes.MapRoute(name: null,
                    template:"",
                    defaults: new { controller="Product", action="List", productPage = 1});

                //routes.MapRoute(
                //    name: "pagination",
                //    template: "Products/Page{productPage}",
                //    defaults : new { Controller = "Product", Action = "List"}
                //);
                routes.MapRoute(
                    name: null,
                    template: "{controller=Product}/{action=List}/{id?}"
                );
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
