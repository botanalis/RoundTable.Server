using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RegisterDI;
using RoundTable.Server.Data;
using RoundTable.Server.Middlewares;
using RoundTable.Server.Models;


namespace RoundTable.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // JWT Options
            services.Configure<JwtAuthOptions>(Configuration.GetSection("JwtAuthOptions"));
            
            //DB Connect
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "RoundTable.Server", Version = "v1"});
            });

            services.RegisterAssemblyPublicGenericClass()
                .Where(x => x.Namespace.StartsWith($"{AppDomain.CurrentDomain.FriendlyName}.Handlers"))
                .AsPublicImplementedInterfaces();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoundTable.Server v1"));
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            
            app.UseMiddleware<JwtMiddleware>();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}