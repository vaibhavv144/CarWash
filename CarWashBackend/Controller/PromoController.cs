

// using Microsoft.AspNetCore.Mvc;

// [ApiController]

// [Route("api/[controller]")]


// public class PromoCodeController :ControllerBase{


//     private readonly IPromocodeService _promoPackageService;

//    public  PromoCodeController(IPromocodeService promoCodeService){
//         _promoPackageService=promoCodeService;
//     }

//     [HttpGet()]

//      public async Task<ActionResult<IEnumerable<ServicePackage>>> GetAllPromo()
//         {
//             var packages = await _promoPackageService.GetAllPromoAsync();
//             return Ok(packages);
//         }

//      [HttpPost]
//         public async Task<ActionResult<ServicePackage>> AddNewPackage([FromBody] PromoCode promoCode)
//         {
//             var addedPackage = await _promoPackageService.AddPromoAsync(promoCode);
//             return CreatedAtAction(nameof(GetAllPromo),  addedPackage);
//         }

// }


using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PromoCodeController : ControllerBase
{
    private readonly IPromoCodeRepository _promoCodeRepository;

    public PromoCodeController(IPromoCodeRepository promoCodeRepository)
    {
        _promoCodeRepository = promoCodeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PromoCode>>> GetAllPromo()
    {
        var promoCodes = await _promoCodeRepository.GetAllPromoCodeAsync();
        return Ok(promoCodes);
    }

    [HttpPost]
    public async Task<ActionResult<PromoCode>> AddNewPromoCode([FromBody] PromoCode promoCode)
    {
        var addedPromoCode = await _promoCodeRepository.AddPromoAsync(promoCode);
        return CreatedAtAction(nameof(GetAllPromo), new { id = addedPromoCode.Id }, addedPromoCode);
    }

    [HttpPut("{promocodeId}")]
    public async Task<ActionResult<PromoCode>> UpdatePromoCode(int promocodeId, [FromBody] PromoCode promoCode)
    {
        var updatedPromoCode = await _promoCodeRepository.UpdatePromoAsync(promocodeId, promoCode);
        if (updatedPromoCode == null)
        {
            return NotFound();
        }
        return Ok(updatedPromoCode);
    }

    [HttpDelete("{promoCodeId}")]
    public async Task<IActionResult> DeletePromoCode(int promoCodeId)
    {
        var result = await _promoCodeRepository.DeletePromoAsync(promoCodeId);
        if (!result) return NotFound("Promo code not found.");

        return NoContent();
    }

}
