using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.ViewModels
{
    public class EditProductViewModel
    {
        public string Heading { get; set; }
        public Product Product { get; set; }
    }
}