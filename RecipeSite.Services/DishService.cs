namespace RecipeSite.Services
{
    using Microsoft.EntityFrameworkCore;
    using RecipeSite.Services.Contracts;
    using RecipesSite.Data.Models;
    using RecipesSite.Web.Data;
    using RecipesSite.Web.viewModels.Dish;

    public class DishService : IDishService
    {
        private readonly RecipesDbContext dbContext;
        private readonly ICategoryService categoryService;

        public DishService(RecipesDbContext dbContext,
            ICategoryService categoryService)
        {
            this.dbContext = dbContext;
            this.categoryService = categoryService;
        }

        public async Task<int> AddDishAsync(DishFormModel model)
        {
            Dish dish=new Dish()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                Ingredients = model.Ingredients,
                PreparationSteps = model.PreparationSteps,
                CookingTime = model.CookingTime,
                CategoryId = model.CategoryId
            };

            await this.dbContext.Dishes.AddAsync(dish);
            await this.dbContext.SaveChangesAsync();

            return dish.Id;
        }

        public async Task<int> AddDishAsync(DishFormModel model, string userId)
        {
            Dish dish = new Dish()
            {
                Name=model.Name,
                ImageUrl=model.ImageUrl,
                Description = model.Description,
                Ingredients=model.Ingredients,
                PreparationSteps = model.PreparationSteps,
                CookingTime=model.CookingTime,
                CategoryId = model.CategoryId,
                PostingUserId=Guid.Parse(userId)
            };

            await this.dbContext.Dishes.AddAsync (dish);
            await this.dbContext.SaveChangesAsync();

            return dish.Id;
        }

        public async Task DeleteRecipeByIdAsync(int id)
        {
            Dish dish = await this.dbContext.Dishes.Where(d => d.Id == id).FirstAsync();

            dish.IsDeleted = true;

            await this.dbContext.SaveChangesAsync();
        }

        public Task<bool> DishExistByIdAsync(int id)
        {
            return this.dbContext.Dishes.AnyAsync(d=>d.Id==id && d.IsDeleted==false);
        }

        public Task<bool> DishWithSameNameExistAsync(string name)
        {
            return this.dbContext.Dishes.AnyAsync(d=>d.Name == name && d.IsDeleted == false);
        }

        public async Task<int> EditRecipeByIdAsync(int id, DishFormModel model)
        {
            Dish dish = await this.dbContext.Dishes.FirstAsync(d=>d.Id==id && d.IsDeleted == false);

            dish.Name = model.Name;
            dish.ImageUrl = model.ImageUrl;
            dish.Description = model.Description;
            dish.Ingredients = model.Ingredients;
            dish.PreparationSteps = model.PreparationSteps;
            dish.CookingTime = model.CookingTime;
            dish.CategoryId = model.CategoryId;

            await this.dbContext.SaveChangesAsync();
            return dish.Id;
        }

        public async Task<IEnumerable<DishDetailsViewModel>> GetAllDishesAddedByUserIdAsync(string userId)
        {
            return await this.dbContext.Dishes.Where(d => d.PostingUserId.ToString() == userId && d.IsDeleted==false)
                .Select(d => new DishDetailsViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    ImageUrl = d.ImageUrl,
                    Description = d.Description,
                    Ingredients = d.Ingredients,
                    PreparationSteps = d.PreparationSteps,
                    CookingTime = d.CookingTime,
                    TotalLikesCount = d.TotalLikesCount,
                    CategoryName = d.Category.Name
                }).ToListAsync();
        }

        public async Task<DishDetailsViewModel> GetDishDetailsAsync(int id)
        {
            return await this.dbContext.Dishes.Where(d=>d.IsDeleted==false && d.Id==id)
                .Select(d=>new DishDetailsViewModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                    ImageUrl = d.ImageUrl,
                    Description = d.Description,
                    Ingredients = d.Ingredients,
                    PreparationSteps = d.PreparationSteps,
                    CookingTime = d.CookingTime,
                    TotalLikesCount = d.TotalLikesCount,
                    CategoryName=d.Category.Name
                }).FirstAsync();
        }

        public async Task<DishDeleteViewModel> GetDishForDeleteByIdAsync(int id)
        {
            return await this.dbContext.Dishes.Where(d => d.Id == id && d.IsDeleted == false)
                .Select(d => new DishDeleteViewModel()
                {
                    Id= d.Id,
                    Name = d.Name,
                    Description = d.Description,
                }).FirstAsync();

        }

        public async Task<DishFormModel> GetDishForEditByIdAsync(int id)
        {
            DishFormModel dishForEdit = await this.dbContext.Dishes.Where(d => d.Id == id && d.IsDeleted == false)
                .Select(d => new DishFormModel()
                {
                    Name = d.Name,
                    ImageUrl = d.ImageUrl,
                    Description =d.Description,
                    Ingredients = d.Ingredients,
                    PreparationSteps = d.PreparationSteps,
                    CookingTime = d.CookingTime,
                    CategoryId = d.CategoryId,
                }).FirstAsync();

            dishForEdit.Categories=await this.categoryService.GetAllCategoriesAsync();

            return dishForEdit;
        }

        public async Task<DishFormModel> GetForAddAsync()
        {
            DishFormModel model = new DishFormModel()
            {
                Categories = await this.categoryService.GetAllCategoriesAsync()
            };

            return model;
        }

        public async Task<IEnumerable<IndexViewModel>> GetLastNineDishesAsync()
        {
            return await dbContext.Dishes.OrderBy(d => d.Id).Where(d=>d.IsDeleted==false).Take(9)
                .Select(d => new IndexViewModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    ImageUrl = d.ImageUrl
                }).ToListAsync();
                
        }

        public async Task<bool> IsUserOwnerOfThisRecipeByIdAsync(int recipeId, string userId)
        {
            Dish dish=await this.dbContext.Dishes.FirstAsync(d=>d.Id==recipeId && d.IsDeleted == false);

            return dish.PostingUserId.ToString() == userId;
        }

        public async Task SaveRecipeAsync(string userId, int dishId)
        {
            UsersDishes usersDishes=new UsersDishes()
            {
                UserId=Guid.Parse(userId),
                DishId=dishId
            };

            await this.dbContext.UsersDishes.AddAsync(usersDishes);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndexViewModel>> TakeAllSavedDishesByUserIdAsync(string userId)
        {
            return  await this.dbContext.UsersDishes.Where(ud => ud.UserId.ToString() == userId && ud.Dish.IsDeleted==false)
                .Select(ud => new IndexViewModel()
                {
                    Id=ud.DishId,
                    Name=ud.Dish.Name,
                    Description=ud.Dish.Description,
                    ImageUrl=ud.Dish.ImageUrl
                }).ToListAsync();
        }

        public async Task<bool> UserAlreadyHasThisRecipeInSavedDishesCollectionAsync(string userId, int dishId)
        {
            return await this.dbContext.UsersDishes.AnyAsync(ud=>ud.UserId.ToString()==userId && ud.DishId==dishId && ud.Dish.IsDeleted == false);
        }
    }
}
