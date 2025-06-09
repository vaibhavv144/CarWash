using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuth _authRepository;
    private readonly IJwtTokenHelper _jwtTokenHelper;
    private readonly UserManager<ApplicationUser> _userManager;
   


    public AccountController(IAuth authRepository, IJwtTokenHelper jwtTokenHelper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _authRepository = authRepository;
        _jwtTokenHelper = jwtTokenHelper;
        _userManager = userManager;
        
    }

    

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerRequestDto)
    {
        var result = await _authRepository.RegisterAsync(registerRequestDto);


        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { Success = false, Message = "Registration failed.", Errors = errors });
        }

        

        return Created("User Registered", new { Success = true, Message = "User registered successfully. Please login." });
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var user = await _authRepository.LoginAsync(loginDto);


        if (user == null)
        {
            return Unauthorized(new { Success = false, Message = "Invalid username or password." });
        }


        var roles = await _userManager.GetRolesAsync(user);
        string jwtToken = _jwtTokenHelper.GenerateToken(user.Id,loginDto.Username, roles.ToList());
        return Ok(new { Success = true, Message = "Login Successful.", Token = jwtToken });
    }
}