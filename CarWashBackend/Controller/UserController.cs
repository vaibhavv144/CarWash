using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// [Authorize(Roles = "Washer")]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ICarRepository _carRepository;

    public UserController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    [HttpPost("AddCar")]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> AddCar([FromBody] CarAddDTO carDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Success = false, Message = "User not authenticated." });
        }

        await _carRepository.AddCarAsync(carDto, userId);

        return Ok(new { Success = true, Message = "Car added successfully." });
    }

    [HttpGet("GetAllCar")]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> GetCar()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Success = false, Message = "User not authenticated." });
        }

        var cars = _carRepository.GetCars(userId);
        return Ok(new { Message = cars });
    }

    [HttpPut("UpdateCar")]
    //[Authorize(Roles = "User")]
    public IActionResult EditDetails(int id, CarAddDTO carDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Success = false, Message = "User not authenticated." });
        }

        bool result = _carRepository.EditDetails(userId, carDto, id);
        return Ok(new { Success = result, Message = result ? "Car details updated successfully." : "Car not found." });
    }

    [HttpDelete("DeleteCar")]
    //[Authorize(Roles = "User")]
    public IActionResult DeleteCar(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Success = false, Message = "User not authenticated." });
        }

        bool result = _carRepository.DeleteCar(userId, id);

        if (result)
        {
            return Ok(new { Success = true, Message = "Car deleted successfully." });
        }

        return BadRequest(new { Success = false, Message = "Car not found or unable to delete." });
    }
}
