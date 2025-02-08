using guitarshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace guitarshop.Controllers
{
    public class ProductController : Controller
    {

        private readonly AspGuiznotesContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(AspGuiznotesContext context, ILogger<ProductController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        [Route("Product/prd/{id:int}")]
        public IActionResult prd(int id)
        {
            var product = _context.CatalogProds
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.UserReview)
                .FirstOrDefault(p => p.IdGuitar == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewData["GuitarId"] = product.IdGuitar;


            return View(product);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReview(int GuitarId, int Rating, string ReviewText)
        {
            string userName = User.Identity.Name;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.NameClient == userName);

            int userId = user.IdUser;
            var review = new Review
            {
                GuitarId = GuitarId,
                UserReviewId = userId,  
                Rating = Rating,
                ReviewText = ReviewText
            };

           
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();


            return RedirectToAction("prd", "Product", new { id = GuitarId });
        }

    }
}