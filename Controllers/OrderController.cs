using Microsoft.AspNetCore.Mvc;

namespace guitarshop.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Ord()
        {
            return View();
        }
    }
}
