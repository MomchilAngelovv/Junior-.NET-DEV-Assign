namespace JuniorAssign.Services.Contragents
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using JuniorAssign.Data.Models;

    public interface IContragentsService
    {
        Task CreateAsync(string name, string address, string email, string vatNumber, string userId);
        IEnumerable<Contragent> GetAll(string userId);
        Contragent GetBy(string id);
    }
}
