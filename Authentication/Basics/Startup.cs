using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Basics
{
    public class Startup
    {
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("cookieAuth")
                .AddCookie("cookieAuth", config =>
                {
                    config.Cookie.Name = "SecureCookie";
                    config.LoginPath = "/Home/Authenticate";

                });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //UseRouting looks at the route you are accessing and then decides wihich endpoint is going to be used ,
            //so at the end endpoint is going to be your functionality
            app.UseRouting();

            //who are you
            app.UseAuthentication();

            //are you allowed
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
