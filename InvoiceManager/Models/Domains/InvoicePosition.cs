﻿using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Models.Domains
{
    public class InvoicePosition
    {
        public int Id { get; set; }
        public int Lp { get; set; }
        public int InvoiceId { get; set; }
        [Display(Name = "Wartość")]
        public decimal Value { get; set; }
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }
        public Invoice Invoice { get; set; }
        [Display(Name = "Produkt")]
        public Product Product { get; set; }        
        public int ProductId { get; set; }
    }
}