namespace RecipesSite.Data.Configorations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RecipesSite.Data.Models;

    public class UsersDishesConfiguration : IEntityTypeConfiguration<UsersDishes>
    {
        public void Configure(EntityTypeBuilder<UsersDishes> builder)
        {
            builder.HasKey(ud => new {ud.UserId, ud.DishId});
        }
    }
}
