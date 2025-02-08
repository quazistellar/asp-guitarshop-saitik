using guitarshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace guitarshop.Components
{
    public class AverageRatingViewComponent: ViewComponent
    {
        private readonly AspGuiznotesContext _context;

        public AverageRatingViewComponent(AspGuiznotesContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int GuitarId)
        {
            var reviews = await _context.Reviews.Where(r => r.GuitarId == GuitarId).ToListAsync();

            double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

            return View("Rating", averageRating);
        }
    }
}
