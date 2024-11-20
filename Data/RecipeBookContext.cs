using FinalСertificationRecipeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalСertificationRecipeBook.Data
{
    public class RecipeBookContext : DbContext
    {
        public RecipeBookContext(DbContextOptions<RecipeBookContext> options) : base(options) 
        { 
        }        
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = NINTENDO; Initial Catalog = RecipeBook; Trusted_Connection=True; TrustServerCertificate=True")
                          .UseLazyLoadingProxies()
                          .LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Один ко многим: User - Recipe
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Author)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Один ко многим: Category - Recipe
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Один ко многим: Recipe - Ingredient
            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
