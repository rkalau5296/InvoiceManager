﻿using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using Microsoft.AspNet.Identity;
using Rotativa;
using Rotativa.Options;
using System;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class PrintController : Controller
    {       
        private InvoiceRepository _invoiceRepository = new InvoiceRepository();
        private ClientRepository _clientRepository = new ClientRepository();
        private ProductRepository _productRepository = new ProductRepository();

        //INVOICE print--

        public ActionResult InvoiceToPdf(int id)
        {
            var handle = Guid.NewGuid().ToString();
            var userId = User.Identity.GetUserId();
            var invoice = _invoiceRepository.GetInvoice(id, userId);

            TempData[handle] = GetPdfContent(invoice);

            return Json(
                new
                {
                    FileGuid = handle,
                    FileName = $@"Faktura_{invoice.Id}.pdf"
                });            
        }
        private byte[] GetPdfContent(Invoice invoice)
        {
            var pdfResult = new ViewAsPdf(@"InvoiceTemplate", invoice)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
            };

            return pdfResult.BuildFile(ControllerContext);
        }
        public ActionResult DownloadInvoicePdf(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] == null)
            {
                throw new Exception("Błąd przy próbie exportu faktury do PDF.");
            }

            var data = TempData[fileGuid] as byte[];
            return File(data, "application/pdf", fileName);
        }

        //CLIENT print--

        public ActionResult ClientToPdf(int id)
        {
            var handle = Guid.NewGuid().ToString();
            var userId = User.Identity.GetUserId();
            var client = _clientRepository.GetClient(id, userId);

            TempData[handle] = GetClientPdfContent(client);

            return Json(
                new
                {
                    FileGuid = handle,
                    FileName = $@"Klient_{client.Id}.pdf"
                });
        }
        private byte[] GetClientPdfContent(Client client)
        {
            var pdfResult = new ViewAsPdf(@"ClientTemplate", client)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
            };

            return pdfResult.BuildFile(ControllerContext);
        }
        public ActionResult DownloadClientPdf(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] == null)
            {
                throw new Exception("Błąd przy próbie exportu faktury do PDF.");
            }

            var data = TempData[fileGuid] as byte[];
            return File(data, "application/pdf", fileName);
        }

        //PRODUCT print--

        public ActionResult ProductToPdf(int id)
        {
            var handle = Guid.NewGuid().ToString();
            var userId = User.Identity.GetUserId();
            var product = _productRepository.GetProduct(id, userId);

            TempData[handle] = GetProductPdfContent(product);

            return Json(
                new
                {
                    FileGuid = handle,
                    FileName = $@"Product{product.Id}.pdf"
                });
        }
        private byte[] GetProductPdfContent(Product product)
        {
            var pdfResult = new ViewAsPdf(@"ProductTemplate", product)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
            };

            return pdfResult.BuildFile(ControllerContext);
        }
        public ActionResult DownloadProductPdf(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] == null)
            {
                throw new Exception("Błąd przy próbie exportu produktu do PDF.");
            }

            var data = TempData[fileGuid] as byte[];
            return File(data, "application/pdf", fileName);
        }
    }
}