using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using TaxiServiceWebUI.Dtos;
using TaxiServiceWebUI.Models;

namespace TaxiServiceWebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IInfoService _infoService;
        public ContactController(IInfoService infoService)
        {
            _infoService = infoService;
        }
        public IActionResult Info()
        {
            var info = _infoService.GetInfo().First();
            InfoViewModel model = new InfoViewModel
            {
                Info = info
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SendMail(InfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                SendContactMail(model.Contact);
                ViewBag.AcceptMessage = "Bize mesaj gönderdiğiniz için teşekkür ederiz. En kısa zamanda dönüş sağlayacağız.";
                return RedirectToAction(nameof(Info));
            }
            return RedirectToAction(nameof(Info));
        }

        private void SendContactMail(ContactDTO contact)
        {
            var credentials = new NetworkCredential("aydincankalyncu@gmail.com", "Aydincan52");
            var mail = new MailMessage()
            {
                From = new MailAddress("aydincankalyncu@gmail.com"),
                Subject = contact.Subject,
                Body = "Merhaba Sayın " + contact.Name + ","+ "<br> İlgilenmiş olduğunuz konu ile ilgili sizlerle kısa sürede iletişime geçilecektir. Teşekkür ederiz.<br><br>"
            };
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress("aydincankalyncu@gmail.com"));
            mail.To.Add(new MailAddress(contact.Email));

            var client = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };
            client.Send(mail);
        }
    }
}
