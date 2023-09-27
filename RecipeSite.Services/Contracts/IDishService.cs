namespace RecipeSite.Services.Contracts
{
    using RecipesSite.Web.viewModels.Dish;

    public interface IDishService
    {
        Task<IEnumerable<IndexViewModel>> GetLastNineDishes();
    }
}
