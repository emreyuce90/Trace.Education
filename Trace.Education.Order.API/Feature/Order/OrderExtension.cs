using Trace.Education.Order.API.Feature.Order;

namespace Trace.Education.Order.API.Repositories.Order
{
    public static class OrderExtension
    {

        public static void AddOrdersExt(this IServiceCollection services) { 
        
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
