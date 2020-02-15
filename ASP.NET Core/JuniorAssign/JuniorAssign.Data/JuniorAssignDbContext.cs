namespace JuniorAssign.Data
{
    using Microsoft.EntityFrameworkCore;

    using JuniorAssign.Data.Models;

    public class JuniorAssignDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contragent> Contragents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=.;Database=JuniorAssignDb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
