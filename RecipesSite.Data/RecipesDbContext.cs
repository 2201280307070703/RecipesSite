namespace RecipesSite.Web.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RecipesSite.Data.Configorations;
    using RecipesSite.Data.Models;

    public class RecipesDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public RecipesDbContext(DbContextOptions<RecipesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Dish> Dishes { get; set; } = null!;

        public DbSet<UsersDishes> UsersDishes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryEntityConfiguration());

            builder.ApplyConfiguration(new DishEntityConfigoration());

            builder.ApplyConfiguration(new UsersDishesConfiguration());

            builder.ApplyConfiguration(new ApplicationUserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}