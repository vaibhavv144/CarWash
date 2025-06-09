using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpPost("book-now")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> BookNow([FromBody] OrderRequestDTO orderRequestDTO)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Success = false, Message = "User not authenticated." });
        }

        var order = await _orderRepository.BookNowAsync(orderRequestDTO, userId);
        return Ok(order);
    }

    [HttpPost("schedule")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Schedule([FromBody] CreateOrderDto createOrderDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Success = false, Message = "User not authenticated." });
        }

        var order = await _orderRepository.ScheduleAsync(createOrderDto, userId);
        return Ok(order);
    }

    [HttpGet("user-orders/{userId}")]
    public async Task<IActionResult> GetUserOrders(string userId)
    {
        var orders = await _orderRepository.GetUserOrdersAsync(userId);
        return Ok(orders);
    }

    [HttpPut("cancel/{orderId}")]
    public async Task<IActionResult> CancelOrder(int orderId)
    {
        var result = await _orderRepository.CancelOrderAsync(orderId);
        if (!result) return NotFound();
        return Ok("Order Canceled");
    }
}
