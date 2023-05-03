using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var invoices = new List<Invoice>()
            {
                new Invoice
                {
                    Id = 1,
                    Title = "Fa/01/2022",
                    CreatedDate = DateTime.Now,
                    PaymentDate = DateTime.Now,
                    Value = 999,
                    Client = new Client { Name = "Klient1" }
                },
                new Invoice
                {
                    Id = 2,
                    Title = "Fa/02/2022",
                    CreatedDate = DateTime.Now,
                    PaymentDate = DateTime.Now,
                    Value = 932299,
                    Client = new Client { Name = "Klient2" }
                }
            };
            return View(invoices);
        }
        public ActionResult Invoice()
        {            
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}