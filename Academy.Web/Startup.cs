using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Academy.Core.Convertor.SendEmail;
using Academy.Core.Services;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Context;

namespace Academy.Web
{
    public class Startup
    {
        public IConfiguration Configuration  { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //تنطیمات حجم فایل برای سستم های مک و غیره
            //services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 6000000; });
            //تنطیمات احراز هویت
            #region Authentication
            services.AddAuthentication(options=> 
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options=>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
                
                
            });
            #endregion

            #region DataBase Context
            services.AddDbContext<AcademyContext>(optins => 
            {
                optins.UseSqlServer(Configuration.GetConnectionString("AcademyConnection"));
            });
            #endregion

            #region IOC
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IOrderService, OrderService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //چون داریم از احراز هویت استفاده میکنیم
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(

                    name: "Areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(

                    name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });





            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
