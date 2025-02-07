using guitarshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace guitarshop.Controllers
{
    public class CRUDController : Controller
    {
        private AspGuiznotesContext db;

        public CRUDController(AspGuiznotesContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Crd()
        {
            return View(await db.CatalogProds.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CatalogProd catalog)
        {
            try
            {
                db.CatalogProds.Add(catalog);
                await db.SaveChangesAsync();
                return RedirectToAction("Crd");
            }
            catch (Exception ex) {

             return RedirectToAction("Crd");
            }


        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                CatalogProd catalog = await db.CatalogProds.FirstOrDefaultAsync(p => p.IdGuitar == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(CatalogProd catalog)
        {
            db.Update(catalog);
            await db.SaveChangesAsync();
            return RedirectToAction("Crd");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                CatalogProd form = await db.CatalogProds.FirstOrDefaultAsync(p => p.IdGuitar == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                CatalogProd catalog = await db.CatalogProds.FirstOrDefaultAsync(p => p.IdGuitar == id);
                if (catalog != null)
                {
                    db.CatalogProds.Remove(catalog);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Crd");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                CatalogProd catalog = await db.CatalogProds.FirstOrDefaultAsync
                   (p => p.IdGuitar == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }


        //пользователи
        public async Task<IActionResult> CrdUser()
        {
            return View(await db.Users.ToListAsync());
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        private string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";

            using (var sha256 = SHA256.Create())
            {

                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User users)
        {
            users.PasswordUser = HashPassword(users.PasswordUser);
            db.Users.Add(users);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdUser");
        }

        public async Task<IActionResult> UpdateUser(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.IdUser == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {
            user.PasswordUser = HashPassword(user.PasswordUser);
            db.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdUser");
        }

        [HttpGet]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> ConfirmDeleteUser(int? id)
        {
            if (id != null)
            {
                User form = await db.Users.FirstOrDefaultAsync(p => p.IdUser == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.IdUser == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("CrdUser");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DetailsUsers(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.IdUser == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }


        // типы
        public async Task<IActionResult> CrdTypes()
        {
            return View(await db.TypeGuitars.ToListAsync());
        }

        public IActionResult CreateType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateType(TypeGuitar type)
        {
            db.TypeGuitars.Add(type);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdTypes");
        }
        public async Task<IActionResult> UpdateType(int? id)
        {
            if (id != null)
            {
                TypeGuitar catalog = await db.TypeGuitars.FirstOrDefaultAsync(p => p.IdType == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateType(TypeGuitar catalog)
        {
            db.Update(catalog);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdTypes");
        }

        [HttpGet]
        [ActionName("DeleteType")]
        public async Task<IActionResult> ConfirmDeleteType(int? id)
        {
            if (id != null)
            {
                TypeGuitar form = await db.TypeGuitars.FirstOrDefaultAsync(p => p.IdType == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteType(int? id)
        {
            if (id != null)
            {
                TypeGuitar catalog = await db.TypeGuitars.FirstOrDefaultAsync(p => p.IdType == id);
                if (catalog != null)
                {
                    db.TypeGuitars.Remove(catalog);
                    await db.SaveChangesAsync();
                    return RedirectToAction("CrdTypes");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DetailsType(int? id)
        {
            if (id != null)
            {
                TypeGuitar catalog = await db.TypeGuitars.FirstOrDefaultAsync(p => p.IdType == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }


        // роли
        public async Task<IActionResult> CrdRole()
        {
            return View(await db.Roles.ToListAsync());
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(Role role)
        {
            db.Roles.Add(role);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdRole");
        }
        public async Task<IActionResult> UpdateRole(int? id)
        {
            if (id != null)
            {
                Role catalog = await db.Roles.FirstOrDefaultAsync(p => p.IdRole == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(Role catalog)
        {
            db.Update(catalog);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdRole");
        }

        [HttpGet]
        [ActionName("DeleteRole")]
        public async Task<IActionResult> ConfirmDeleteRole(int? id)
        {
            if (id != null)
            {
                Role form = await db.Roles.FirstOrDefaultAsync(p => p.IdRole == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(int? id)
        {
            if (id != null)
            {
                Role catalog = await db.Roles.FirstOrDefaultAsync(p => p.IdRole == id);
                if (catalog != null)
                {
                    db.Roles.Remove(catalog);
                    await db.SaveChangesAsync();
                    return RedirectToAction("CrdRole");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DetailsRole(int? id)
        {
            if (id != null)
            {
                Role catalog = await db.Roles.FirstOrDefaultAsync(p => p.IdRole == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }


        //формы

        public async Task<IActionResult> CrdForm()
        {
            return View(await db.GuitarForms.ToListAsync());
        }

        public IActionResult CreateForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm(GuitarForm form)
        {
            db.GuitarForms.Add(form);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdForm");
        }
        public async Task<IActionResult> UpdateForm(int? id)
        {
            if (id != null)
            {
                GuitarForm catalog = await db.GuitarForms.FirstOrDefaultAsync(p => p.IdForm == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateForm(GuitarForm catalog)
        {
            db.Update(catalog);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdForm");
        }

        [HttpGet]
        [ActionName("DeleteForm")]
        public async Task<IActionResult> ConfirmDeleteForm(int? id)
        {
            if (id != null)
            {
                GuitarForm form = await db.GuitarForms.FirstOrDefaultAsync(p => p.IdForm == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm(int? id)
        {
            if (id != null)
            {
                GuitarForm catalog = await db.GuitarForms.FirstOrDefaultAsync(p => p.IdForm == id);
                if (catalog != null)
                {
                    db.GuitarForms.Remove(catalog);
                    await db.SaveChangesAsync();
                    return RedirectToAction("CrdForm");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DetailsForm(int? id)
        {
            if (id != null)
            {
                GuitarForm catalog = await db.GuitarForms.FirstOrDefaultAsync(p => p.IdForm == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }


        // заказы
        public async Task<IActionResult> CrdOrder()
        {
            return View(await db.Orders.ToListAsync());
        }

        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order form)
        {
            db.Orders.Add(form);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdOrder");
        }
        public async Task<IActionResult> UpdateOrder(int? id)
        {
            if (id != null)
            {
                Order catalog = await db.Orders.FirstOrDefaultAsync(p => p.IdOrder == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order catalog)
        {
            db.Update(catalog);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdOrder");
        }

        [HttpGet]
        [ActionName("DeleteOrder")]
        public async Task<IActionResult> ConfirmDeleteOrder(int? id)
        {
            if (id != null)
            {
                Order form = await db.Orders.FirstOrDefaultAsync(p => p.IdOrder == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int? id)
        {
            if (id != null)
            {
                Order catalog = await db.Orders.FirstOrDefaultAsync(p => p.IdOrder == id);
                if (catalog != null)
                {
                    db.Orders.Remove(catalog);
                    await db.SaveChangesAsync();
                    return RedirectToAction("CrdOrder");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DetailsOrder(int? id)
        {
            if (id != null)
            {
                Order catalog = await db.Orders.FirstOrDefaultAsync(p => p.IdOrder == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }


        //  корзина

        public async Task<IActionResult> CrdCart()
        {
            return View(await db.Carts.ToListAsync());
        }

        public IActionResult CreateCart()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(Cart form)
        {
            db.Carts.Add(form);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdCart");
        }
        public async Task<IActionResult> UpdateCart(int? id)
        {
            if (id != null)
            {
                Cart catalog = await db.Carts.FirstOrDefaultAsync(p => p.CartId == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(Cart catalog)
        {
            db.Update(catalog);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdCart");
        }

        [HttpGet]
        [ActionName("DeleteCart")]
        public async Task<IActionResult> ConfirmDeleteCart(int? id)
        {
            if (id != null)
            {
                Cart form = await db.Carts.FirstOrDefaultAsync(p => p.CartId == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCart(int? id)
        {
            if (id != null)
            {
                Cart catalog = await db.Carts.FirstOrDefaultAsync(p => p.CartId == id);
                if (catalog != null)
                {
                    db.Carts.Remove(catalog);
                    await db.SaveChangesAsync();
                    return RedirectToAction("CrdCart");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DetailsCart(int? id)
        {
            if (id != null)
            {
                Cart catalog = await db.Carts.FirstOrDefaultAsync(p => p.CartId == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }

        // позиция заказа

        public async Task<IActionResult> CrdPos()
        {
            return View(await db.PosOrders.ToListAsync());
        }

        public IActionResult CreatePos()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePos(PosOrder form)
        {
            db.PosOrders.Add(form);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdPos");
        }
        public async Task<IActionResult> UpdatePos(int? id)
        {
            if (id != null)
            {
                PosOrder catalog = await db.PosOrders.FirstOrDefaultAsync(p => p.IdPosOrder == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePos(PosOrder catalog)
        {
            db.Update(catalog);
            await db.SaveChangesAsync();
            return RedirectToAction("CrdPos");
        }

        [HttpGet]
        [ActionName("DeletePos")]
        public async Task<IActionResult> ConfirmDeletePos(int? id)
        {
            if (id != null)
            {
                PosOrder form = await db.PosOrders.FirstOrDefaultAsync(p => p.IdPosOrder == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeletePos(int? id)
        {
            if (id != null)
            {
                PosOrder catalog = await db.PosOrders.FirstOrDefaultAsync(p => p.IdPosOrder == id);
                if (catalog != null)
                {
                    db.PosOrders.Remove(catalog);
                    await db.SaveChangesAsync();
                    return RedirectToAction("CrdPos");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DetailsPos(int? id)
        {
            if (id != null)
            {
                PosOrder catalog = await db.PosOrders.FirstOrDefaultAsync(p => p.IdPosOrder == id);
                if (catalog != null)
                    return View(catalog);
            }
            return NotFound();
        }

    }
}
