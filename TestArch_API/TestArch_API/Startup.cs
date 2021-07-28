using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TestArch_API.Data;

namespace TestArch_API
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
            /* ------------------------------------------------------------------------------------------------------------------*/
            /* ----- SERVICIO: CORS -----*/
            /* ------------------------------------------------------------------------------------------------------------------*/
            services.AddCors();

            /* ------------------------------------------------------------------------------------------------------------------*/
            /* ----- SERVICOS DEL API FRAMEWORK -----*/
            /* ------------------------------------------------------------------------------------------------------------------*/
            services.AddControllers();

            /* ------------------------------------------------------------------------------------------------------------------*/
            /* ----- SERVICIO: ORM DbContext -----*/
            /* ------------------------------------------------------------------------------------------------------------------*/
            services.AddDbContext<TestArch_APIContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("TestArch_APIContext")));                    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TestArch_APIContext context)
        {
            /* ------------------------------------------------------------------------------------------------------------------*/
            /* ----- CORS -----*/
            /* ----- Dan acceso al cliente en el localhost 3000 -----*/
            /* ------------------------------------------------------------------------------------------------------------------*/
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:3000");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            /* ------------------------------------------------------------------------------------------------------------------*/
            /* ----- API FRAMEWORK -----*/            
            /* ------------------------------------------------------------------------------------------------------------------*/
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });           
        }
    }
}
