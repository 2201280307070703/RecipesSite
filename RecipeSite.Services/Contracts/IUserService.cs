namespace RecipeSite.Services.Contracts
{
    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);
    }
}
