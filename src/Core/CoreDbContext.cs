using System;
using ChefsBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core
{
    public class CoreDbContext : DbContext
    {
        public const string Schema = "core";

        public DbSet<Recipe> Recipes => Set<Recipe>();

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<Recipe>(cfg =>
            {
                cfg.HasKey(e => e.Id);

                cfg.Property(e => e.Title).HasMaxLength(200);
                cfg.Property(e => e.Description).HasMaxLength(4000);
                cfg.Property(e => e.Image).HasMaxLength(2000);
                cfg.Property(e => e.Notes).HasMaxLength(2000);

                cfg
                    .HasMany(e => e.Ingredients)
                    .WithOne(i => i.Recipe)
                    .HasForeignKey(i => i.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade);

                cfg
                    .HasMany(e => e.Steps)
                    .WithOne(s => s.Recipe)
                    .HasForeignKey(s => s.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade);

                cfg
                    .HasMany(e => e.Tags);
            });

            modelBuilder.Entity<Ingredient>(cfg =>
            {
                cfg.HasKey(e => new { e.IngredientId, e.RecipeId });
            });

            modelBuilder.Entity<Step>(cfg =>
            {
                cfg.HasKey(e => new { e.StepId, e.RecipeId });
            });

            modelBuilder.Entity<Tag>(cfg =>
            {
                cfg.HasKey(e => e.Id);
            });
        }
    }
}
