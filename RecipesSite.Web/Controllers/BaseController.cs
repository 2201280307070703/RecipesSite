namespace RecipesSite.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Authorize]
    public abstract class BaseController : Controller
    {
        public string GetId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
