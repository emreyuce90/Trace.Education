
namespace Trace.Education.Order.API.Feature.Orders {
    public interface IOrderService {
        Task CreateAsync(OrderCreateDto orderCreateDto);
    }
}