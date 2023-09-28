namespace RecipesSite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RecipeSite.Services.Contracts;
    using RecipesSite.Web.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly IDishService dishService;
        public HomeController(IDishService dishService)
        {
            this.dishService = dishService; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await dishService.GetLastNineDishesAsync();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}