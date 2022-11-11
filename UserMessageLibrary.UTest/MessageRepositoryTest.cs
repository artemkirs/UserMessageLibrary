using UserMessageLibrary.Repository.ContactRep;
using UserMessageLibrary.Repository.MessageRep;
using UserMessageLibrary.Repository.UserRep;

namespace UserMessageLibrary.UTest
{
    public class MessageRepositoryTest : BaseRepositoryTest
    {
        [Fact]
        public async Task AddMessage()
        {
            var contactRepository = new ContactRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);
            var messageRepository = new MessageRepository(_dbContext);

            var user = GenerationEntity.CreateUser();
            var contactUser = GenerationEntity.CreateUser();

            var userId = await userRepository.AddAsync(user);
            var contactUserId = await userRepository.AddAsync(contactUser);

            var message = GenerationEntity.CreateMessage(userId, contactUserId);

            await messageRepository.AddAsync(message);

            var newMessage = messageRepository.GetAllByUserId(userId).FirstOrDefault();
            Assert.NotNull(newMessage);
            Assert.Equal(message.UserId, newMessage?.UserId);
            Assert.Equal(message.Content, newMessage?.Content);
            Assert.Equal(message.ContactId, newMessage?.ContactId);
            Assert.Equal(message.SendTime, newMessage?.SendTime);
            Assert.Equal(message.DeliveryTime, newMessage?.DeliveryTime);
        }

        [Fact]
        public async Task GetAllMessageByUserId()
        {
            var contactRepository = new ContactRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);
            var messageRepository = new MessageRepository(_dbContext);

            var user = GenerationEntity.CreateUser();
            var contactUser = GenerationEntity.CreateUser();

            var userId = await userRepository.AddAsync(user);
            var contactUserId = await userRepository.AddAsync(contactUser);

            var message = GenerationEntity.CreateMessage(userId, contactUserId);
            var message2 = GenerationEntity.CreateMessage(userId, contactUserId);
            var message3 = GenerationEntity.CreateMessage(userId, contactUserId);

            await messageRepository.AddAsync(message);
            await messageRepository.AddAsync(message2);
            await messageRepository.AddAsync(message3);

            var newMessage = messageRepository.GetAllByUserId(userId);
            Assert.NotNull(newMessage);
            Assert.True(newMessage.Count() > 0);
            Assert.True(newMessage.All(x => x.UserId == userId));
        }


        [Fact]
        public async Task FindAllByUserAndContent()
        {
            var contactRepository = new ContactRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);
            var messageRepository = new MessageRepository(_dbContext);

            var user = GenerationEntity.CreateUser();
            var contactUser = GenerationEntity.CreateUser();

            var userId = await userRepository.AddAsync(user);
            var contactUserId = await userRepository.AddAsync(contactUser);
            var findContent = "findContent";

            var message = GenerationEntity.CreateMessage(userId, contactUserId);
            message.Content = findContent;
            var message2 = GenerationEntity.CreateMessage(userId, contactUserId);
            message2.Content += findContent;
            var message3 = GenerationEntity.CreateMessage(userId, contactUserId);
            message3.Content = findContent + message3.Content;
            var message4 = GenerationEntity.CreateMessage(userId, contactUserId);

            await messageRepository.AddAsync(message);
            await messageRepository.AddAsync(message2);
            await messageRepository.AddAsync(message3);

            var messages =messageRepository.FindAllByUserAndContent(userId, findContent);
            Assert.NotNull(messages);
            Assert.True(messages.Any());
            Assert.True(messages.All(x => x.UserId == userId));
            Assert.True(messages.All(x => x.Content.Contains(findContent)));
        }
    }
}
