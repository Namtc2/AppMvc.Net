using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppMvc.Net.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppMvc.Net
{
    public class Startup
    {
        public static string ContentRootPath;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<RazorViewEngineOptions>(options=>{
                //Tìm kiếm trong /View/Controller/Action.cshtml nếu không thấy tìm tiếp format do mình tạo chỉ định tiếp ->
                //Tìm kiếm trong /MyView/Controller/Action.cshtml
                // {0} Tên action, {1} Tên controller, {2} Tên area
                options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
            });
            //services.AddSingleton- chỉ tạo một đối tượng dịch vụ
            // services.AddTransient - mỗi lần lấy dịch vụ thì một đối tượng mới được tạo ra
            // services.AddScoped - mỗi phiên truy cập, nếu lấy dịch vụ này ra thì là một đối tượng mới được tạo
            services.AddSingleton<ProductService, ProductService>();
            // services.AddSingleton<ProductService>();
            // services.AddSingleton(typeof(ProductService));
            // services.AddSingleton(typeof(ProductService),typeof(ProductService));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
