using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using E_CommerceAPI.DTOs;
using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string _targetFilePath;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ProductController(IUnitOfWork unitOfWork, 
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           _hostEnvironment = hostEnvironment;
            _targetFilePath = Path.Combine(_hostEnvironment.WebRootPath, "Images");
        }

        
        [HttpGet("All")]
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> All([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync(page,pageSize);

            if (products == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products));
        }

        [HttpGet("Count")]
        public async Task<ActionResult<int>> Count()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            return Ok(products.Count);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id )
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
                return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductDTO>(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest(new ApiResponse(400));

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
                return NotFound(new ApiResponse(404));

            _unitOfWork.ProductRepository.Delete(id);
            _unitOfWork.SaveChanges();

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost("Add")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductUploadDTO productDto)
        {
            if (productDto.Image == null || productDto.Image.Length == 0)
            {
                return BadRequest("No image file selected.");
            }

            var fileName = Path.GetRandomFileName() + Path.GetExtension(productDto.Image.FileName);
            var filePath = Path.Combine(_targetFilePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productDto.Image.CopyToAsync(stream);
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                Image = fileName,
                BrandId = productDto.BrandId,
                CategoryId = productDto.CategoryId,
                StockQuantity = productDto.StockQuantity
            };

             _unitOfWork.ProductRepository.Insert(product);
             _unitOfWork.SaveChanges();

            var productDTO =  _mapper.Map<Product, ProductDTO>(product);
            return Ok(new { Message = "Product created successfully!", productDTO });
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromForm] ProductUploadDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400));

            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                BrandId = productDto.BrandId,
                CategoryId = productDto.CategoryId,
                StockQuantity = productDto.StockQuantity
            };

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.SaveChanges();

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost("EditProductPhoto")]
        public async Task<IActionResult> EditProductPhoto([FromForm] ProductUploadDTO productDto)
        {
            if (productDto.Image == null || productDto.Image.Length == 0)
            {
                return BadRequest("No image file selected.");
            }

            var productdb = await _unitOfWork.ProductRepository.GetByIdAsync(productDto.Id);

            if (productdb == null)
            {
                return NotFound("Product not found.");
            }

            // Delete the old image
            if (!string.IsNullOrEmpty(productdb.Image))
            {
                var oldFilePath = Path.Combine(_targetFilePath, productdb.Image);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            var fileName = Path.GetRandomFileName() + Path.GetExtension(productDto.Image.FileName);
            var filePath = Path.Combine(_targetFilePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productDto.Image.CopyToAsync(stream);
            }

            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                Image = fileName,
                BrandId = productDto.BrandId,
                CategoryId = productDto.CategoryId,
                StockQuantity = productDto.StockQuantity
            };


            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.SaveChanges();

            var productDTO = _mapper.Map<Product, ProductDTO>(product);
            return Ok(new { Message = "Product Updated successfully!", productDTO });
        }
    }
}
