namespace RecipesSite.Web.viewModels.Dish
{
    using System.ComponentModel.DataAnnotations;
    using RecipesSite.Web.viewModels.Category;
    using static RecipesSite.Common.EntityValidations.DishValidations;

    public class DishFormModel
    {
        public DishFormModel()
        {
            this.Categories=new HashSet<CategoriesListViewModel>();
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength =NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Url]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name="Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(IngredientsMaxLength, MinimumLength = IngredientsMinLength)]
        public string Ingredients { get; set; } = null!;

        [Required]
        [StringLength(PreparationStepsMaxLength, MinimumLength = PreparationStepsMinLength)]
        [Display(Name = "Preparation Steps")]
        public string PreparationSteps { get; set; } = null!;

        [Range(5,360)]
        [Display(Name="Time For Cook")]
        public int CookingTime { get; set; }

        [Display(Name="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoriesListViewModel> Categories { get; set; }
    }
}
