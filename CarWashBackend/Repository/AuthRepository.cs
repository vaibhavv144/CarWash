
using Microsoft.AspNetCore.Identity;

    public class AuthRepository : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISMTP _smtp;

        public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,ISMTP smtp)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _smtp = smtp;
        }

        public async Task<ApplicationUser> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
           
            return result.Succeeded ? user : null;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO signUpDto)
        {
            
            var user = new ApplicationUser
            {
                UserName = signUpDto.Username,
                Email = signUpDto.Email
            };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);
            if (result.Succeeded && signUpDto.Roles != null && signUpDto.Roles.Any())
            {
                result = await _userManager.AddToRolesAsync(user, signUpDto.Roles);
            }
            string message = "Registration successful";
             await _smtp.SendEmailAsync(signUpDto.Email,"CarWash.PVT LTD",message);

            return result;
        }
    }

