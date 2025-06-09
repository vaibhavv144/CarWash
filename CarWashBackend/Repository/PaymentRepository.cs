


using Microsoft.EntityFrameworkCore;


public class PaymentRepository : IPaymentRepository
{
    private readonly CarWashContext _context;

    public PaymentRepository(CarWashContext context)
    {
        _context = context;
    }

    public async Task<WashRequest> GetOrderByIdAsync(int orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) return null;

        var wash = await _context.WashRequests.FirstOrDefaultAsync(c => c.UserId == order.UserId);
        return wash;
    }

    public async Task<PaymentReceipt> AddPaymentAsync(PaymentReceipt payment)
    {
        _context.PaymentReceipts.Add(payment);
        return payment;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<PaymentReceipt> MakePaymentAsync(PaymentRequestDto dto)
    {
        var order = await GetOrderByIdAsync(dto.OrderId);
        var orders = await _context.Orders.FirstOrDefaultAsync(c => c.Id == dto.OrderId);

        if (orders == null)
            throw new Exception("Order not found");

        if (orders.TotalAmount > dto.AmountPaid)
            throw new Exception("Insufficient payment");

        var payment = new PaymentReceipt
        {
            OrderId = dto.OrderId,
            PaymentDate = DateTime.UtcNow,
            AmountPaid = dto.AmountPaid,
            ReceiptImageUrl = "https://tse4.mm.bing.net/th/id/OIP.oQkrT1aIxCimilfOfkSOyAHaE6?rs=1&pid=ImgDetMain"
        };

        await AddPaymentAsync(payment);
        await SaveChangesAsync();

        return payment;
    }
}
