using System;
using System.Threading.Tasks;

namespace ChefsBook.Core
{
    public class CoreUnitOfWork
    {
        
        private CoreDbContext dbContext;

        public CoreUnitOfWork(CoreDbContext dbContext)
        {
            dbContext = dbContext;
        }

        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
