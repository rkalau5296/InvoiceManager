using InvoiceManager.Models.Domains;

namespace InvoiceManager.Models.ViewModels
{
    public class ContactViewModel
    {
        public ApplicationUser applicationUser { get; set; }
        public Address address { get; set; }
    }
}