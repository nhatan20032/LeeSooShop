using Bll.Impliment;
using Bll.Interface;
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
            #endregion

            return services;
        }
    }
}
