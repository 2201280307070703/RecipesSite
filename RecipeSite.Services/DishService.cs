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

        public async Task<DishDetailsViewModel> GetDishDetailsAsync(int id)
        {
            return await this.dbContext.Dishes.Where(d=>d.IsDeleted==false && d.Id==id)
                .Select(d=>new DishDetailsViewModel()
                {
                    Id = id,
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

        public async Task<DishFormModel> GetForAddAsync()
        {
            DishFormModel model = new DishFormModel()
            {
                Categories = await this.categoryService.GetAllCategoryNamesAsync()
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
    }
}
