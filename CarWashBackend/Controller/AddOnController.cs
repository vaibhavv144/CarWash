using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Admin")]
public class AddOnController : ControllerBase
{
    private readonly IAddOnRepository _addOnRepository;

    public AddOnController(IAddOnRepository addOnRepository)
    {
        _addOnRepository = addOnRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAddOns()
    {
        var result = await _addOnRepository.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddAddOn([FromBody] AddOnCreateDto addOnDto)
    {
        var result = await _addOnRepository.AddAsync(addOnDto);
        return Ok(result);
    }

    [HttpPut("{addOnId}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAddOn(int addOnId, [FromBody] AddOnCreateDto addOnDto)
    {
        var result = await _addOnRepository.UpdateAsync(addOnId, addOnDto);
        if (result == null) return NotFound("AddOn not found.");
        return Ok(result);
    }

    [HttpDelete("{addOnId}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAddOn(int addOnId)
    {
        var result = await _addOnRepository.DeleteAsync(addOnId);
        if (!result) return NotFound("AddOn not found.");
        return NoContent();
    }

}

