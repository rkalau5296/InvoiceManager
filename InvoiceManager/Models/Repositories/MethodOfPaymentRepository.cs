using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Repositories
{
    public class MethodOfPaymentRepository
    {
        public List<MethodOfPayment> GetMethodOfPayments(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.MethodOfPayments.Where(x => x.UserId == userId).ToList();
            }
        }

        public MethodOfPayment GetMethodOfPayment(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.MethodOfPayments.Single(p => p.Id == id && p.UserId == userId);
            }
        }
        public void Add(MethodOfPayment methodOfPayment)
        {
            using (var context = new ApplicationDbContext())
            {
                context.MethodOfPayments.Add(methodOfPayment);
                context.SaveChanges();
            }
        }
        public void Update(MethodOfPayment methodOfPayment)
        {
            using (var context = new ApplicationDbContext())
            {
                var methodOfPaymentToUpdate = context.MethodOfPayments.Single(x => x.Id == methodOfPayment.Id && x.UserId == methodOfPayment.UserId);

                methodOfPaymentToUpdate.Name = methodOfPayment.Name;                
                context.SaveChanges();
            }
        }
        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var methodOfPaymentToDelete = context.MethodOfPayments.Single(x => x.Id == id && x.UserId == userId);

                context.MethodOfPayments.Remove(methodOfPaymentToDelete);
                context.SaveChanges();
            }
        }
    }
}