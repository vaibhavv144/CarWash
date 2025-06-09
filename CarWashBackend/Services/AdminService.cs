using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


public class AdminService : IAdminService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CarWashContext _context;


    public AdminService(UserManager<ApplicationUser> userManager, CarWashContext context)
    {
        _userManager = userManager;
        _context = context;
    }


    public async Task<object> GetDashboardAsync()
    {
        var washers = await _userManager.GetUsersInRoleAsync("Washer");
        var orders = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.Washer)
            .ToListAsync();


        return new
        {
            Washers = washers.Select(w => new { w.Id, w.UserName, w.Email, w.IsActive, w.IsAvailable }),
            Orders = orders
        };
    }


    public async Task<object> GetAdminProfileAsync(string adminId)
    {
        var admin = await _userManager.FindByIdAsync(adminId);
        if (admin == null) return null;


        return new
        {
            admin.Id,
            admin.UserName,
            admin.Email,
            admin.PhoneNumber,
            admin.IsActive
        };
    }


  public async Task<List<CustomerDto>> GetAllCustomersAsync()
{
    var users = await _userManager.GetUsersInRoleAsync("User");

    return users.Select(user => new CustomerDto
    {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        IsActive=user.IsActive,
        IsAvailable=user.IsAvailable
        
    }).ToList();
}


    public async Task<List<WasherDto>> GetAllWashersAsync()
{
    var users = await _userManager.GetUsersInRoleAsync("Washer");

    return users.Select(user => new WasherDto
    {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        IsAvailable = user.IsAvailable,
        IsActive = user.IsActive
    }).ToList();
}



    public async Task<string> AddOrEditWasherAsync(WasherInputDto dto)
{
    var existing = await _userManager.FindByEmailAsync(dto.Email);
    if (existing == null)
    {
        var washer = new ApplicationUser
        {
            UserName = dto.Name,
            Email = dto.Email,
            IsAvailable = dto.IsAvailable,
            IsActive = dto.IsActive
        };

        var result = await _userManager.CreateAsync(washer, "Washer@123");
        if (!result.Succeeded)
            return string.Join(", ", result.Errors.Select(e => e.Description));

        await _userManager.AddToRoleAsync(washer, "Washer");
        return "Washer added";
    }
    else
    {
        existing.UserName = dto.Name;
        existing.IsAvailable = dto.IsAvailable;
        existing.IsActive = dto.IsActive;

        var result = await _userManager.UpdateAsync(existing);
        return result.Succeeded ? "Washer updated" : "Update failed";
    }
}




    public async Task<object> ToggleUserStatusAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return null;


        user.IsActive = !user.IsActive;
        await _userManager.UpdateAsync(user);
        return new { user.Id, user.IsActive };
    }


    public async Task<List<OrderDto>> GetAllOrdersAsync()
{
    return await _context.Orders
        .Include(o => o.User)
        .Include(o => o.Washer)
        .Select(o => new OrderDto
        {
            Id = o.Id,
            UserId = o.UserId,
            UserName = o.User.UserName,
            WasherId = o.WasherId,
            WasherName = o.Washer != null ? o.Washer.UserName : null,
            CarId = o.CarId,
            PackageId = o.PackageId,
            PromoCodeId = o.PromoCodeId,
            ScheduledDate = o.ScheduledDate,
            Status = o.Status,
            TotalAmount = o.TotalAmount
        })
        .ToListAsync();
}

    public async Task<string> DeleteWasherAsync(string washerId)
{
    var washer = await _userManager.FindByIdAsync(washerId);
    if (washer == null)
        return "Washer not found";

    var result = await _userManager.DeleteAsync(washer);
    return result.Succeeded ? "Washer deleted" : "Delete failed";
}



    public List<PdfResponse> reportGenerate(string userId){
        var user = _context.Users.Find(userId);
        var orders = _context.Orders.Where(o =>o.UserId==userId).ToList();
        List<PdfResponse>ls=new List<PdfResponse>();
        foreach(var order in orders){
            var car=_context.Cars.FirstOrDefault(c=>c.Id==order.CarId);
            var package=_context.ServicePackages.FirstOrDefault(c=>c.Id==order.PackageId);
            var washer=_context.Users.FirstOrDefault(c=>c.Id==order.WasherId);
            //var user=_context.Users.FirstOrDefault(c=>c.Id==order.UserId);
            var price=_context.Orders.FirstOrDefault(c=>c.Status=="ACCEPTED"&&c.UserId==userId);
            var promo=_context.PromoCodes.FirstOrDefault(c=>c.Id==order.PromoCodeId);
            PdfResponse pdf=new PdfResponse{
                CarName=car.Model,
                PackageName=package.Name,
                WasherName=washer.UserName,
                UserName=user.UserName,
                Amount=price.TotalAmount,
                PromoCodeName=promo.Code

            };
            ls.Add(pdf);

        }
        return ls;
         }
    
}