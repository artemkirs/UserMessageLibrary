using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserMessageLibrary.Data;

namespace UserMessageLibrary.UTest
{
    public abstract class BaseRepositoryTest
    {
        protected UserMessageContext _dbContext;

        protected BaseRepositoryTest()
        {
            var options = CreateNewContextOptions();

            _dbContext = new UserMessageContext(options);
        }

        protected static DbContextOptions<UserMessageContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<UserMessageContext>();
            builder.UseInMemoryDatabase("testUserMessages").UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }


    
}
