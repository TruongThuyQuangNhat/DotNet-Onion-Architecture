using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using ServicesLayer;
using ServicesLayer.ChucDanhService;
using ServicesLayer.ChucVuService;
using ServicesLayer.NhanVienService;
using ServicesLayer.PhongBanService;
using System;
using System.Collections.Generic;
using System.Linq;
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
            services.AddDbContext<ApplicationDbContext>(item => item.UseNpgsql(Configuration.GetConnectionString("myconn")));
            #endregion

            #region Services Injected  
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<INhanVienService, NhanVienService>();
            services.AddTransient<IPhongBanService, PhongBanService>();
            services.AddTransient<IChucVuService, ChucVuService>();
            services.AddTransient<IChucDanhService, ChucDanhService>();
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
