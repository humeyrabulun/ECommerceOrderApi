using Microsoft.AspNetCore.Mvc;
using ECommerceOrderApi.Models;
using ECommerceOrderApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task <IActionResult> GetAll()

        {
            var products = await _productService.GetAllProductAsync();
            return Ok(products);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(new {message = $"Product with ID {id} not found." });

            }
            return Ok(product);
         
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product newProduct)
        {
            var createdProduct = await _productService.CreateProductAsync(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
        {
            
            if (id != updatedProduct.Id)
            {
                return BadRequest(new { message = "URL'deki ID ile gövdedeki (Body) ürün ID'si uyuşmuyor!" });
            }

          
            var isUpdated = await _productService.UpdateProductAsync(id, updatedProduct);

            
            if (!isUpdated)
            {
                return NotFound(new { message = $"{id} numaralı ürün bulunamadı!" });
            }

            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }
            return NoContent();
        }
    }
}
