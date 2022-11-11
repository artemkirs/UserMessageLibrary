using Microsoft.EntityFrameworkCore;
using UserMessageLibrary.Models;
using UserMessageLibrary.Repository.ContactRep;
using UserMessageLibrary.Repository.UserRep;

namespace UserMessageLibrary.UTest
{
    public class ContactRepositoryTest : BaseRepositoryTest
    {
        [Fact]
        public async Task AddsContact()
        {
            var contactRepository = new ContactRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);

            var user = GenerationEntity.CreateUser();
            var contactUser = GenerationEntity.CreateUser();

            var userId = await userRepository.AddAsync(user);
            var contactUserId = await userRepository.AddAsync(contactUser);
            var date = DateTime.Now;

            var contact = new Contact
            {
                ContactId = contactUserId,
                UserId = userId,
                LastUpdateTime = date
            };

            await contactRepository.AddAsync(contact);

            _dbContext.Entry(contact).State = EntityState.Detached;
            var newContact = contactRepository.GetAllByUserId(userId).FirstOrDefault();

            Assert.NotNull(newContact);
            Assert.Equal(contactUserId, newContact?.ContactId);
            Assert.Equal(userId, newContact?.UserId);
            Assert.Equal(date, newContact?.LastUpdateTime);
        }


        [Fact]
        public async Task UpdateContact()
        {
            var contactRepository = new ContactRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);

            var user = GenerationEntity.CreateUser();
            var contactUser = GenerationEntity.CreateUser();

            var userId = await userRepository.AddAsync(user);
            var contactUserId = await userRepository.AddAsync(contactUser);
            var date = DateTime.Now;

            var contact = new Contact
            {
                ContactId = contactUserId,
                UserId = userId,
                LastUpdateTime = date
            };

            await contactRepository.AddAsync(contact);

            _dbContext.Entry(contact).State = EntityState.Detached;

            var newContact = contactRepository.GetAllByUserId(userId).FirstOrDefault();
            if (newContact == null)
            {
                Assert.NotNull(newContact);
                return;
            }
            Assert.NotSame(contact, newContact);

            var newDate = DateTime.Now;
            newContact.LastUpdateTime = newDate;
            await contactRepository.UpdateAsync(newContact);
            var updatedContact = contactRepository.GetAllByUserId(userId).FirstOrDefault();
            Assert.NotNull(updatedContact);
            Assert.NotEqual(contact.LastUpdateTime, updatedContact?.LastUpdateTime);
            Assert.Equal(newContact.Id, updatedContact?.Id);
            Assert.Equal(contact.UserId, updatedContact?.UserId);
            Assert.Equal(contact.ContactId, updatedContact?.ContactId);
        }

        [Fact]
        public async Task GetByContactName()
        {
            var contactRepository = new ContactRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);
            var findName = "find";
            var user = GenerationEntity.CreateUser();
            var contactUser = GenerationEntity.CreateUser();
            contactUser.Name = findName;

            var contactUser2 = GenerationEntity.CreateUser();
            contactUser2.Name += findName; 
            var contactUser3 = GenerationEntity.CreateUser();

            var userId = await userRepository.AddAsync(user);
            var contactUserId = await userRepository.AddAsync(contactUser);
            var contactUserId2 = await userRepository.AddAsync(contactUser2);
            var contactUserId3 = await userRepository.AddAsync(contactUser3);

            var contact = GenerationEntity.CreateContact(userId, contactUserId);
            var contact2 = GenerationEntity.CreateContact(userId, contactUserId2);
            var contact3 = GenerationEntity.CreateContact(userId, contactUserId3);

            await contactRepository.AddAsync(contact);
            await contactRepository.AddAsync(contact2);
            await contactRepository.AddAsync(contact3);

            var findContact = await contactRepository.GetByContactNameAsync(findName, userId);

            Assert.NotNull(findContact);
            Assert.Equal(contact.ContactId, findContact?.ContactId);
            Assert.Equal(contact.UserId, findContact?.UserId);
        }

        [Fact]
        public async Task DeleteContact()
        {
            var contactRepository = new ContactRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);

            var user = GenerationEntity.CreateUser();
            var contactUser = GenerationEntity.CreateUser();

            var userId = await userRepository.AddAsync(user);
            var contactUserId = await userRepository.AddAsync(contactUser);
            var contact = GenerationEntity.CreateContact(userId, contactUserId);
            await contactRepository.AddAsync(contact);

            var newContact = contactRepository.GetAllByUserId(userId).FirstOrDefault();

            if (newContact == null)
            {
                Assert.NotNull(newContact);
                return;
            }
            await contactRepository.DeleteAsync(contact);

            var deletedContact = contactRepository.GetAllByUserId(userId).FirstOrDefault();
            Assert.Null(deletedContact);

        }


        [Fact]
        public async Task GetContactById()
        {
            var contactRepository = new ContactRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);

            var user = GenerationEntity.CreateUser();
            var contactUser = GenerationEntity.CreateUser();

            var userId = await userRepository.AddAsync(user);
            var contactUserId = await userRepository.AddAsync(contactUser);
            var contact = GenerationEntity.CreateContact(userId, contactUserId);
            await contactRepository.AddAsync(contact);

            var newContact = contactRepository.GetAllByUserId(userId).FirstOrDefault();

            if (newContact == null)
            {
                Assert.NotNull(newContact);
                return;
            }
            var findContact = await contactRepository.GetByIdAsync(contact.Id);

            Assert.NotNull(findContact);
            Assert.Equal(newContact.Id, findContact?.Id);
            Assert.Equal(newContact.UserId, findContact?.UserId);
            Assert.Equal(newContact.ContactId, findContact?.ContactId);
            Assert.Equal(newContact.LastUpdateTime, findContact?.LastUpdateTime);

        }
    }
}
