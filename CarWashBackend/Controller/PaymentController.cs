// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;


// [ApiController]
// [Route("api/[controller]")]
// public class PaymentController : ControllerBase
// {
//     private readonly IPaymentService _paymentService;


//     public PaymentController(IPaymentService paymentService)
//     {
//         _paymentService = paymentService;
//     }


//     [HttpPost]
//     [Authorize(Roles = "User")]
//     public async Task<IActionResult> MakePayment([FromBody] PaymentRequestDto dto)
//     {
//         try
//         {
            
//             var result = await _paymentService.MakePaymentAsync(dto);
//             return Ok(new
//             {
//                 Success = true,
//                 Message = "Payment successful",
//                 Receipt = result
//             });
//         }
//         catch (Exception ex)
//         {
//             return BadRequest(new { Success = false, Message = ex.Message });
//         }
//     }
// }


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentController(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> MakePayment([FromBody] PaymentRequestDto dto)
    {
        try
        {
            var result = await _paymentRepository.MakePaymentAsync(dto);
            return Ok(new
            {
                Success = true,
                Message = "Payment successful",
                Receipt = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }
}
