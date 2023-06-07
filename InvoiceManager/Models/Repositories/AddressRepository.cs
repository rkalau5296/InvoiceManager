using InvoiceManager.Models.Domains;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceManager.Models.Repositories
{
    public class AddressRepository
    {
        public List<Address> GetAddresses()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Addresses.ToList();
            }
        }

        public Address GetAddress(int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Addresses.Single(x => x.Id == userId);
            }
        }
    }
}