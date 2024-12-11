using Trace.Education.Order.API.Feature.Orders;

namespace Trace.Education.Order.API.Feature.Order
{
    public static class OrderExtension
    {

        public static void AddOrdersExt(this IServiceCollection services) { 
        
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
