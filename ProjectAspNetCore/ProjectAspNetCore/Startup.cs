using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using ProjectAspNetCore.Contexts;
using ProjectAspNetCore.Entities;
using ProjectAspNetCore.Interfaces;
using ProjectAspNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProjectContext>();

            services.AddHttpContextAccessor();

            services.AddAuthentication();

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ProjectContext>();

            services.ConfigureApplicationCookie(opt =>
           {
               opt.LoginPath = new PathString("/Home/GirisYap");
               opt.Cookie.Name = "AspNetCore";
               opt.Cookie.HttpOnly = true;
               opt.Cookie.SameSite = SameSiteMode.Strict;
               opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
           });

            services.AddScoped<ISepetRepository, SepetRepository > ();
            services.AddScoped<IKategoriRepository, KategoriRepository>();
            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddScoped<IUrunKategoriRepository, UrunKategoriRepository>();
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment
            env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/Home/NotFound","?code={0}");

            IdentityInitializer.OlusturAdmin(userManager, roleManager);
            app.UseRouting();
            //Node Moduls Dþarý Açma npm ile
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider=new PhysicalFileProvider(Path.Combine
            //    (Directory.GetCurrentDirectory(),"node_modules")),
            //    RequestPath="/content"

            //});
            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {//ysk.com.tr/Home/Index 
             //endpoints.MapControllerRoute(
             //    name:"alpRoute",
             //    pattern: "alp",
             //    defaults: new {Controller = "Home", Action = "Index"}
             //    );

                endpoints.MapControllerRoute(name: "areas", pattern: "{area}/{Controller=Home}/{Action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller=Home}/{Action=Index}/{id?}");


            });
        }
    }
}
