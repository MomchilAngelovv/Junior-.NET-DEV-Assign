namespace JuniorAssign.Services.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using JuniorAssign.Data;
    using JuniorAssign.Services.Users;
    using JuniorAssign.Services.Contragents;

    public class ContragentsServiceTests
    {
        [Fact]
        public async Task Test_Create_Should_Create()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var contragentsService = new ContragentsService(inMemoryDb);
            var usersService = new UsersService(inMemoryDb);

            var createdUsedId = await usersService.CreateAsync("TestUsername", "123");
            await contragentsService.CreateAsync("Test","Testovo","Testov@abv.bg","123456", createdUsedId);

            Assert.Equal(1, inMemoryDb.Contragents.Count());
        }

        [Fact]
        public async Task Test_GetAll_Should_Return_Only_Contragents_For_Current_User()
        {
            using var inMemoryDb = new JuniorAssignDbContext(CreateNewContextOptions());
            var contragentsService = new ContragentsService(inMemoryDb);
            var usersService = new UsersService(inMemoryDb);

            var createdUsedId = await usersService.CreateAsync("TestUsername", "123");
            await contragentsService.CreateAsync("Test", "Testovo", "Testov@abv.bg", "123456", createdUsedId);
            await contragentsService.CreateAsync("Test1", "Testovo1", "Testov1@abv.bg", "1234567", createdUsedId);
            await contragentsService.CreateAsync("Test2", "Testovo2", "Testov2@abv.bg", "12345678", "otherId");

            var allContragents = contragentsService.GetAll(createdUsedId);
            Assert.Equal(2, allContragents.Count());
            Assert.Equal(3, inMemoryDb.Contragents.Count());
        }

        private static DbContextOptions CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<JuniorAssignDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        }
    }
}
