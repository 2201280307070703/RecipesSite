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

            builder.HasOne(d=>d.Category)
                .WithMany(d=>d.Dishes)
                .HasForeignKey(d=>d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

                
        }
    }
}
