namespace RecipeSite.Services.Contracts
{
    public interface IUserService
    {
        Task<string?> GetFullNameByUsernameAsync(string username);
    }
}
