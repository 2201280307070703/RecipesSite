namespace RecipeSite.Services.Contracts
{
    using RecipesSite.Web.viewModels.Dish;

    public interface IDishService
    {
        Task<IEnumerable<IndexViewModel>> GetLastNineDishesAsync();

        Task<DishDetailsViewModel> GetDishDetailsAsync(int id);

        Task<DishFormModel> GetForAddAsync();

        Task<bool> DishWithSameNameExistAsync(string name);

        Task<int> AddDishAsync(DishFormModel model, string userId);
    }
}
