using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependancyInjection
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddVisitorService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Random random = new Random();
            int id = random.Next();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/exception");
                //  app.UseStatusCodePagesWithRedirects("/error");
                //app.UseStatusCodePagesWithReExecute("/error",$"?status{0}");
                app.UseStatusCodePagesWithRedirects($"/{id}/"+"{0}.html");

            }
            app.UseMiddleware<VisitMiddleware>();
            app.UseMiddleware<VisitMiddleware>();
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = $"/{id}"
            }); 
            app.Use((context, next) =>
            {
                int x = 0;
               
                return next.Invoke();
            });
            app.UseRouting();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                //endpoints.MapGet("/error", async context =>
                //{
                ////    await context.Response.WriteAsync("Error!"+context.Response.StatusCode);

                //});
                //endpoints.MapGet("/exception", async context =>
                //{
                //    if (context.Response.StatusCode==500)
                //    {
                //        context.Request.Path = "404.html";
                //    }
                //    //    await context.Response.WriteAsync("Error!"+context.Response.StatusCode);

                //});
            });
        }
    }

}
