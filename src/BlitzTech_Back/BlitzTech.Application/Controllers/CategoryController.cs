using BlitzTech.Data.Context;
using BlitzTech.Data.Mapping;
using BlitzTech.Domain.Dtos.Category;
using BlitzTech.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlitzTech.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace BlitzTech.Application.Controllers
{
    [Route("API/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(DataContext context, ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var categoryModel = await _categoryRepo.GetAllAsync(query);
            var categoryDto = categoryModel.Select(s => s.ToCategoryDto());
            return Ok(categoryModel);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var categoryModel = await _categoryRepo.GetByIdAsync(id);

            if (categoryModel == null)
            {
                return NotFound("Nada encontrado :( ");
            }

            return Ok(categoryModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryRequestDto categoryDto)
        {
            var categoryModel = categoryDto.ToCategoryFromCreateDTO();
            await _categoryRepo.CreateAsync(categoryModel);
            return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, AutoMapperProfiles.ToCategoryDto(categoryModel));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateDto)
        {
            var categoryModel = await _categoryRepo.UpdateAsync(id, updateDto);

            if (categoryModel == null)
            {
                return NotFound("Id não encontrado :( ");
            }

            return Ok(categoryModel.ToCategoryDto());
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var categoryModel = await _categoryRepo.DeleteAsync(id);

            if (categoryModel == null)
            {
                return NotFound("Id não encontrado :( ");
            }

            return NoContent();
        }
    }
}
