using guitarshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace guitarshop.Controllers
{
    public class ProductController : Controller
    {

        private readonly AspGuiznotesContext _context;
        public ProductController(AspGuiznotesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Route("Product/prd/{id:int}")]
        public IActionResult prd(int id)
        {
            CatalogProd product = _context.CatalogProds.FirstOrDefault(p => p.IdGuitar == id);
            if (product == null)
            {
                return NotFound(); 
            }
            return View(product);
        }
    }
}
