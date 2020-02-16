namespace JuniorAssign.Services.Contragents
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using JuniorAssign.Data;
    using JuniorAssign.Data.Models;

    public class ContragentsService : IContragentsService
    {
        private readonly JuniorAssignDbContext db;

        public ContragentsService(JuniorAssignDbContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(string name, string address, string email, string vatNumber, string userId)
        {
            var contragent = new Contragent
            {
                Name = name,
                Address = address,
                Email = email,
                VatNumber = vatNumber,
                UserId = userId
            };

            await this.db.Contragents.AddAsync(contragent);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<Contragent> GetAll(string userId)
        {
            return this.db.Contragents.Where(c => c.UserId == userId).ToList();
        }

        public Contragent GetBy(string id)
        {
            return this.db.Contragents.FirstOrDefault(c => c.Id == id);
        }
    }
}
