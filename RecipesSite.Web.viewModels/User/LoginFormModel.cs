using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RecipesSite.Web.viewModels.User
{
    public class LoginFormModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
