namespace RecipesSite.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static RecipesSite.Common.EntityValidations.DishValidations;

    public class Dish
    {
        public Dish()
        {
            this.UsersSaved = new HashSet<UsersDishes>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(IngredientsMaxLength)]
        public string Ingredients { get; set; } = null!;

        [Required]
        [MaxLength(PreparationStepsMaxLength)]
        public string PreparationSteps { get; set; } = null!;

        public int CookingTime { get; set; }

        public int TotalLikesCount { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        [ForeignKey(nameof(PostingUser))]
        public Guid PostingUserId { get; set; }

        public ApplicationUser PostingUser { get; set; } = null!;

        public ICollection<UsersDishes> UsersSaved { get; set; }
    }
}
