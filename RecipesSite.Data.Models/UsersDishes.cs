using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesSite.Data.Models
{
    public class UsersDishes
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Dish))]
        public int DishId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public Dish Dish { get; set; } = null!;
    }
}
