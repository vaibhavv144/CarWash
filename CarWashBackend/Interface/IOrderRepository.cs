
public interface IOrderRepository
{
    Task<OrderResponseDTO> BookNowAsync(OrderRequestDTO dto, string userId);
    Task<Order> ScheduleAsync(CreateOrderDto dto, string userId);
    Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
    Task<IEnumerable<Order>> GetWasherOrdersAsync(string washerId);
    Task<bool> CancelOrderAsync(int orderId);
}
