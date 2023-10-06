namespace RecipesSite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AboutUsController : Controller
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
