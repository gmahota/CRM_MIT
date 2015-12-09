using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Configuration;
using Microsoft.Extensions.OptionsModel;
using MIT.CRM.Models.Helper;

namespace MIT.CRM.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly IOptions<AppSettings> _appSettings;

        public AuthMessageSender(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public async Task SendAsync(string from,string from_name, string to,string cc, string subject, string message)
        {
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();

            // this defines email and name of the sender
            myMessage.From = new MailAddress(from, from_name);

            // set where we are sending the email
            myMessage.AddTo(to);

            if (cc != null && cc != "")
                myMessage.AddCc(cc);

            myMessage.Subject = subject;

            // make sure all your messages are formatted as HTML
            myMessage.Html = message;
            myMessage.Text = message;

            
            
            myMessage.EnableTemplateEngine("4ea35a49-5415-4d02-81e3-8adc54650b31");

            // Create credentials, specifying your SendGrid username and password.
            var credentials = new NetworkCredential (
                "gmahota",
                "Accsys2011!"
            );

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            await transportWeb.DeliverAsync(myMessage);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }

      
    }
}
