using ECommerceOrderApi.Models;
using ECommerceOrderApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOrderApi.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductWithReviewDto>> GetProductsWithMinRatingAsync(int minRating,int categroyId)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .Include(C=>C.Products)
                    .ThenInclude(p=> p.Reviews)
                .FirstOrDefaultAsync(c => c.Id == categroyId);

            if(category == null)
            {
                return new List<ProductWithReviewDto>();

            }

            var result= category.Products

                .Where(p=> p.Reviews.Any() && p.Reviews.Average(r=>r.Rating)>=minRating)
                .Select(p => new ProductWithReviewDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Price = p.Price,
                    AverageRating = p.Reviews.Average(r => r.Rating),
                    Comments = p.Reviews.Select(r => r.Comment).ToList()
                })
                .ToList();

            return result;


        }
    }
}
