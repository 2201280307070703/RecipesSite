namespace RecipesSite.Data.Configorations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RecipesSite.Data.Models;

    public class DishEntityConfigoration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {

            builder.Property(d => d.IsDeleted).HasDefaultValue(false);

            builder.Property(d=>d.TotalLikesCount).HasDefaultValue(0);

            builder.HasOne(d=>d.Category)
                .WithMany(d=>d.Dishes)
                .HasForeignKey(d=>d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateDishes());

        }

        private List<Dish> GenerateDishes()
        {
            List<Dish> dishes=new List<Dish>();

            Dish dish;

            dish = new Dish()
            {
                Id = 1,
                Name="Chicken soup",
                ImageUrl= "https://media.istockphoto.com/id/1173599844/photo/mulligatawny-soup-with-naan.jpg?s=612x612&w=0&k=20&c=5eE4TJT_AG6CL-eoCohFMmTcGwOd_dH3tkTdbGX4nl0=",
                Description="This is very tasty and healthy soup!",
                Ingredients= "Chicken, Potatoes, Fresh Herbs",
                PreparationSteps= "Place a large dutch oven or pot over medium high heat and add in oil. Once oil is hot, add in garlic, onion, carrots and celery and so on...",
                CookingTime=60,
                CategoryId=4
            };

            dishes.Add(dish);

            dish = new Dish()
            {
                Id = 2,
                Name = "Cake",
                ImageUrl = "https://images.unsplash.com/photo-1588195538326-c5b1e9f80a1b?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8fA%3D%3D&w=1000&q=80",
                Description = "This is very tasty and juicy cake. You will love it!",
                Ingredients = "Sugar, Butter, Eggs, Baking powder,Milk",
                PreparationSteps = "Place white sugar and butter into a mixing bowl. Beat with an electric mixer on medium speed until light and fluffy and so on...",
                CookingTime = 120,
                CategoryId = 2
            };

            dishes.Add(dish);

            return dishes;
        }
    }
}
