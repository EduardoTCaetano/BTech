using BlitzTech.Data.Context;
using BlitzTech.Domain.Dtos.Product;
using BlitzTech.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlitzTech.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Data.Mapping;
using BlitzTech.Domain.DTOs.Product;

namespace BlitzTech.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
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

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductRequestDto productDto)
        {
            var productModel = productDto.ToProductFromCreateDTO();
            await _productRepo.CreateAsync(productModel);
            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequestDto updateDto)
        {
            var productModel = await _productRepo.UpdateAsync(id, updateDto);

            if (productModel == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(productModel.ToProductDto());
        }

        [Authorize(Policy = "AdminOnly")]
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

    }
}
