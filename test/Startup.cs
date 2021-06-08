using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using test.Contexts;
using test.Entities;
using test.Interfaces;
using test.Repositories;

namespace test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication();
            services.AddDbContext<TestContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();//gördüğün zaman onu ver.
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddIdentity<AppUser,IdentityRole>(opt => { opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<TestContext>();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/Login");
                opt.Cookie.Name = "TestNetCore";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite= SameSiteMode.Strict;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });
            //Baskette kullanacığımız http contexti ekledik.
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePagesWithReExecute("/Home/NotFound","?code=");
            /*app.UseStatusCodePages();*///olmayan bir urlde  booş sayfa çıkmaması için 
            //IdentityInitializier.CreateAdmin(userManager,roleManager);//burada hata olursa göstermez hata ile karşılaşırsa
            app.UseExceptionHandler("/Error");
            app.UseRouting();
            //npm i disari acma
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
            //    RequestPath="/content"
            //});
            app.UseStaticFiles();
            app.UseSession();
            //2 Middleware kullandık
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(name: "TestRouute", pattern: "Ezgi" ,  defaults :new {Controller="Home", Action="Index"});

            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "areas", pattern: "{area}/{Controller=Home}/{Action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller=Home}/{Action=Index}/{id?}");
               
            });
           
        }
    }
}
