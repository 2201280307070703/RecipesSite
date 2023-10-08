namespace RecipeSite.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using RecipeSite.Services.Contracts;
    using RecipesSite.Data.Models;
    using RecipesSite.Web.Data;
    using RecipesSite.Web.viewModels.User;
    using System.Text;

    public class UserService : IUserService
    {
        private readonly RecipesDbContext dbContext;

        private readonly UserManager<ApplicationUser> userManager;

        public UserService(RecipesDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async  Task EditPersonalDataByIdAsync(string id, UserChangeInfoFormModel model)
        {
            ApplicationUser user = await this.dbContext.Users.Where(u => u.Id.ToString() == id).FirstAsync();

            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            if(model.NewPassword != null)
            {
                await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            }
            var code = await this.userManager.GenerateChangeEmailTokenAsync(user, model.Email);
            //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            await this.userManager.ChangeEmailAsync(user, model.Email, code);

            this.dbContext.SaveChanges();
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

        public async Task<UserPersonalDataViewModel> GetUserPersonalDataByIdAsync(string id)
        {
            return await this.dbContext.Users.Where(u => u.Id.ToString() == id)
                .Select(u => new UserPersonalDataViewModel()
                {   
                    Id=u.Id.ToString(),
                    Email = u.Email,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).FirstAsync();
        }

        public async Task<UserChangeInfoFormModel> GetUserPersonalDataForEditByIdAsync(string id)
        {
            UserChangeInfoFormModel model = await this.dbContext.Users.Where(u => u.Id.ToString() == id)
                .Select(u => new UserChangeInfoFormModel()
                {
                    Email = u.Email,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).FirstAsync();

             return model;
        }
    }
}
