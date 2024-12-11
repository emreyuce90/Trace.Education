
namespace Trace.Education.Order.API.Feature.Order {
    public interface IOrderService {
        Task CreateAsync(OrderCreateDto orderCreateDto);
    }
}