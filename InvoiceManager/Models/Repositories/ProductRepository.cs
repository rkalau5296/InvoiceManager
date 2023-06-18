using InvoiceManager.Models.Domains;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceManager.Models.Repositories
{
    public class ProductRepository
    {
        public List<Product> GetProducts(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Products.Where(x=>x.UserId == userId).ToList();
            }
        }

        public Product GetProduct(int productId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Products.Single(p => p.Id == productId && p.UserId == userId);
            }
        }
        public void Add(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public void Update(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                var productToUpdate = context.Products.Single(x => x.Id == product.Id && x.UserId == product.UserId);

                productToUpdate.Name = product.Name;
                productToUpdate.Value = product.Value;
                context.SaveChanges();
            }
        }
        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var productToDelete = context.Products.Single(x => x.Id == id && x.UserId == userId);

                context.Products.Remove(productToDelete);
                context.SaveChanges();
            }
        }
    }
}