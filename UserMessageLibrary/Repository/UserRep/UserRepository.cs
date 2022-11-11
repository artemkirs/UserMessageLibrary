using Microsoft.EntityFrameworkCore;

namespace UserMessageLibrary.Repository.UserRep
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<User> FindByName(string name)
        {
            return _dbSet.AsParallel().Where(x => x.Name.Contains(name)).ToList();
        }

        public new async Task<User?> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<User?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }

        public new async Task<Guid> AddAsync(User user)
        {
            var newUser = await base.AddAsync(user);
            return newUser.Id;
        }

        public new async Task UpdateAsync(User user)
        {
            await base.UpdateAsync(user);
        }
    }
}
