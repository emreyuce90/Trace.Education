namespace Trace.Education.Order.API.Feature.Order; 
public record OrderCreateDto(string OrderCode,Guid UserId, List<OrderItemDto> Items);
