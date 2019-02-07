using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace Infoblog.Controllers
{
    public class SendEmailController : Controller
    {
        private string receiver;

        // GET: SendEmail
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("oru.infoblog@gmail.com", "Infoblog");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Aa12345!";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
        [HttpPost]
        public ActionResult EmailWhenPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("oru.infoblog@gmail.com", "Infoblog");
                    var receiverEmail = new MailAddress("henrik.ojeda@hotmail.com", "Receiver");
                    var password = "Aa12345!";
                    var subject = "Nytt blogginlägg";
                    var body = "Ett nytt inlägg om porr har lagts till";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {

                        smtp.Send(mess);
                    }
                    return View();
                }
            }

            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();

        }
    }
}