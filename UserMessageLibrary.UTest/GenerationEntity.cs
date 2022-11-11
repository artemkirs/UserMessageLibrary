using UserMessageLibrary.Models;

namespace UserMessageLibrary.UTest
{
    public static class GenerationEntity
    {
        public static User CreateUser()
        {
            return new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                State = new Random().Next(0, 10) % 2 == 0
            };
        }

        public static Contact CreateContact(Guid userId, Guid contactId)
        {
            return new Contact
            {
                ContactId = contactId,
                UserId = userId,
                LastUpdateTime = DateTime.Now
            };
        }


        public static Message CreateMessage(Guid userId, Guid contactId)
        {
            return new Message
            {
                ContactId = contactId,
                UserId = userId,
                Content = Guid.NewGuid().ToString(),
                SendTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddMinutes(5),
            };
        }
    }
}
