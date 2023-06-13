using InvoiceManager.Models.Domains;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;

namespace InvoiceManager.Models.Repositories
{
    public class ClientRepository
    {
        public List<Client> GetClients(string userId)
        {
            using(var context = new ApplicationDbContext())
            {
                return context.Clients
                    .Include(x=>x.Address)
                    .Where(u => u.UserId == userId)                    
                    .ToList();
            }
        }

        public Client GetClient(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Clients
                    .Include(x => x.Address)
                    .Single(x => x.UserId == userId && x.Id == id);
            }
        }
    }
}