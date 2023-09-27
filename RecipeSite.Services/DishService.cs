namespace RecipeSite.Services
{
    using Microsoft.EntityFrameworkCore;
    using RecipeSite.Services.Contracts;
    using RecipesSite.Web.Data;
    using RecipesSite.Web.viewModels.Dish;

    public class DishService : IDishService
    {
        private readonly RecipesDbContext dbContext;

        public DishService(RecipesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> GetLastNineDishes()
        {
            return await dbContext.Dishes.OrderBy(d => d.Id).Take(9)
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
