namespace RecipesSite.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUser:IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();

            this.SavedDishes = new HashSet<UsersDishes>();
            this.PostedDishes = new HashSet<Dish>();
        }

        [InverseProperty("PostingUser")]
        public ICollection<Dish> PostedDishes { get; set; }

        public ICollection<UsersDishes> SavedDishes { get; set; }
    }
}
