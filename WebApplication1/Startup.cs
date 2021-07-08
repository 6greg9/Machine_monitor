using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRChat.Hubs;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        ///// <summary>
        ///// 中间件
        ///// </summary>
        //public class SignalRSendMildd
        //{
        //    /// <summary>
        //    ///上下文
        //    /// </summary>
        //    private readonly RequestDelegate _next;

        //    private readonly IHubContext<MonitorHub> _hubContext;

        //    /// <summary>
        //    ///注入上下文
        //    /// </summary>
        //    /// <param name="next"></param>
        //    /// <param name="hubContext"></param>
        //    public SignalRSendMildd(RequestDelegate next, IHubContext<MonitorHub> hubContext)
        //    {
        //        _next = next;
        //        _hubContext = hubContext;
        //    }

        //    /// <summary>
        //    /// SignalR过滤
        //    /// </summary>
        //    /// <param name="context"></param>
        //    /// <returns></returns>
        //    public async Task InvokeAsync(HttpContext context)
        //    {
        //        await _next(context);
        //    }
        //}
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSignalR();
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

            app.UseAuthorization();

            app.UseCors("AllowCors");
            //app.UseSignalRSendMildd();
            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<ChatHub>("/chatHub");
            //    routes.MapHub<MonitorHub>("/MonitorHub");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<MonitorHub>("/MonitorHub");
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
