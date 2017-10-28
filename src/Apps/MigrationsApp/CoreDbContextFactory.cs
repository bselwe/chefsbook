using ChefsBook.Core;
using ChefsBook.Environment;
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
                    MigrationsConfig.LoadConnectionString("WebApi"),
                    cfg => cfg.MigrationsAssembly(ProjectConsts.Migrations))
                .Options;
            return new CoreDbContext(opts);
        }
    }
}
