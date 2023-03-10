using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductController : Controller
    {
        //private readonly IProductService _productService;
        private readonly IProductServiceComMediator _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductServiceComMediator productService, ICategoryService categoryService,
                                 IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var productDTO = await _productService.GetByIdAsync(id);
            if (productDTO is null) return NotFound();

            var wwwrootPath = _environment.WebRootPath;
            var image = Path.Combine(wwwrootPath, "images\\" + productDTO.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(productDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await FillViewBagCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateAsync(productDTO);
                return RedirectToAction("Index");
            }
            return View(productDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var productDTO = await _productService.GetByIdAsync(id);
            if (productDTO is null) return NotFound();

            await FillViewBagCategories();
            return View(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(productDTO);
                return RedirectToAction("Index");
            }
            return View(productDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var productDTO = await _productService.GetByIdAsync(id);
            if (productDTO is null) return NotFound();

            return View(productDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private async Task FillViewBagCategories()
        {
            var categoriesDTO = await _categoryService.GetAllAsync();
            ViewBag.CategoryId = new SelectList(categoriesDTO, "Id", "Name");
        }
    }
}
