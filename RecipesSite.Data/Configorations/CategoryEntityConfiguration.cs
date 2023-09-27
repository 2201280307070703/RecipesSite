namespace RecipesSite.Data.Configorations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RecipesSite.Data.Models;

    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private List<Category> GenerateCategories()
        {
            var categories = new List<Category>();

            Category category;

            category= new Category()
            {
                Id=1,
                Name="Pastry"
            };

            categories.Add(category);

            category = new Category()
            {
                Id=2,
                Name = "Desserts"
            };

            categories.Add(category);

            category = new Category()
            {
                Id=3,
                Name = "Starters"
            };

            categories.Add(category);

            category = new Category()
            {
                Id=4,
                Name = "Soups"
            };

            categories.Add(category);

            category = new Category()
            {
                Id=5,
                Name = "Main Dishes"
            };

            categories.Add(category);

            return categories;
        }
    }
}
