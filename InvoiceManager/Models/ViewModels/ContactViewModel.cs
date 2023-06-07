using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.ViewModels
{
    public class ContactViewModel
    {
        public ApplicationUser applicationUser { get; set; }
        public Address address { get; set; }
    }
}