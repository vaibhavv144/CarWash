using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


public class OrderRepository : IOrderRepository
{
    private readonly CarWashContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderRepository(CarWashContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<OrderResponseDTO> BookNowAsync(OrderRequestDTO dto, string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        var car = await _context.Cars.FirstOrDefaultAsync(c => c.UserId == user.Id && c.Make == dto.Car);
        if (car == null)
        {
            throw new Exception("Car not found.");
        }

        var addOn = await _context.AddOns.FirstOrDefaultAsync(c => c.Name == dto.AddOnService);
        if (addOn == null)
        {
            throw new Exception("AddOn not found.");
        }

        var package = await _context.ServicePackages.FirstOrDefaultAsync(c => c.Name == dto.PackageName);
        if (package == null)
        {
            throw new Exception("Package not found.");
        }

        var promo = await _context.PromoCodes.FirstOrDefaultAsync(c => c.Code == dto.PromoCode);
        if (promo == null)
        {
            throw new Exception("Promo code not found.");
        }

        var order = new Order
        {
            UserId = user.Id,
            CarId = car.Id,
            PackageId = package.Id,
            PromoCodeId = promo.Id,
            TotalAmount = package.Price + addOn.Price - (package.Price + addOn.Price) * promo.DiscountPercent / 100
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return new OrderResponseDTO
        {
            OrderId = order.Id,
            TotalAmount = order.TotalAmount
        };

        // return order;
    }

    public async Task<Order> ScheduleAsync(CreateOrderDto dto, string userId)
    {
        var addOnIds = dto.AddOnIds ?? new List<int>();
        var addOns = await _context.AddOns
            .Where(a => addOnIds.Contains(a.Id))
            .ToListAsync();

        if (addOns.Count != addOnIds.Count)
        {
            throw new Exception("One or more AddOns do not exist.");
        }

        var order = new Order
        {
            UserId = userId,
            CarId = dto.CarId,
            PackageId = dto.PackageId,
            PromoCodeId = dto.PromoCodeId,
            ScheduledDate = dto.ScheduledDate,
            Status = "PENDING",
            TotalAmount = dto.TotalAmount,
            OrderAddOns = addOns.Select(addOn => new OrderAddOn { AddOnId = addOn.Id }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetWasherOrdersAsync(string washerId)
    {
        return await _context.Orders
            .Where(o => o.WasherId == washerId)
            .ToListAsync();
    }

    public async Task<bool> CancelOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return false;

        order.Status = "Canceled";
        await _context.SaveChangesAsync();
        return true;
    }
}
