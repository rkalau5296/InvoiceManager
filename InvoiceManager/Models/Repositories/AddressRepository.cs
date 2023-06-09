﻿using InvoiceManager.Models.Domains;
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

        public Address GetAddress(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Addresses.Single(x => x.Id == id);
            }
        }        
        public void Update(Address address)
        {
            using (var context = new ApplicationDbContext())
            {
                var addressToUpdate = context.Addresses.Single(x => x.Id == address.Id);

                addressToUpdate.Street = address.Street;
                addressToUpdate.Number = address.Number;
                addressToUpdate.City = address.City;
                addressToUpdate.PostalCode = address.PostalCode;

                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var addressToDelete = context.Addresses.Single(x => x.Id == id);

                context.Addresses.Remove(addressToDelete);
                context.SaveChanges();
            }
        }
    }
}