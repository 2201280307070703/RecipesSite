namespace RecipeSite.Services
{
    using Microsoft.EntityFrameworkCore;
    using RecipeSite.Services.Contracts;
    using RecipesSite.Data.Models;
    using RecipesSite.Web.Data;

    public class UserService : IUserService
    {
        private readonly RecipesDbContext dbContext;

        public UserService(RecipesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            ApplicationUser user = await this.dbContext.Users.Where(u => u.Email==email).FirstAsync();

            return user.FirstName + " " + user.LastName;
        }
    }
}
