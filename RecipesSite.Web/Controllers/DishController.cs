namespace RecipesSite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RecipeSite.Services.Contracts;
    using static RecipesSite.Common.NotificationMessageConstants;

    public class DishController : Controller
    {
        private readonly IDishService dishService;

        public DishController(IDishService dishService) 
        {
            this.dishService = dishService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model= await this.dishService.GetDishDetailsAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
           var model= await this.dishService.GetForAddAsync();

            return View(model);
        }
    }
}
