namespace JuniorAssign.Services.Users
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    using JuniorAssign.Data;
    using JuniorAssign.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly JuniorAssignDbContext db;

        public UsersService(JuniorAssignDbContext db)
        {
            this.db = db;
        }

        public async Task<string> CreateAsync(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = this.HashPassword(password)
            };

            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();

            return user.Id;
        }

        public List<User> GetAll()
        {
            return this.db.Users.ToList();
        }

        public User GetBy(string id)
        {
            return this.db.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetBy(string username, string password)
        {
            var passwordHash = this.HashPassword(password);
            return this.db.Users.FirstOrDefault(u => u.Username == username && u.Password == passwordHash);
        }

        public async Task UpdateAsync(string id, string newUsername, string newPassword)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Id == id);

            user.Username = newUsername;
            user.Password = this.HashPassword(newPassword);

            this.db.Users.Update(user);
            await this.db.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
