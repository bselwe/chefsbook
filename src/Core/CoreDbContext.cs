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
                cfg.Property(e => e.Name).HasMaxLength(200);
            });
        }
    }
}
