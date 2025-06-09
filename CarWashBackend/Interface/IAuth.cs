
using Microsoft.AspNetCore.Identity;

public interface IAuth
    {
        Task<ApplicationUser> LoginAsync(LoginDTO loginDto);
        Task<IdentityResult> RegisterAsync(RegisterDTO signUpDto);
    }



 
