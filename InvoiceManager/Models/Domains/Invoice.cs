using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManager.Models.Domains
{
    public class Invoice
    {
        public Invoice()
        {
            InvoicePositions = new Collection<InvoicePosition>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        public decimal Value { get; set; }
        [Display(Name = "Metoda płatności")]
        public int MethodOfPaymentId { get; set; }
        [Display(Name = "Data płatności")]
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Uwagi")]
        public string Comments { get; set; }
        [Display(Name = "Klient")]
        public int ClientId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public MethodOfPayment MethodOfPayment { get; set; }
        public Client Client { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<InvoicePosition> InvoicePositions { get; set; }
    }
}