using InvoiceManager.Models.Domains;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceManager.Models.Repositories
{
    public class UserRepository
    {
        public List<ApplicationUser> GetUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.ToList();
            }
        }

        public ApplicationUser GetUser(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.Single(x => x.Id == userId);
            }
        }
    }
}