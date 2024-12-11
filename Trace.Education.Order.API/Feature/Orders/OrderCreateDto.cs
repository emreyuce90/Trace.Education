namespace Trace.Education.Order.API.Feature.Orders; 
public record OrderCreateDto(string OrderCode,Guid UserId, List<OrderItemDto> Items);
