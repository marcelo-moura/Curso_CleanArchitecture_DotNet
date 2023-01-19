using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var categoryDTO = await _categoryService.GetByIdAsync(id);
            if (categoryDTO is null) return NotFound();

            return View(categoryDTO);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(categoryDTO);
                return RedirectToAction("Index");
            }

            return View(categoryDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var categoryDTO = await _categoryService.GetByIdAsync(id);
            if (categoryDTO is null) return NotFound();

            return View(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(categoryDTO);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var categoryDTO = await _categoryService.GetByIdAsync(id);
            if (categoryDTO is null) return NotFound();

            return View(categoryDTO);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
