namespace RecipesSite.Web.Infrastructure.Extensions
{
    using System.Security.Claims;
    using static Common.GeneralApplicationConstants;
    public static  class ClaimsPrincipalExtensions
    {
        public static bool IsUserAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
