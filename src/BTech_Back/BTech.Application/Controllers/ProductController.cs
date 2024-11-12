using BlitzTech.Domain.Dtos.Product;
using BlitzTech.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlitzTech.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using BlitzTech.Data.Mapping;
using BlitzTech.Domain.DTOs.Product;

namespace BlitzTech.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly BlobStorageService _blobStorageService;

        public ProductController(IProductRepository productRepo, BlobStorageService blobStorageService)
        {
            _productRepo = productRepo;
            _blobStorageService = blobStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var productModel = await _productRepo.GetAllAsync(query);
            var productDto = productModel.Select(p => p.ToProductDto());
            return Ok(productDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var productModel = await _productRepo.GetByIdAsync(id);

            if (productModel == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(productModel.ToProductDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductRequestDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = productDto.ToProductFromCreateDTO();
            await _productRepo.CreateAsync(productModel);
            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = await _productRepo.UpdateAsync(id, updateDto);

            if (productModel == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(productModel.ToProductDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var productModel = await _productRepo.DeleteAsync(id);

            if (productModel == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return NoContent();
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory([FromRoute] Guid categoryId)
        {
            var products = await _productRepo.GetByCategoryAsync(categoryId);

            if (products == null || !products.Any())
            {
                return NotFound("Nenhum produto encontrado para esta categoria.");
            }

            var productDtos = products.Select(p => p.ToProductDto());
            return Ok(productDtos);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string name)
        {
            var queryObject = new QueryObject { Name = name };
            var products = await _productRepo.GetAllAsync(queryObject);

            if (!products.Any())
            {
                return NotFound("Nenhum produto encontrado.");
            }

            var productDtos = products.Select(p => p.ToProductDto());
            return Ok(productDtos);
        }

        // New endpoint to upload product image
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadProductImage([FromRoute] Guid id, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Ensure the file is an image
            if (!file.ContentType.StartsWith("image"))
            {
                return BadRequest("Only image files are allowed.");
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // Generate unique filename
            using (var stream = file.OpenReadStream())
            {
                // Upload the image to Azure Blob Storage
                var fileUrl = await _blobStorageService.UploadFileAsync(stream, fileName);

                // Update the product with the image URL (assuming Product has an ImageUrl property)
                var product = await _productRepo.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound("Produto não encontrado.");
                }

                product.Image = fileUrl; // Store the image URL in the product

                await _productRepo.UpdateAsync(id, product.ToUpdateProductDto()); // Save the updated product
                return Ok(new { FileUrl = fileUrl });
            }
        }
    }
}
