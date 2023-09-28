namespace RecipesSite.Web.viewModels.Dish
{
    public class DishDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Ingredients { get; set; } = null!;

        public string PreparationSteps { get; set; } = null!;

        public int CookingTime { get; set; }

        public int TotalLikesCount { get; set; }

        public string CategoryName { get; set; } = null!;

    }
}
