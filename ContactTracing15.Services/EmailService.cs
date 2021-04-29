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
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("x23730389@gmail.com", "1380slfF");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            //Setting From , To and CC
            mail.From = new MailAddress("x23730389@gmail.com");
            mail.Subject = "YOU GOT A FREND WITH COVID";
            mail.Body = "HAHA BRO ITS JUST A TEST";
            mail.To.Add(new MailAddress(contact.Email));
            smtpClient.Send(mail);
        }
    }
}
