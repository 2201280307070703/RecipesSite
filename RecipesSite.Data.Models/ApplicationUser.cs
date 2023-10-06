namespace RecipesSite.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static RecipesSite.Common.EntityValidations.UserValidations;

    public class ApplicationUser:IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();

            this.SavedDishes = new HashSet<UsersDishes>();
            this.PostedDishes = new HashSet<Dish>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [InverseProperty("PostingUser")]
        public ICollection<Dish> PostedDishes { get; set; }

        public ICollection<UsersDishes> SavedDishes { get; set; }
    }
}
