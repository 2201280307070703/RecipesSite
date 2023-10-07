namespace RecipesSite.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipeSite.Services.Contracts;
    using RecipesSite.Web.Infrastructure.Extensions;
    using RecipesSite.Web.viewModels.Dish;
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

        [HttpGet]
        public async Task<IActionResult> MyAdded()
        {
            try
            {
                var model = await this.dishService.GetAllDishesAddedByUserIdAsync(GetId());

                return View(model);
            }
            catch (Exception) 
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool dishExists=await this.dishService.DishExistByIdAsync(id);

            bool isUserOwnerOfRecipe = await this.dishService.IsUserOwnerOfThisRecipeByIdAsync(id, GetId());

            if (!isUserOwnerOfRecipe || !User.IsUserAdmin())
            {
                this.TempData[ErrorMessage] = "This recipe is not yours and you can not delete it!";
                return RedirectToAction("Dish", "MyAdded");
            }

            if (!dishExists)
            {
                this.TempData[ErrorMessage] = "This recipe do not exist!";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var model=await this.dishService.GetDishForDeleteByIdAsync(id);
                return View(model);
            }
            catch(Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DishDeleteViewModel model)
        {
            bool dishExists = await this.dishService.DishExistByIdAsync(id);

            bool isUserOwnerOfRecipe = await this.dishService.IsUserOwnerOfThisRecipeByIdAsync(id, GetId());

            if (!isUserOwnerOfRecipe || !User.IsUserAdmin())
            {
                this.TempData[ErrorMessage] = "This recipe is not yours and you can not delete it!";
                return RedirectToAction("Dish", "MyAdded");
            }

            if (!dishExists)
            {
                this.TempData[ErrorMessage] = "This recipe do not exist!";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                await this.dishService.DeleteRecipeByIdAsync(id);


                this.TempData[WarningMessage] = "Successfully deleted!";
                return RedirectToAction("MyAdded", "Dish");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool dishExists = await this.dishService.DishExistByIdAsync(id);

            bool isUserOwnerOfRecipe = await this.dishService.IsUserOwnerOfThisRecipeByIdAsync(id, GetId());

            if (!isUserOwnerOfRecipe || !User.IsUserAdmin())
            {
                this.TempData[ErrorMessage] = "This recipe is not yours and you can not edit it!";
                return RedirectToAction("Dish", "MyAdded");
            }

            if (!dishExists)
            {
                this.TempData[ErrorMessage] = "This recipe do not exist!";
                return RedirectToAction("Index", "Home");
            }

            try
            {
               var model= await this.dishService.GetDishForEditByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DishFormModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories=await this.categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            bool dishExists = await this.dishService.DishExistByIdAsync(id);

            bool isUserOwnerOfRecipe = await this.dishService.IsUserOwnerOfThisRecipeByIdAsync(id, GetId());

            if (!isUserOwnerOfRecipe || !User.IsUserAdmin())
            {
                this.TempData[ErrorMessage] = "This recipe is not yours and you can not edit it!";
                return RedirectToAction("Dish", "MyAdded");
            }

            if (!dishExists)
            {
                this.TempData[ErrorMessage] = "This recipe do not exist!";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                int dishId=await this.dishService.EditRecipeByIdAsync(id, model);
                return RedirectToAction("Details", "Dish" ,new {id= dishId});
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            bool dishExists = await this.dishService.DishExistByIdAsync(id);

            bool dishIsAlreadyInSavedDishesCollection = await this.dishService.UserAlreadyHasThisRecipeInSavedDishesCollectionAsync(GetId(), id);

            bool userIsOwnerOfRecipe = await this.dishService.IsUserOwnerOfThisRecipeByIdAsync(id, GetId());

            if (!dishExists)
            {
                this.TempData[ErrorMessage] = "This recipe do not exist!";
                return RedirectToAction("Index", "Home");
            }

            if (dishIsAlreadyInSavedDishesCollection)
            {
                this.TempData[ErrorMessage] = "This recipe is already saved in your collection";
                return RedirectToAction("Saved", "Dish");
            }

            if (userIsOwnerOfRecipe)
            {
                this.TempData[ErrorMessage] = "You are the author of this recipe. You can not save it!";
                return RedirectToAction("MyAdded", "Dish");
            }

            try
            {
                await this.dishService.SaveRecipeAsync(GetId(), id);
                return RedirectToAction("Saved", "Dish");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Saved()
        {
            var model = await this.dishService.TakeAllSavedDishesByUserIdAsync(GetId());

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Like(int id)
        {
            bool dishExists = await this.dishService.DishExistByIdAsync(id);

            if (!dishExists)
            {
                this.TempData[ErrorMessage] = "This recipe do not exist!";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                await this.dishService.IncreaseLikesCountByIdAsync(id);

                return RedirectToAction("Details", "Dish", new {id=id});
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> MostRated()
        {
            var model = await this.dishService.TakeTopTenDishesAsync();

            return View(model);
        }
    }
}
