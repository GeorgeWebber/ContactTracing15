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
using Microsoft.Extensions.Configuration;

namespace ContactTracing15.Services
{
    public class EmailService : IEmailService
    {

        IConfiguration _config;

        public EmailService (IConfiguration config)
        {
            _config = config;
        }

        public void ContactByEmail(Contact contact)
        {
            /*
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("groupdesignproject15@gmail.com", _config["emailPassword"]);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            //Setting From , To and CC
            mail.From = new MailAddress("groupdesignproject15@gmail.com");
            mail.Subject = "Test email - IMPORTANT: Confirmed Covid-19 Contact";


            string messageBody = String.Format("Dear {0} {1},", contact.Forename, contact.Surname);
            messageBody += "\nWe regret to inform you that you have been listed as a close contact of someone who has subsequently tested positive for Covid-19.";
            messageBody += "\nAs a result, you are required by law to self-isolate for 10 days. Further technical guidance is listed at ABCD.";
            messageBody += "\n\nIf you think this message has been sent in error, please call XXX to speak to a trained contact tracer.";
            messageBody += "\n\nYours sincerely,\nContactTracing15";
            messageBody += "\n\nFurther details about how to contact us can be found at https://contacttracing15online.azurewebsites.net ";

            messageBody += "\n\n\nPlease note that is an automated test email from a student project and not any reflection on real life events.";

            mail.Body = messageBody;
            mail.To.Add(new MailAddress(contact.Email));
            smtpClient.Send(mail);
            */
        }
    }
}
