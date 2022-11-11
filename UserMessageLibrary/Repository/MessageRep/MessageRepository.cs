using Microsoft.EntityFrameworkCore;
using UserMessageLibrary.Models;

namespace UserMessageLibrary.Repository.MessageRep
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<Message> FindAllByUserAndContent(Guid userId, string content)
        {
            return _dbContext.Set<Message>().Where(x => x.UserId == userId && x.Content.Contains(content)).ToList();
        }

        public List<Message> GetAllByUserId(Guid userId)
        {
            return _dbContext.Set<Message>().Where(x => x.UserId == userId).ToList();
        }

        public new async Task AddAsync(Message message)
        {
            await base.AddAsync(message);
        }
    }
}
