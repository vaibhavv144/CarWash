using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Security.Claims;
using System.Threading.Tasks;

// [Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin")]

public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly UserManager<ApplicationUser> _userManager;


    public AdminController(IAdminService adminService, UserManager<ApplicationUser> userManager)
    {
        _adminService = adminService;
        _userManager = userManager;
    }


   


    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();


        var admin = await _userManager.FindByIdAsync(userId);
        if (admin == null) return NotFound();


        return Ok(new
        {
            admin.Id,
            admin.UserName,
            admin.Email,
            admin.PhoneNumber,
            admin.IsActive
        });
    }



    // [Authorize(Roles = "Admin")]
    [HttpGet("customers")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var users = await _adminService.GetAllCustomersAsync();
        return Ok(users);
    }


    [HttpGet("washers")]
    public async Task<IActionResult> GetAllWashers()
    {
        var users = await _adminService.GetAllWashersAsync();
        return Ok(users);
    }


    [HttpPost("washer")]
    // [HttpPut("washer")]
    public async Task<IActionResult> AddOrEditWasher([FromBody] WasherInputDto washerDto)
    {
        var result = await _adminService.AddOrEditWasherAsync(washerDto);
        return Ok(result);
    }

     [HttpGet("orders")]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await _adminService.GetAllOrdersAsync();
        return Ok(result);
    }


    [HttpGet("reportgenerate")]
    public async Task<IActionResult> GenerateReport(string userId)
    {
        List<PdfResponse> result = _adminService.reportGenerate(userId);
        //return Ok(result);

        //List<InvoiceResponseDTO> invoices = _dealerBL.GetAllInvoices(id);
        var email = await _userManager.FindByIdAsync(userId);
        using var memoryStream = new MemoryStream();
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var fontTitle = new XFont("Verdana", 20);
        var fontHeader = new XFont("Verdana", 10);
        var fontBody = new XFont("Verdana", 10);
        double y = 40;
        // Invoice Title
        gfx.DrawString("INVOICE", fontTitle, XBrushes.Black, new XRect(400, y, 200, 30), XStringFormats.TopLeft);
        y += 60;

        // var invoice = invoices.First();

        // Issued To
        gfx.DrawString("User's Mail:", fontHeader, XBrushes.Black, new XPoint(40, y));
        gfx.DrawString($"{email.Email}", fontBody, XBrushes.Black, new XRect(40, y + 15, 200, 60), XStringFormats.TopLeft);
        // Invoice Info
        gfx.DrawString($"INVOICE PRINT DATE: {DateTime.Now:MM.dd.yyyy}", fontHeader, XBrushes.Black, new XRect(350, y + 15, 200, 60), XStringFormats.TopLeft);
        y += 90;

        // Table Headers
        string[] headers = { "USER NAME", "PROMO CODE", "PACKAGE ", "TOTAL" };
        double[] colX = { 40, 120, 200, 470 };

        for (int i = 0; i < headers.Length; i++)
            gfx.DrawString(headers[i], fontHeader, XBrushes.Black, new XPoint(colX[i], y));
        y += 20;

        decimal subtotal = 0;
        foreach (var item in result)
        {
            string description = $" {item.UserName.ToString().Substring(0)}";
            decimal total = item.Amount;
            subtotal += total;
            gfx.DrawString(description, fontBody, XBrushes.Black, new XPoint(colX[0], y));
            gfx.DrawString(item.PromoCodeName, fontBody, XBrushes.Black, new XPoint(colX[1], y));
            gfx.DrawString(item.PackageName, fontBody, XBrushes.Black, new XPoint(colX[2], y));
            // gfx.DrawString(item.Amount.ToString("F2"), fontBody, XBrushes.Black, new XPoint(colX[2], y));

            gfx.DrawString(total.ToString("F2"), fontBody, XBrushes.Black, new XPoint(colX[3], y));
            y += 20;
            if (y > page.Height - 100)
            {
                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                y = 40;
            }
        }
        // Summary
        decimal tax = subtotal * 0.10m;
        decimal grandTotal = subtotal + tax;
        y += 20;
        gfx.DrawString($"TOTAL", fontHeader, XBrushes.Black, new XPoint(400, y));
        gfx.DrawString($"₹{subtotal:F2}", fontHeader, XBrushes.Black, new XPoint(470, y));
        // Signature
        y += 60;
        gfx.DrawString("CAR WASH Management Pvt Ltd.", fontBody, XBrushes.Black, new XPoint(400, y));
        gfx.DrawLine(XPens.Black, 400, y + 12, 540, y + 12); // signature line
        document.Save(memoryStream, false);
        return File(memoryStream.ToArray(), "application/pdf", $"Invoice_.pdf");
    }


    [HttpDelete("washer/{id}")]
    public async Task<IActionResult> DeleteWasher(string id)
    {
        var result = await _adminService.DeleteWasherAsync(id);
        return Ok(result);
    }




    

    // [HttpPut("user-status/{userId}")]
    // public async Task<IActionResult> ChangeUserStatus(string userId)
    // {
    //     var result = await _adminService.ToggleUserStatusAsync(userId);
    //     if (result == null) return NotFound("User not found");


    //     return Ok(result);
    // }




     // [HttpGet("dashboard")]
    // public async Task<IActionResult> GetDashboard()
    // {
    //     var data = await _adminService.GetDashboardAsync();
    //     return Ok(data);
    // }


}


