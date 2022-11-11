using Microsoft.EntityFrameworkCore;
using UserMessageLibrary.Repository.UserRep;

namespace UserMessageLibrary.UTest
{
    public class UserRepositoryTest : BaseRepositoryTest
    {
        [Fact]
        public async Task AddsUserAndSetsId()
        {
            var repository  = new UserRepository(_dbContext);
            var testUser = GenerationEntity.CreateUser();
            var userId = await repository.AddAsync(testUser);
            var user = await repository.GetByIdAsync(userId);

            Assert.NotEqual(Guid.Empty, userId);
            Assert.Equal(testUser.Name, user?.Name );
            Assert.Equal(testUser.Password, user?.Password );
            Assert.Equal(testUser.State, user?.State);
        }


        [Fact]
        public async Task UpdateUserAfterAddingIt()
        {
            var repository = new UserRepository(_dbContext);
            var user = GenerationEntity.CreateUser();
            var userId = await repository.AddAsync(user);

            _dbContext.Entry(user).State = EntityState.Detached;

            var newUser = await repository.GetByIdAsync(userId);

            if (newUser == null)
            {
                Assert.NotNull(newUser);
                return;
            }

            Assert.NotSame(user, newUser);
            var newName = Guid.NewGuid().ToString();
            var newPassword = Guid.NewGuid().ToString();
            var newState = !user.State;

            newUser.Name = newName;
            newUser.Password = newPassword;
            newUser.State = newState;

            await repository.UpdateAsync(newUser);
            var updatedUser = (await repository.GetByIdAsync(newUser.Id));

            Assert.NotNull(updatedUser);
            Assert.NotEqual(user.Name, updatedUser?.Name);
            Assert.NotEqual(user.Password, updatedUser?.Password);
            Assert.NotEqual(user.State, updatedUser?.State);
            Assert.Equal(newUser.Id, updatedUser?.Id);
        }


        public async Task GetByName()
        {
            var repository = new UserRepository(_dbContext);
            var userTest = GenerationEntity.CreateUser();
            var userId = await repository.AddAsync(userTest);
            var user = await repository.GetByNameAsync(userTest.Name);

            Assert.NotEqual(Guid.Empty, userId);
            Assert.Equal(userTest.Name, user?.Name);
            Assert.Equal(userTest.Password, user?.Password);
            Assert.Equal(userTest.State, user?.State);
        }

        [Fact]
        public async Task FindByName()
        {
            var repository = new UserRepository(_dbContext);
            var findName = "find";

            var user1 = GenerationEntity.CreateUser();
            user1.Name = findName;

            var user2 = GenerationEntity.CreateUser();
            user2.Name += findName; 

            var notFindUser = GenerationEntity.CreateUser();

            await repository.AddAsync(notFindUser);
            await repository.AddAsync(user1);
            await repository.AddAsync(user2);

            var users = repository.FindByName(findName);
            Assert.True(users.Any());
            Assert.True(users.All(x => x.Name.Contains(findName)));
        }
    }

}