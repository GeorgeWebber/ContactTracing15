using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;
using ContactTracing15.Services;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace ContactTracing15.Services
{
    public class EmailService : IEmailService
    {
        public void ContactByEmail(Contact contact)
        {
            //TODO send email to contact
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 25);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            //Setting From , To and CC
            mail.From = new MailAddress("x23730389@gmail.com", "Testing Email");
            mail.To.Add(new MailAddress(contact.Email));
            smtpClient.Send(mail);
        }
    }
}
