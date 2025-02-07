using guitarshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace guitarshop.Controllers
{
    public class CartController : Controller
    {
        private readonly AspGuiznotesContext _context;

        public CartController(AspGuiznotesContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Crt()
        {
            string userName = User.Identity.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Cab", "Autorize"); 
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.NameClient == userName); 

            if (user == null) 
            {
               
                return RedirectToAction("Cab", "Autorize"); 
            }

            int userId = user.IdUser; 

            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Crt");
        }


        public async Task<IActionResult> RemoveFromCart(int cartId)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Crt");
        }
    }
}


