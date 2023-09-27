namespace RecipesSite.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    public class ApplicationUser:IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();

            this.SavedDishes = new HashSet<Dish>();
        }

        public ICollection<Dish> SavedDishes { get; set; }
    }
}
