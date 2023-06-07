using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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