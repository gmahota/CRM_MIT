
using System.Threading.Tasks;

using Microsoft.Extensions.OptionsModel;
using MIT.CRM.Models.Helper;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using System.IO;
using System.Net;
using System;

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

        public async Task SendAsync(string from,string from_name, string to,string cc, string subject, string messageBody, string host)
        {
            

            System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;

            var message = new MimeMessage();
            var e_from = new MailboxAddress(from, from_name);
            e_from.Encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            
            message.From.Add(e_from);

            var e_to = new MailboxAddress("", to);
            e_to.Encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            message.To.Add(e_to);

            if (cc != null && cc != "" )
            {
                message.Cc.Add(new MailboxAddress("", cc));
            }

            message.Subject = subject;
          

            var builder = new BodyBuilder();

            //Microsoft.Extensions.PlatformAbstractions.IApplicationEnvironment
            //var app_environment = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Runtime.RuntimePath;//
            var app_environment = host;

            var filename = "http://"+ app_environment + "/templates/Mail/Signature.html";

            WebClient clients = new WebClient();
            
            string mailboy = clients.DownloadString(new Uri(filename));


            //var mailboy = System.IO.File.ReadAllText(filename);
            var image_source = new Uri("http://" + app_environment + "/images/M32G2.png");

            
            var image = builder.LinkedResources.Add("M32G2.png", clients.DownloadData(image_source)) ;
            
            image.ContentId = Path.GetFileName("M32G2.png");


            //imagepath= app_environment.ApplicationBasePath + 

            mailboy = mailboy.Replace("#imageLogo#", image.ContentId);
            mailboy = mailboy.Replace("#body#", messageBody);
            
            
            builder.HtmlBody = mailboy;
            //message.Body.ContentLocation = "pt-PT";
            //builder.HtmlBody = messageBody;



            //builder.HtmlBody = string.Format(@"<p>Hey!</p><img src=""cid:{0}"">", image.ContentId);
           
            message.Body = builder.ToMessageBody();
            
            using (var client = new SmtpClient())
            {
               client.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("do-not-reply@meridian32.com", "Meridian321");
                
                client.Send(message);
                client.Disconnect(true);
            }
            

        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }

      
    }
}
