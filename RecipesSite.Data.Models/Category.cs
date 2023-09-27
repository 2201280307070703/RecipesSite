namespace RecipesSite.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using static RecipesSite.Common.EntityValidations.CategoryValidations;

    public class Category
    {
        public Category() 
        { 
            this.Dishes=new HashSet<Dish>();
        }  

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Dish> Dishes { get; set; }

    }
}
