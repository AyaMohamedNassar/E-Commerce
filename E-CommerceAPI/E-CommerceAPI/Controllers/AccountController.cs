using Core.Entities;
using Core.Interfaces;
using E_CommerceAPI.DTOs;
using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;

namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ItokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ItokenService tokenService,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            if (CheckEmailExixtAsync(register.Email).Result.Value) {
                return new BadRequestObjectResult(new ApiValidation
                {
                    Errors = new [] {"Email Address Aready in use."}
                });
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, register.Password);

                if (!result.Succeeded) { return BadRequest(new ApiResponse(400)); }

                await _userManager.AddToRoleAsync(user, "User");

                await _signInManager.SignInAsync(user, isPersistent: false);
                
                return Ok(new UserDTO
                {
                    Email = user?.Email,
                    UserName = user?.UserName,
                    Token = await _tokenService.CreateToken(user)
                }); 
            }

            return BadRequest(new ApiResponse(400)); 
           
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> login(LoginDTO login)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null) { return Unauthorized(new ApiResponse(401)); }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            var role = await _unitOfWork.RoleRepository.GetUserRole(user.Id);


            return Ok(new UserDTO {
                Email = user?.Email,
                UserName = user?.UserName,
                Token = await _tokenService.CreateToken(user),
                Role = role
            });
            // check mvc
        }

        [Authorize]
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDTO
            {
                UserName = user.UserName,
                Email = email,
                Token = await _tokenService.CreateToken(user)
            });
        }

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExixtAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
