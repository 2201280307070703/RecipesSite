namespace RecipeSite.Services.Contracts
{
    using RecipesSite.Web.viewModels.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoriesListViewModel>> GetAllCategoriesAsync();

        Task<bool> CheckIfCategoryExistAsync(int categoryId);
    }
}
