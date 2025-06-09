

public interface IWasherRepository
{
    Task<List<AvailableOrderResponseDTO>> GetAvailableOrdersAsync();
    Task<string> AcceptOrderAsync(int orderId, string washerId);
    Task<string> RejectOrderAsync(int orderId);
    Task<IEnumerable<Order>> GetWasherOrdersAsync(string washerId, List<string> statuses);
    Task<bool> UpdateOrderStatusAsync(int orderId, string status, string imageUrl, string washerId);
}
