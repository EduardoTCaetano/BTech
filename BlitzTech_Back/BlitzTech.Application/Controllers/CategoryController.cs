using BlitzTech.Data.Context;
using BlitzTech.Data.Mapping;
using BlitzTech.Data.Migrations;
using BlitzTech.Domain.Dtos.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlitzTech.Application.Controllers
{
    [Route("API/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryModel = await _context.Categories.ToListAsync();

            var categoryDto =  categoryModel.Select(s => s.ToCategoryDto());
            return Ok(categoryModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var categoryModel = _context.Categories.FindAsync(id);

            if (categoryModel == null)
            {
                return NotFound("Nada encontrado :( ");
            }

            return Ok(categoryModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryRequestDto categoryDto)
        {
            var categoryModel = categoryDto.ToCategoryFromCreateDTO();
            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, AutoMapperProfiles.ToCategoryDto(categoryModel));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateDto)
        {
            var categoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryModel == null)
            {
                return NotFound("Id não encontrado :( ");
            }
            categoryModel.Description = updateDto.Description;
            categoryModel.IsActive = updateDto.IsActive;

            await _context.SaveChangesAsync();
            return Ok(categoryModel.ToCategoryDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var categoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryModel == null)
            {
                return NotFound("Id não encontrado :( ");
            }

            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}