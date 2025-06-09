
public interface IAdminService{


    Task<object> GetDashboardAsync();
    Task<object> GetAdminProfileAsync(string adminId);

    Task<List<CustomerDto>> GetAllCustomersAsync();
    Task<List<WasherDto>> GetAllWashersAsync();
    Task<string> AddOrEditWasherAsync(WasherInputDto dto);

    Task<string> DeleteWasherAsync(string washerId);

    Task<object> ToggleUserStatusAsync(string userId);

     Task<List<OrderDto>> GetAllOrdersAsync();

     List<PdfResponse> reportGenerate(string userId);
}