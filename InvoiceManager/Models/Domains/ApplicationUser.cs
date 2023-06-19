﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InvoiceManager.Models.Domains
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Invoices = new Collection<Invoice>();
            Clients = new Collection<Client>();
            Products = new Collection<Product>();
            MethodOfPayments = new Collection<MethodOfPayment>();
        }
        [Required]
        public string Name { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<MethodOfPayment> MethodOfPayments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}