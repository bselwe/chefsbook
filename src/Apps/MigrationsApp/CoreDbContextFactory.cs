using ChefsBook.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChefsBook.MigrationsApp
{
    public class CoreDbContextFactory : IDesignTimeDbContextFactory<CoreDbContext>
    {
        public CoreDbContext CreateDbContext(string[] args)
        {
            var opts = new DbContextOptionsBuilder<CoreDbContext>()
                .UseSqlServer(
                    @"Server=db;Database=master;User=sa;Password=MyVeryStrongPassword1!",
                    cfg => cfg.MigrationsAssembly("Recipe.MigrationsApp"))
                .Options;
            return new CoreDbContext(opts);
        }
    }
}
