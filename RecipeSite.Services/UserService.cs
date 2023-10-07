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

        public async Task<string?> GetFullNameByUsernameAsync(string username)
        {
            ApplicationUser? user = await this.dbContext.Users.Where(u => u.UserName==username||u.Email== username).FirstOrDefaultAsync();

            if(user==null)
            {
                return null;
            }

            return user.FirstName + " " + user.LastName;
        }
    }
}
