using Microsoft.EntityFrameworkCore;

public class WasherRepository : IWasherRepository
{
    private readonly CarWashContext _context;

    public WasherRepository(CarWashContext context)
    {
        _context = context;
    }

    public async Task<List<AvailableOrderResponseDTO>> GetAvailableOrdersAsync()
    {
        var orders = await _context.Orders.Where(o => o.Status == "PENDING").ToListAsync();
        var result = new List<AvailableOrderResponseDTO>();

        foreach (var order in orders)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == order.CarId);
            if (car != null)
            {
                result.Add(new AvailableOrderResponseDTO
                {
                    userId = order.UserId,
                    Make = car.Make,
                    Model = car.Model,
                    Amount = order.TotalAmount
                });
            }
        }

        return result;
    }

    public async Task<string> AcceptOrderAsync(int orderId, string washerId)
    {
        var order = await _context.Orders.FindAsync(orderId);

        if (order == null || order.Status != "PENDING")
            throw new Exception("Order not found or already processed.");

        order.Status = "ACCEPTED";
        order.WasherId = washerId;

        var wash = new WashRequest
        {
            UserId = order.UserId,
            WasherId = washerId,
            CarId = order.CarId,
            PackageId = order.PackageId,
            PromoCodeId = order.PromoCodeId,
            ScheduledDate = order.ScheduledDate,
            Status = "ACCEPTED"
        };

        _context.WashRequests.Add(wash);
        await _context.SaveChangesAsync();

        return "Order accepted.";
    }

    public async Task<string> RejectOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);

        if (order == null || order.Status != "PENDING")
            throw new Exception("Order not found or already processed.");

        order.Status = "REJECTED";
        await _context.SaveChangesAsync();
        return "Order rejected.";
    }

    public async Task<IEnumerable<Order>> GetWasherOrdersAsync(string washerId, List<string> statuses)
    {
        return await _context.Orders
            .Include(o => o.Car)
            .Include(o => o.Package)
            .Where(o => o.WasherId == washerId && statuses.Contains(o.Status))
            .ToListAsync();
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, string status, string imageUrl, string washerId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return false;

        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == order.CarId);
        var package = await _context.ServicePackages.FirstOrDefaultAsync(p => p.Id == order.PackageId);
        var washReq = await _context.WashRequests.FirstOrDefaultAsync(w => w.UserId == order.UserId);

        if (washReq != null)
        {
            washReq.Status = status;
        }

        var invoice = new Invoice
        {
            CarName = car?.Model,
            Payment = order.TotalAmount,
            UserId = order.UserId,
            PackageName = package?.Name,
            ImageUrl = imageUrl,
            WasherId = washerId
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();
        return true;
    }
}
