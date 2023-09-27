namespace RecipesSite.Common
{
    public static class EntityValidations
    {
        public static class CategoryValidations
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 20;
        }

        public static class DishValidations
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;

            public const int ImageUrlMaxLength = 2048;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 300;

            public const int IngredientsMinLength = 10;
            public const int IngredientsMaxLength = 300;

            public const int PreparationStepsMinLength = 10;
            public const int PreparationStepsMaxLength = 400;
        }
    }
}
