using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using RepositoryLayer;
using ServicesLayer;
using ServicesLayer.ChucDanhService;
using ServicesLayer.ChucVuService;
using ServicesLayer.NhanVienService;
using ServicesLayer.PhongBanService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ERMCoreUI
{
    public class Startup
    {
        private readonly string _policyName = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERMCoreUI", Version = "v1" });
            });

            #region Connection String  
            services.AddDbContext<ApplicationDbContext>(item =>
            {
                item.UseNpgsql(Configuration.GetConnectionString("myconn"));
                item.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            #endregion

            #region Services Injected  
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<INhanVienService, NhanVienService>();
            services.AddScoped<IPhongBanService, PhongBanService>();
            services.AddScoped<IChucVuService, ChucVuService>();
            services.AddScoped<IChucDanhService, ChucDanhService>();
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            //  services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERMCoreUI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_policyName);
           // app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200"));

            app.UseAuthorization();

            // removed for brevity

            app.UseStaticFiles();    // for the wwwroot folder

            // for the wwwroot/uploads folder
            string uploadsDir = Path.Combine(env.WebRootPath, "images");
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = "/images",
                FileProvider = new PhysicalFileProvider(uploadsDir)
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
