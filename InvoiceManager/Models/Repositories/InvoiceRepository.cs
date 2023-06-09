
using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace InvoiceManager.Models.Repositories
{
    public class InvoiceRepository
    {
        public List<Invoice> GetInvoices(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Invoices
                    .Include(c =>c.Client)
                    .Where(u => u.UserId == userId)
                    .ToList();
            }
        }

        public Invoice GetInvoice(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Invoices
                    .Include(ip => ip.InvoicePositions)
                    .Include(ip => ip.InvoicePositions.Select(p => p.Product))
                    .Include(m => m.MethodOfPayment)
                    .Include(u => u.User)
                    .Include(u => u.User.Address)
                    .Include(c => c.Client)
                    .Include(c => c.Client.Address)
                    .Single(x => x.UserId == userId && x.Id == id);                    
            }
        }

        public List<MethodOfPayment> GetMethodOfPayment()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.MethodOfPayments.ToList();
            }
        }

        public InvoicePosition GetInvoicePosition(int invoicePositionId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.InvoicePositions
                    .Include(x => x.Invoice)
                    .Single(x => x.Id == invoicePositionId && x.Invoice.UserId == userId);
            }
        }

        public void Add(Invoice invoice)
        {
            using (var context = new ApplicationDbContext())
            {                
                context.Invoices.Add(invoice);
                context.SaveChanges();
            }
        }

        public void Update(Invoice invoice)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoiceToUpdate = context.Invoices.Single(x => x.Id == invoice.Id && x.UserId == invoice.UserId);

                invoiceToUpdate.ClientId = invoice.ClientId;
                invoiceToUpdate.Comments = invoice.Comments;
                invoiceToUpdate.MethodOfPayment = invoice.MethodOfPayment;
                invoiceToUpdate.PaymentDate = invoice.PaymentDate;
                invoiceToUpdate.Title = invoice.Title;
                context.SaveChanges();
            }
        }

        public void AddPosition(InvoicePosition invoicePosition, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoice = context.Invoices.Single(x=>x.Id == invoicePosition.InvoiceId && x.UserId == userId);
                
                context.InvoicePositions.Add(invoicePosition);
                context.SaveChanges();
            }
        }

        public void UpdatePosition(InvoicePosition invoicePosition, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var positionToUpdate = context.InvoicePositions
                    .Include(x => x.Product)
                    .Include(x => x.Invoice)
                    .Single(x => x.Id == invoicePosition.Id && x.Invoice.UserId == userId);

                positionToUpdate.Lp = invoicePosition.Lp;
                positionToUpdate.ProductId = invoicePosition.ProductId;
                positionToUpdate.Quantity= invoicePosition.Quantity;
                positionToUpdate.Value= invoicePosition.Product.Value * invoicePosition.Value;

                context.SaveChanges();
            }
        }

        public decimal UpdateInvoiceValue(int invoiceId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoice = context.Invoices
                    .Include(x=>x.InvoicePositions)
                    .Single(x => x.Id == invoiceId && x.UserId == userId);

                invoice.Value= invoice.InvoicePositions.Sum(x=> x.Value);                
                context.SaveChanges();
                return invoice.Value;
            }
        }

        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoiceToDelete = context.Invoices.Single(x => x.Id == id && x.UserId == userId);

                context.Invoices.Remove(invoiceToDelete);
                context.SaveChanges();
            }
        }

        internal void DeletePosition(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var positionToDelete = context.InvoicePositions                    
                    .Include(x => x.Invoice)
                    .Single(x => x.Id == id && x.Invoice.UserId == userId);

                context.InvoicePositions.Remove(positionToDelete);

                context.SaveChanges();
            }
        }
    }
}