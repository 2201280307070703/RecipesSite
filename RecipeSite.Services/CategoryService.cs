namespace RecipeSite.Services
{
    using Microsoft.EntityFrameworkCore;
    using RecipeSite.Services.Contracts;
    using RecipesSite.Web.Data;
    using RecipesSite.Web.viewModels.Category;

    public class CategoryService : ICategoryService
    {
        private readonly RecipesDbContext dbContext;

        public CategoryService(RecipesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CategoriesListViewModel>> GetAllCategoryNamesAsync()
        {
            return await this.dbContext.Categories
                 .Select(c => new CategoriesListViewModel()
                 {
                     Id = c.Id,
                     Name = c.Name
                 }).ToListAsync();
        }
    }
}
