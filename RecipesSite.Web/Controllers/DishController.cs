namespace RecipesSite.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipeSite.Services.Contracts;
    using RecipesSite.Web.viewModels.Dish;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using static RecipesSite.Common.NotificationMessageConstants;

    public class DishController : BaseController
    {
        private readonly IDishService dishService;
        private readonly ICategoryService categoryService;

        public DishController(IDishService dishService,
            ICategoryService categoryService) 
        {
            this.dishService = dishService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
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

        [HttpPost]
        public async Task<IActionResult> Add(DishFormModel model)
        {
            bool nameAlreadyExists = await this.dishService.DishWithSameNameExistAsync(model.Name);

            bool categoryDoNotExist = await this.categoryService.CheckIfCategoryExistAsync(model.CategoryId);

            if(nameAlreadyExists)
            {
                ModelState.AddModelError(nameof(model.Name), "Dish with same name already exists! Please try again with different one.");

                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            if(!categoryDoNotExist)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "This category do not exist! Please select again.");

                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            try
            {
                var newDishId = await this.dishService.AddDishAsync(model, GetId());

                return RedirectToAction("Details", "Dish", new { id = newDishId });
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later.";

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
