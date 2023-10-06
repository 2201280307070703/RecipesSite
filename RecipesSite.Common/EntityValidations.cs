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

        public static class UserValidations
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 15;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 20;

            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 30;

            public const int UserNameMinLength = 4;
            public const int UserNameMaxLength = 40;
        }
    }
}
