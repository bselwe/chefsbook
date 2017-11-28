using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Auth
{
    public class AuthDbContext : IdentityDbContext<AuthUser, AuthRole, Guid>
    {
        public const string Schema = "auth";

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schema);

            builder.Entity<AuthUser>(b => b.ToTable("AspNetUsers"));
            builder.Entity<AuthRole>(b => b.ToTable("AspNetRoles"));
            builder.Entity<IdentityUserClaim<Guid>>(b => b.ToTable("AspNetUserClaims"));
            builder.Entity<IdentityRoleClaim<Guid>>(b => b.ToTable("AspNetRoleClaims"));
            builder.Entity<IdentityUserRole<Guid>>(b => b.ToTable("AspNetUserRoles"));
            builder.Entity<IdentityUserLogin<Guid>>(b => b.ToTable("AspNetUserLogins"));
            builder.Entity<IdentityUserToken<Guid>>(b => b.ToTable("AspNetUserTokens"));

            builder.Entity<AuthUser>(b =>
            {
                b.Property(u => u.FirstName)
                    .HasMaxLength(100);
                b.Property(u => u.LastName)
                    .HasMaxLength(100);
            });
        }
    }
}
