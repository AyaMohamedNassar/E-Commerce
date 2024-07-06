using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(string role)
        {
            if (role != null)
            {
                IdentityRole identityRole = new IdentityRole { Name = role };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded) { return Ok(role); }
            }

            return BadRequest(new ApiResponse(401));
        }
    }
}
