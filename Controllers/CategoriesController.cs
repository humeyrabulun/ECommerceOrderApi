using Microsoft.AspNetCore.Mvc;
using ECommerceOrderApi.Models;
using ECommerceOrderApi.Services;

namespace ECommerceOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {

        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductsByMinRating(
            [FromRoute] int id,
            [FromQuery] int minRating = 0)
        {

            var products = await _categoryService.GetProductsWithMinRatingAsync(minRating, id);
            if (products.Count == 0)
            {
                return NotFound(new { message = "bu kritere uygun ürün bulunamadı" });
            }
            return Ok(products);
        }
    }
}
