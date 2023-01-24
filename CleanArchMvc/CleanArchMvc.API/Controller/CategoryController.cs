using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories is null) return NotFound("Categories not found");
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int? id)
        {
            if (id is null) return NotFound();

            var categoryDTO = await _categoryService.GetByIdAsync(id);
            if (categoryDTO is null) return NotFound("Category not found");
            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoryDTO categoryDTO)
        {
            if (categoryDTO is null) return BadRequest("Invalid data");

            await _categoryService.CreateAsync(categoryDTO);
            return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.Id }, categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int? id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id) return BadRequest();
            if (categoryDTO is null) return BadRequest();

            await _categoryService.UpdateAsync(categoryDTO);
            return Ok(categoryDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var categoryDTO = await _categoryService.GetByIdAsync(id);
            if (categoryDTO is null) return NotFound("Category not found");

            await _categoryService.DeleteAsync(id);
            return Ok();
        }
    }
}
