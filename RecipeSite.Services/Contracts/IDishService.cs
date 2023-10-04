namespace RecipeSite.Services.Contracts
{
    using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
    using RecipesSite.Web.viewModels.Dish;

    public interface IDishService
    {
        Task<IEnumerable<IndexViewModel>> GetLastNineDishesAsync();

        Task<DishDetailsViewModel> GetDishDetailsAsync(int id);

        Task<DishFormModel> GetForAddAsync();

        Task<bool> DishWithSameNameExistAsync(string name);

        Task<int> AddDishAsync(DishFormModel model, string userId);

        Task<IEnumerable<DishDetailsViewModel>> GetAllDishesAddedByUserIdAsync(string userId);

        Task<bool> IsUserOwnerOfThisRecipeByIdAsync(int recipeId, string userId);

        Task<bool> DishExistByIdAsync(int id);

        Task<DishDeleteViewModel> GetDishForDeleteByIdAsync(int id);

        Task DeleteRecipeByIdAsync(int id);

        Task<DishFormModel> GetDishForEditByIdAsync(int id);

        Task<int> EditRecipeByIdAsync(int id, DishFormModel model);

        Task<bool> UserAlreadyHasThisRecipeInSavedDishesCollectionAsync(string userId, int dishId);

        Task SaveRecipeAsync(string userId, int dishId);

        Task<IEnumerable<IndexViewModel>> TakeAllSavedDishesByUserIdAsync(string userId);
    }
}