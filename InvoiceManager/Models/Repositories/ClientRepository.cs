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
        public void Add(Client client)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Clients.Add(client);                
                context.SaveChanges();
            }
        }

        public void Update(Client client)
        {
            using (var context = new ApplicationDbContext())
            {
                var clientToUpdate = context.Clients.Single(x => x.Id == client.Id && x.UserId == client.UserId);

                clientToUpdate.Name = client.Name;
                clientToUpdate.Email = client.Email;                
                context.SaveChanges();
            }
        }
        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var clientToDelete = context.Clients.Single(x => x.Id == id && x.UserId == userId);

                context.Clients.Remove(clientToDelete);
                context.SaveChanges();
            }
        }
    }
}