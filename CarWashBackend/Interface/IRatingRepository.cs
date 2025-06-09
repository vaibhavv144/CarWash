
public interface IRatingRepository
{
    Task<bool> AddRatingAsync(string userId, RatingDto dto);
    Task<IEnumerable<Rating>> GetRatingsByWasherIdAsync(string washerId);
}
