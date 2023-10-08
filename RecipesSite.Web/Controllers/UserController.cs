namespace RecipesSite.Web.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RecipeSite.Services.Contracts;
    using RecipesSite.Data.Models;
    using RecipesSite.Web.viewModels.User;
    using System.Security.Claims;
    using static RecipesSite.Common.NotificationMessageConstants;

    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService userService;

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            await this.userManager.SetEmailAsync(user, model.Email);
            await this.userManager.SetUserNameAsync(user, model.UserName);

            IdentityResult result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await this.signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if(!ModelState.IsValid) 
            {
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                this.TempData[ErrorMessage] = "There was an error while logging you in! Please try again.";

                return View(model);
            }

            return Redirect(model.ReturnUrl ?? "/Home/Index");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> PersonalData()
        {
            string? user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await this.userService.GetUserPersonalDataByIdAsync(user);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditInfo(string id)
        {
            var model= await this.userService.GetUserPersonalDataForEditByIdAsync(id);

             return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditInfo(string id, UserChangeInfoFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.userService.EditPersonalDataByIdAsync(id, model);
                
                return RedirectToAction("PersonalData", "User");
            }
            catch(Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again later.";

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
