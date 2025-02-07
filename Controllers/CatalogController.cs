using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using guitarshop.Models; 


namespace guitarshop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AspGuiznotesContext _context;

        public CatalogController(AspGuiznotesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Ctlg(string searchString, string sortOrder, int? typeId, int? formId, string clearFilters)
        {
           
            var guitars = _context.CatalogProds
                .Include(g => g.GuitarType)
                .Include(g => g.GuitarForm)
                .AsQueryable();


            if (!string.IsNullOrEmpty(clearFilters))
            {
                searchString = null;
                typeId = null;
                formId = null;
                sortOrder = null;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                guitars = guitars.Where(g => g.NameGuitar.ToLower().Contains(searchString.ToLower()));
            }

           
            if (typeId.HasValue && typeId.Value != 0) 
            {
                guitars = guitars.Where(g => g.GuitarTypeId == typeId.Value);
            }

       
            if (formId.HasValue && formId.Value != 0)
            {
                guitars = guitars.Where(g => g.GuitarFormId == formId.Value);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    guitars = guitars.OrderByDescending(g => g.NameGuitar);
                    break;
                case "price_asc":
                    guitars = guitars.OrderBy(g => g.GuitarPrice);
                    break;
                case "price_desc":
                    guitars = guitars.OrderByDescending(g => g.GuitarPrice);
                    break;
                default:
                    guitars = guitars.OrderBy(g => g.NameGuitar);
                    break;
            }

          
            var types = new SelectList(await _context.TypeGuitars.Select(t => new { t.IdType, t.TypeName }).ToListAsync(), "IdType", "TypeName", typeId);
            var forms = new SelectList(await _context.GuitarForms.Select(f => new { f.IdForm, f.FormName }).ToListAsync(), "IdForm", "FormName", formId);

      
            var viewModel = new CartItems
            {
                Guitars = await guitars.ToListAsync(),
                Types = types,
                Forms = forms,
                SearchString = searchString,
                GuitarTypeId = typeId,
                GuitarFormId = formId,
                NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc",
                PriceSortParm = sortOrder == "price_asc" ? "price_desc" : "price_asc",
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            bool isAuth = User.Identity.IsAuthenticated;

            if (!isAuth)
            {
                return RedirectToAction("Cab", "Autorize"); 
            }

            var userName = User.Identity.Name;

            if (userName != null)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.NameClient == userName); 

                if (user != null)
                {
                    int userId = user.IdUser; 

                    var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == id);
                    if (cartItem != null)
                    {
                        cartItem.Quantity++;
                    }
                    else
                    {
                        _context.Carts.Add(new Cart { UserId = userId, ProductId = id, Quantity = 1 });
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("Ctlg");
        }

    }
}
