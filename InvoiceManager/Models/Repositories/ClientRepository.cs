using InvoiceManager.Models.Domains;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceManager.Models.Repositories
{
    public class ClientRepository
    {
        public List<Client> GetClients(string userId)
        {
            using(var context = new ApplicationDbContext())
            {
                return context.Clients.Where(u => u.UserId == userId).ToList();
            }
        }
    }
}