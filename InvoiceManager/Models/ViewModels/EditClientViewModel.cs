using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.ViewModels
{
    public class EditClientViewModel
    {
        public Address Address { get; set; }
        public List<Client> Clients { get; set; }
        public string Heading { get; set; }
        public Client Client { get; set; }
    }
}