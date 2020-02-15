namespace JuniorAssign.Data
{
    using Microsoft.EntityFrameworkCore;

    using JuniorAssign.Data.Models;

    public class JuniorAssignDbContext : DbContext
    {
        public JuniorAssignDbContext(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Contragent> Contragents { get; set; }
    }
}
