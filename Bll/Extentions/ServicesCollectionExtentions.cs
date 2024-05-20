using Bll.Services.Impliment;
using Bll.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bll.Extentions
{
    public static class ServicesCollectionExtentions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {

            #region ================== DI Services ==================           
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IBrandServices, BrandServices>();
            services.AddTransient<IAgeServices, AgeServices>();
            services.AddTransient<ICatalogServices, CatalogServices>();
            services.AddTransient<IDiscountServices, DiscountServices>();
            #endregion

            return services;
        }
    }
}
