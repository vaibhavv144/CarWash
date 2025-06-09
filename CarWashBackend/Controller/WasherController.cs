
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// [Authorize(Roles = "Washer")]
[ApiController]
[Route("api/[controller]")]
public class WasherController : ControllerBase
{
    private readonly IWasherRepository _washerRepo;
    private readonly UserManager<ApplicationUser> _userManager;

    public WasherController(IWasherRepository washerRepo, UserManager<ApplicationUser> userManager)
    {
        _washerRepo = washerRepo;
        _userManager = userManager;
    }

    [HttpGet("profile/{washerId}")]
    public async Task<IActionResult> GetWasherProfile(string washerId)
    {
        var washer = await _userManager.FindByIdAsync(washerId);
        if (washer == null) return NotFound("Washer not found");

        var profile = new
        {
            washer.Id,
            washer.UserName,
            washer.Email,
            washer.PhoneNumber,
            washer.IsAvailable,
            washer.IsActive
        };

        return Ok(profile);
    }

    [HttpPut("update-profile/{washerId}")]
    public async Task<IActionResult> UpdateWasherProfile(string washerId, [FromBody] WasherUpdateDto updateDto)
    {
        var washer = await _userManager.FindByIdAsync(washerId);
        if (washer == null) return NotFound("Washer not found");

        washer.UserName = updateDto.UserName ?? washer.UserName;
        washer.Email = updateDto.Email ?? washer.Email;
        washer.PhoneNumber = updateDto.PhoneNumber ?? washer.PhoneNumber;
        washer.IsAvailable = updateDto.IsAvailable;
        washer.IsActive = updateDto.IsActive;

        var result = await _userManager.UpdateAsync(washer);
        if (!result.Succeeded) return BadRequest("Failed to update washer profile");

        return NoContent();
    }

    [HttpGet("available-orders")]
    public async Task<IActionResult> GetAvailableOrders()
    {
        var result = await _washerRepo.GetAvailableOrdersAsync();
        return Ok(result);
    }

    [HttpPost("accept/{orderId}")]
    public async Task<IActionResult> AcceptOrder(int orderId)
    {
        var washerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _washerRepo.AcceptOrderAsync(orderId, washerId);
        return Ok(result);
    }

    [HttpPost("reject/{orderId}")]
    public async Task<IActionResult> RejectOrder(int orderId)
    {
        var result = await _washerRepo.RejectOrderAsync(orderId);
        return Ok(result);
    }

    [HttpGet("current-orders")]
    public async Task<IActionResult> GetCurrentOrders()
    {
        var washerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _washerRepo.GetWasherOrdersAsync(washerId, new List<string> { "ACCEPTED", "INPROCESS" });
        return Ok(result);
    }

    // [HttpGet("past-orders")]
    // public async Task<IActionResult> GetPastOrders()
    // {
    //     var washerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //     var result = await _washerRepo.GetWasherOrdersAsync(washerId, new List<string> { "COMPLETED", "CANCELLED" });
    //     return Ok(result);
    // }

    [Authorize(Roles = "Washer")]
    [HttpPut("update-status/{orderId}")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, string status, string imageUrl)
    {
        var washerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _washerRepo.UpdateOrderStatusAsync(orderId, status, imageUrl, washerId);
        if (!result) return NotFound();
        return Ok("Status Updated");
    }
}
