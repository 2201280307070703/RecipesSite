namespace RecipeSite.Services.Contracts
{
    using RecipesSite.Web.viewModels.User;
    public interface IUserService
    {
        Task<string?> GetFullNameByUsernameAsync(string username);

        Task<UserPersonalDataViewModel> GetUserPersonalDataByIdAsync(string  id);

        Task<UserChangeInfoFormModel> GetUserPersonalDataForEditByIdAsync(string id);

        Task EditPersonalDataByIdAsync(string id, UserChangeInfoFormModel model);
    }
}
