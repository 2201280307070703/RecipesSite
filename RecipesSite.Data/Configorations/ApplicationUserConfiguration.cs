namespace RecipesSite.Data.Configorations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RecipesSite.Data.Models;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(au => au.FirstName)
                .HasDefaultValue("Test");

            builder.Property(au => au.LastName)
                .HasDefaultValue("Test");
        }
    }
}
