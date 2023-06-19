using InvoiceManager.Models;
using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using InvoiceManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using System.Xml.Linq;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private InvoiceRepository _invoiceRepository = new InvoiceRepository();
        private ClientRepository _clientRepository = new ClientRepository();
        private ProductRepository _productRepository = new ProductRepository();
        private AddressRepository _addressRepository = new AddressRepository();
        private UserRepository _userRepository = new UserRepository();
        private MethodOfPaymentRepository _methodOfPaymentRepository = new MethodOfPaymentRepository();

        //INVOICE-------

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var invoices = _invoiceRepository.GetInvoices(userId);
            return View(invoices);
        }
        public ActionResult Invoice(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var invoice = id == 0 ? GetNewInvoice(userId) : _invoiceRepository.GetInvoice(id, userId);

            var vm = PrepareInvoiceVm(invoice, userId);
            return View(vm);
        }
        private EditInvoiceViewModel PrepareInvoiceVm(Invoice invoice, string userId)
        {
            return new EditInvoiceViewModel
            {
                Invoice = invoice,
                Heading = invoice.Id == 0 ? "Dodawanie nowej faktury" : "Faktura",
                Clients = _clientRepository.GetClients(userId),
                MethodOfPayments = _invoiceRepository.GetMethodOfPayment()
            };
        }
        private Invoice GetNewInvoice(string userId)
        {
            return new Invoice
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                PaymentDate = DateTime.Now.AddDays(7),
            };
        }
        public ActionResult InvoicePosition(int invoiceId, int invoicePositionId = 0)
        {
            var userId = User.Identity.GetUserId();
            var invoicePosition = invoicePositionId == 0 ? GetNewPosition(invoiceId, invoicePositionId) : _invoiceRepository.GetInvoicePosition(invoicePositionId, userId);

            var vm = PrepareInvoicePositionVm(invoicePosition, userId);
            return View(vm);
        }
        private EditInvoicePositionViewModel PrepareInvoicePositionVm(InvoicePosition invoicePosition, string userId)
        {
            return new EditInvoicePositionViewModel
            {
                InvoicePosition = invoicePosition,
                Heading = invoicePosition.Id == 0 ? "Dodawanie nowej pozycji" : "Pozycja",
                Products = _productRepository.GetProducts(userId),
            };
        }
        private InvoicePosition GetNewPosition(int invoiceId, int invoicePositionId)
        {
            return new InvoicePosition
            {
                InvoiceId = invoiceId,
                Id = invoicePositionId,
            };
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invoice(Invoice invoice)
        {
            var userId = User.Identity.GetUserId();
            invoice.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = PrepareInvoiceVm(invoice, userId);
                return View("Invoice", vm);
            }
            if (invoice.Id == 0)
            {
                _invoiceRepository.Add(invoice);
            }
            else
            {
                _invoiceRepository.Update(invoice);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoicePosition(InvoicePosition invoicePosition)
        {
            var userId = User.Identity.GetUserId();

            var product = _productRepository.GetProduct(invoicePosition.ProductId, userId);

            if (!ModelState.IsValid)
            {
                var vm = PrepareInvoicePositionVm(invoicePosition, userId);
                return View("InvoicePosition", vm);
            }

            invoicePosition.Value = invoicePosition.Quantity * product.Value;

            if (invoicePosition.Id == 0)
            {
                _invoiceRepository.AddPosition(invoicePosition, userId);
            }
            else
            {
                _invoiceRepository.UpdatePosition(invoicePosition, userId);
            }

            _invoiceRepository.UpdateInvoiceValue(invoicePosition.InvoiceId, userId);

            return RedirectToAction("Invoice", new { id = invoicePosition.InvoiceId });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _invoiceRepository.Delete(id, userId);

            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = e.Message });
            }
            return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult DeletePosition(int id, int invoiceId)
        {
            var invoiceValue = 0m;
            try
            {
                var userId = User.Identity.GetUserId();
                _invoiceRepository.DeletePosition(id, userId);
                invoiceValue = _invoiceRepository.UpdateInvoiceValue(invoiceId, userId);

            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = e.Message });
            }
            return Json(new { Success = true, InvoiceValue = invoiceValue });
        }

        //ABOUT----

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //CONTACT----

        [AllowAnonymous]
        public ActionResult Contact()
        {
            var userId = User.Identity.GetUserId();            

            return View(PrepareContactVm(userId));
        }
        private ContactViewModel PrepareContactVm(string userId)
        {
            var user = _userRepository.GetUser(userId);
            return new ContactViewModel
            {
                applicationUser = user,
                address = _addressRepository.GetAddress(user.AddressId)
            };
        }

        //CLIENT----

        [AllowAnonymous]
        public ActionResult Client()
        {
            var userId = User.Identity.GetUserId();
            var clients = _clientRepository.GetClients(userId);
            return View(clients);
        }
        [AllowAnonymous]
        public ActionResult EditClient(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var client = id == 0 ? GetNewClient(userId) : _clientRepository.GetClient(id, userId);

            var vm = PrepareEditClientVm(client);
            return View(vm);
        }
        private Client GetNewClient(string userId)
        {
            return new Client
            {
                UserId = userId,                
            };
        }
        private EditClientViewModel PrepareEditClientVm(Client client)
        {
            return new EditClientViewModel
            {
                Client = client,                
                Heading = client.Id == 0 ? "Dodawanie nowego odbiorcy" : "Odbiorca",                
            };
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Client(Client client)
        {  
            if (!ModelState.IsValid)
            {
                var vm = PrepareEditClientVm(client);
                return View("Client", vm);
            }
            if (client.Id == 0)
            {                
                _clientRepository.Add(client);
            }
            else
            {
                var addressId = _clientRepository.GetAddressId(client.Id);
                client.Address.Id = addressId;
                _addressRepository.Update(client.Address);
                _clientRepository.Update(client);
            }
            return RedirectToAction("Client");
        }
        [HttpPost]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var client = _clientRepository.GetClient(id, userId);
                _clientRepository.Delete(id, userId);                
                _addressRepository.Delete(client.AddressId);
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = e.Message });
            }
            return Json(new { Success = true });
        }

        //PRODUCT---

        [AllowAnonymous]
        public ActionResult Product()
        {
            var userId = User.Identity.GetUserId();
            var products = _productRepository.GetProducts(userId);
            return View(products);
        }
        [AllowAnonymous]
        public ActionResult EditProduct(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var product = id == 0 ? GetNewProduct(userId) : _productRepository.GetProduct(id, userId);

            var vm = PrepareEditProductVm(product);
            return View(vm);
        }
        private Product GetNewProduct(string userId)
        {
            return new Product
            {
                UserId = userId,
            };
        }
        private EditProductViewModel PrepareEditProductVm(Product product)
        {
            return new EditProductViewModel
            {
                Product = product,
                Heading = product.Id == 0 ? "Dodawanie nowego produktu" : "Produkt",
            };
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Product(Product product)
        {
            if (!ModelState.IsValid)
            {
                var vm = PrepareEditProductVm(product);
                return View("Product", vm);
            }
            if (product.Id == 0)
            {
                _productRepository.Add(product);
            }
            else
            {
                _productRepository.Update(product);
            }
            return RedirectToAction("Product");
        }
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _productRepository.Delete(id, userId);                
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = e.Message });
            }
            return Json(new { Success = true });
        }

        //METHOD_OF_PAYMENT---

        [AllowAnonymous]
        public ActionResult MethodOfPayment()
        {
            var userId = User.Identity.GetUserId();
            var methodOfPayment = _methodOfPaymentRepository.GetMethodOfPayments(userId);
            return View(methodOfPayment);
        }
        [AllowAnonymous]
        public ActionResult EditMethodOfPayment(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var methodOfPayment = id == 0 ? GetNewMethodOfPayment(userId) : _methodOfPaymentRepository.GetMethodOfPayment(id, userId);

            var vm = PrepareEditMethodOfPaymentVm(methodOfPayment);
            return View(vm);
        }
        private MethodOfPayment GetNewMethodOfPayment(string userId)
        {
            return new MethodOfPayment
            {
                UserId = userId,
            };
        }
        private EditMethodOfPaymentViewModel PrepareEditMethodOfPaymentVm(MethodOfPayment methodOfPayment)
        {
            return new EditMethodOfPaymentViewModel
            {
                MethodOfPayment = methodOfPayment,
                Heading = methodOfPayment.Id == 0 ? "Dodawanie nowej metody płatności" : "Metoda płatności",
            };
        }
    }
}