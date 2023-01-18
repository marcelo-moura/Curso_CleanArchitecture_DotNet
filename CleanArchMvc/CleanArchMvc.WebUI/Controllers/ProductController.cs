using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductController : Controller
    {
        //private readonly IProductService _productService;
        private readonly IProductServiceComMediator _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductServiceComMediator productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categoriesDTO = await _categoryService.GetAllAsync();
            ViewBag.CategoryId = new SelectList(categoriesDTO, "Id", "Name");
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
    }
}
