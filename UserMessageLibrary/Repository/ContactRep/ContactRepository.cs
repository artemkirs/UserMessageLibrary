using Microsoft.EntityFrameworkCore;
using UserMessageLibrary.Models;

namespace UserMessageLibrary.Repository.ContactRep
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public new async Task AddAsync(Contact contact)
        {        
            await base.AddAsync(contact);
        }

        public new async Task DeleteAsync(Contact contact)
        {
            await base.DeleteAsync(contact);
        }

        public List<Contact> GetAllByUserId(Guid id)
        {
           return _dbContext.Set<Contact>().Where(x => x.UserId == id).ToList();
        }

        public async Task<Contact?> GetByContactNameAsync(string contactName, Guid userId)
        {
            return await _dbContext.Set<Contact>().Include(x => x.ContactNavigation)
                .FirstOrDefaultAsync(x => x.ContactNavigation != null 
                    && x.ContactNavigation.Name == contactName 
                    && x.UserId == userId);
        }

        public new async Task<Contact?> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task UpdateAsync(Contact contact)
        {
            await base.UpdateAsync(contact);
        }
    }
}
