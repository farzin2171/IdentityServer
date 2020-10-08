using IdentityExample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseInMemoryDatabase("Memory");
            });
            services.AddIdentity<IdentityUser, IdentityRole>(config=>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "SecureCookie";
                config.LoginPath = "/Home/Login";
            });
            //services.AddAuthentication("cookieAuth")
            //    .AddCookie("cookieAuth", config =>
            //    {
            //        config.Cookie.Name = "SecureCookie";
            //        config.LoginPath = "/Home/Authenticate";

            //    });
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
