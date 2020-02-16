namespace JuniorAssign.Services.Tests
{
    using System;
    using System.Threading.Tasks;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using JuniorAssign.Data;
    using JuniorAssign.Services.Users;

    public class UsersServiceTests
    {
        [Fact]
        public async Task Test_Create_Should_Create()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var usersService = new UsersService(inMemoryDb);

            await usersService.CreateAsync("Monkata", "123");

            Assert.Equal(1, await inMemoryDb.Users.CountAsync());
        }

        [Fact]
        public async Task Test_GetAll_Should_Return_All_Users()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var usersService = new UsersService(inMemoryDb);

            await usersService.CreateAsync("Monkata", "123");
            await usersService.CreateAsync("Goshkata", "123");
            await usersService.CreateAsync("Mitko", "123");

            Assert.Equal(3, await inMemoryDb.Users.CountAsync());
        }

        [Fact]
        public async Task Test_GetBy_Id_Should_Return_Correct_User()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var usersService = new UsersService(inMemoryDb);

            var createdUserId = await usersService.CreateAsync("Test", "123");
            var user = usersService.GetBy(createdUserId);

            Assert.Equal("Test", user.Username);
        }

        [Fact]
        public async Task Test_GetBy_Username_And_Password_Should_Return_Correct_User()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var usersService = new UsersService(inMemoryDb);

            await usersService.CreateAsync("Test", "123");
            var user = usersService.GetBy("Test", "123");

            Assert.Equal("Test", user.Username);
        }

        [Fact]
        public async Task Test_GetUserIdBy_Username_Should_Return_Correct_User()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var usersService = new UsersService(inMemoryDb);

            var createdUserId = await usersService.CreateAsync("Test", "123");
            var userId = usersService.GetUserIdBy("Test");

            Assert.Equal(userId, createdUserId);
        }

        [Fact]
        public async Task Test_IsUsernameUsed_Should_Return_True_If_Username_Exists()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var usersService = new UsersService(inMemoryDb);

            await usersService.CreateAsync("Test", "123");
            var isUsernameUsed = usersService.IsUsernameUsed("Test");

            Assert.True(isUsernameUsed);
        }

        [Fact]
        public async Task Test_IsUsernameUsed_Should_Return_False_If_Username_Does_Not_Exists()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var usersService = new UsersService(inMemoryDb);

            await usersService.CreateAsync("Test", "123");
            var isUsernameUsed = usersService.IsUsernameUsed("Invalid");

            Assert.False(isUsernameUsed);
        }

        [Fact]
        public async Task Test_UpdateAsync_Should_Propelry_Update()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var usersService = new UsersService(inMemoryDb);

            var createdUserId = await usersService.CreateAsync("Test", "123");
            await usersService.UpdateAsync(createdUserId, "NewTest", "New123");
            var user = usersService.GetBy(createdUserId);

            Assert.Equal("NewTest",user.Username);
        }

        private static DbContextOptions CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<JuniorAssignDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        }
    }
}
