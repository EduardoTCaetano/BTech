using BlitzTech.Domain.Dtos.Category;
using BlitzTech.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlitzTech.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using BlitzTech.Data.Mapping;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzTech.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var categoryModel = await _categoryRepo.GetAllAsync(query);
            var categoryDto = categoryModel.Select(s => s.ToCategoryDto());
            return Ok(categoryDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var categoryModel = await _categoryRepo.GetByIdAsync(id);

            if (categoryModel == null)
            {
                return NotFound("Nada encontrado :(");
            }

            return Ok(categoryModel.ToCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryRequestDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = categoryDto.ToCategoryFromCreateDTO();
            await _categoryRepo.CreateAsync(categoryModel);
            return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, categoryModel.ToCategoryDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = await _categoryRepo.UpdateAsync(id, updateDto);

            if (categoryModel == null)
            {
                return NotFound("Id não encontrado :(");
            }

            return Ok(categoryModel.ToCategoryDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var categoryModel = await _categoryRepo.DeleteAsync(id);

            if (categoryModel == null)
            {
                return NotFound("Id não encontrado :(");
            }

            return NoContent();
        }
    }
}
