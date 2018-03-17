using ChefsBook.Auth;
using ChefsBook.Environment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChefsBook.MigrationsApp
{
    public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var opts = new DbContextOptionsBuilder<AuthDbContext>()
                .UseSqlServer(
                    MigrationsConfig.LoadConnectionString("Auth"),
                    cfg => cfg.MigrationsAssembly(ProjectConsts.Migrations))
                .Options;
            return new AuthDbContext(opts);
        }
    }
}
