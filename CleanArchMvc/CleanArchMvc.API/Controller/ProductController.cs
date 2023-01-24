using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductServiceComMediator _productService;

        public ProductController(IProductServiceComMediator productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetAllAsync();
            if (products is null) return NotFound("Products not found");
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int? id)
        {
            if (id is null) return NotFound();

            var productDTO = await _productService.GetByIdAsync(id);
            if (productDTO is null) return NotFound("Product not found");
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductDTO productDTO)
        {
            if (productDTO is null) return BadRequest("Invalid data");

            await _productService.CreateAsync(productDTO);
            return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int? id, ProductDTO productDTO)
        {
            if (id != productDTO.Id) return BadRequest();
            if (productDTO is null) return BadRequest();

            await _productService.UpdateAsync(productDTO);
            return Ok(productDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var productDTO = await _productService.GetByIdAsync(id);
            if (productDTO is null) return NotFound("Product not found");

            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}
