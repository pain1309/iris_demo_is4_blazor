using CMS.BlazorServer.Data;
using CMS.BlazorServer.HubService;
using Infrastructure.Context;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Management.gRPC.ProductService;

namespace CMS.BlazorServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                .WithOrigins("http://localhost:4203")
                .AllowAnyHeader()
                .AllowAnyMethod());
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddSignalR().AddMessagePackProtocol();

            services.AddGrpcClient<ProductServiceClient>(o => o.Address = new Uri(Configuration["GrpcSettings:DiscountUrl"]));

            services.AddScoped<ProductService>();

            services.AddDbContext<OrderContext>(o => o.UseSqlServer(Configuration.GetConnectionString("OrderingConnectionString")));

            services.AddStackExchangeRedisCache(o => o.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString"));

            services.AddRazorPages();
            services.AddServerSideBlazor();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapHub<HubBusiness>("/TransferHub");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
