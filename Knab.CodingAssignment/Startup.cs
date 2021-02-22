using Knab.CodingAssignment.ApplicationServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Knab.CodingAssignment.Domain.Crypto;
using Knab.CodingAssignment.Domain.Exchange;
using Knab.CodingAssignment.Framework.Exceptions;
using Knab.CodingAssignment.Infrastructure;
using Knab.CodingAssignment.Infrastructure.CryptoPriceRepository;
using Knab.CodingAssignment.Infrastructure.ExchangeRateRepository;

namespace Knab.CodingAssignment
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
            services.AddMemoryCache();

            services.AddControllers();

            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();

            services.AddScoped<IExchangeRateRepository, UsdExchangeRateRepository>();
            services.AddScoped<ICryptoPriceRepository, CoinMarketCapRepository>();
            
            services.AddScoped<ICryptoPriceService, CryptoPriceService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler();

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
