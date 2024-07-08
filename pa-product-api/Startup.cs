using Microsoft.OpenApi.Models;
using pa_product_api.Services;
using pa_product_api.Extensions;

namespace pa_product_api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "pa-product-api API", Version = "v1" });
            });
            
            //Oluşturulan fake servislerin eklenmesi
            //services.AddScoped<IAccountService, FakeAccountService>();
            services.AddSingleton<IAccountService, FakeAccountService>();
            services.AddSingleton<IUserService, FakeUserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "pa-product-api API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            
            app.Use(async (context, next) =>
            {
                Console.WriteLine("Action Girildi....");
                await next.Invoke();
                Console.WriteLine("Action çıktı...");
            });
            
            app.UseLoggingMiddleware();
            
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}