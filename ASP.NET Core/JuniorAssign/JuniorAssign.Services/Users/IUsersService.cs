namespace JuniorAssign.Services.Users
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using JuniorAssign.Data.Models;

    public interface IUsersService
    {
        Task<string> CreateAsync(string username, string password);
        Task UpdateAsync(string id, string newUsername, string newPassword);
        List<User> GetAll();
        User GetBy(string id);
        User GetBy(string username, string password);
    }
}
