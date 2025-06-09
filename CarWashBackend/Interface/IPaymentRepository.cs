
public interface IPaymentRepository
{
    Task<WashRequest> GetOrderByIdAsync(int orderId);
    Task<PaymentReceipt> AddPaymentAsync(PaymentReceipt payment);
    Task SaveChangesAsync();
    Task<PaymentReceipt> MakePaymentAsync(PaymentRequestDto dto);
}
