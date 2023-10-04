using Microsoft.AspNetCore.Mvc;

namespace RecipesSite.Web.Controllers
{
    public class AboutUs : Controller
    {
        public IActionResult Info()
        {
            return View();
        }

        public IActionResult Instructions()
        {
            return View();
        }
    }
}
