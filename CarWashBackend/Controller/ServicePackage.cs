// using Microsoft.AspNetCore.Mvc;


//  [ApiController]

// [Route("api/[controller]")]
   
//     public class ServicePackageController : ControllerBase
//     {
//         private readonly IServicePackageService _servicePackageService;


//         public ServicePackageController(IServicePackageService servicePackageService)
//         {
//             _servicePackageService = servicePackageService;
//         }


//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<ServicePackage>>> GetAllPackages()
//         {
//             var packages = await _servicePackageService.GetAllPackagesAsync();
//             return Ok(packages);
//         }


//         [HttpPost]
//         public async Task<ActionResult<ServicePackage>> AddNewPackage([FromBody] ServicePackage package)
//         {
//             var addedPackage = await _servicePackageService.AddPackageAsync(package);
//             return CreatedAtAction(nameof(GetAllPackages),  addedPackage);
//         }


//         [HttpPut("{packageId}")]
//         public async Task<IActionResult> UpdatePackage(int packageId, [FromBody] ServicePackage package)
//         {
//             var updatedPackage = await _servicePackageService.UpdatePackageAsync(packageId, package);
//             if (updatedPackage == null)
//                 return NotFound();


//             return NoContent();
//         }


//         [HttpPut("deactivate/{packageId}")]
//         public async Task<IActionResult> DeactivatePackage(int packageId)
//         {
//             var result = await _servicePackageService.DeactivatePackageAsync(packageId);
//             if (!result)
//                 return NotFound();


//             return NoContent();
//         }
//     }



using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ServicePackageController : ControllerBase
{
    private readonly IServicePackageRepository _servicePackageRepository;

    public ServicePackageController(IServicePackageRepository servicePackageRepository)
    {
        _servicePackageRepository = servicePackageRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServicePackage>>> GetAllPackages()
    {
        var packages = await _servicePackageRepository.GetAllPackagesAsync();
        return Ok(packages);
    }

    [HttpPost]
    public async Task<ActionResult<ServicePackage>> AddNewPackage([FromBody] ServicePackage package)
    {
        var addedPackage = await _servicePackageRepository.AddPackageAsync(package);
        return CreatedAtAction(nameof(GetAllPackages), new { id = addedPackage.Id }, addedPackage);
    }

    [HttpPut("{packageId}")]
    public async Task<IActionResult> UpdatePackage(int packageId, [FromBody] ServicePackage package)
    {
        var updatedPackage = await _servicePackageRepository.UpdatePackageAsync(packageId, package);
        if (updatedPackage == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{packageId}")]
public async Task<IActionResult> DeletePackage(int packageId)
{
    var result = await _servicePackageRepository.DeletePackageAsync(packageId);
    if (!result)
        return NotFound();

    return Ok(new { success = true });
}


    // [HttpPut("deactivate/{packageId}")]
    // public async Task<IActionResult> DeactivatePackage(int packageId)
    // {
    //     var result = await _servicePackageRepository.DeactivatePackageAsync(packageId);
    //     if (!result)
    //         return NotFound();

    //     return NoContent();
    // }
}
