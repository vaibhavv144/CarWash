

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RatingRepository : IRatingRepository
{
    private readonly CarWashContext _context;

    public RatingRepository(CarWashContext context)
    {
        _context = context;
    }

    public async Task<bool> AddRatingAsync(string userId, RatingDto dto)
    {
        var order = await _context.Orders.FindAsync(dto.OrderId);
        if (order == null || order.UserId != userId || order.WasherId != dto.WasherId)
            return false;

        var rating = new Rating
        {
            OrderId = dto.OrderId,
            WasherId = dto.WasherId,
            RatingValue = dto.RatingValue,
            UserId = userId
        };

        await _context.Ratings.AddAsync(rating);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Rating>> GetRatingsByWasherIdAsync(string washerId)
    {
        return await _context.Ratings
            .Where(r => r.WasherId == washerId)
            .Include(r => r.User)
            .Include(r => r.Order)
            .ToListAsync();
    }
}
