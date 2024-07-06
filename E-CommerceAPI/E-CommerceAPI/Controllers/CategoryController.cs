using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using E_CommerceAPI.DTOs;
using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("All")]
        public async Task<ActionResult<IReadOnlyList<CategoryDTO>>> All()
        {
            var category = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (category == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDTO>>(category));
        }
    }
}
