namespace JuniorAssign.Services.Tests
{
    using System;
    using System.Threading.Tasks;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using JuniorAssign.Data;
    using JuniorAssign.Services.Users;

    public class UnitTest1
    {
        [Fact]
        public async Task Test_User_Create_Properly_Create()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var usersService = new UsersService(inMemoryDb);

            await usersService.CreateAsync("Monkata", "123");

            Assert.Equal(1, await inMemoryDb.Users.CountAsync());
        }

        private static DbContextOptions CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<JuniorAssignDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        }
    }
}
