

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly IRatingRepository _ratingRepository;

    public RatingController(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    [HttpPost("submit")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> SubmitRating([FromBody] RatingDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Success = false, Message = "User not authenticated." });
        }

        var result = await _ratingRepository.AddRatingAsync(userId, dto);
        if (!result)
            return BadRequest("Invalid rating data or not allowed.");

        return Ok("Rating submitted.");
    }

    [HttpGet("washer/{washerId}")]
    [Authorize(Roles = "Washer,Admin,User")]
    public async Task<IActionResult> GetRatingsForWasher(string washerId)
    {
        var ratings = await _ratingRepository.GetRatingsByWasherIdAsync(washerId);
        return Ok(ratings);
    }
}
