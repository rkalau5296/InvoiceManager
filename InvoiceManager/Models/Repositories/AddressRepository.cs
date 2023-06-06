using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Repositories
{
    public class AddressRepository
    {
        public List<Address> GetAddress()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Addresses.ToList();
            }
        }
    }
}