
using System.Threading.Tasks;

using Microsoft.Extensions.OptionsModel;
using MIT.CRM.Models.Helper;
using MimeKit;

using MailKit.Net.Smtp;
using MailKit;


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

        public async Task SendAsync(string from,string from_name, string to,string cc, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("gmahota", "gmahota@mit.co.mz"));
            message.To.Add(new MailboxAddress("Guimaraes", "guimaraesmahota@gmail.com"));
            message.Subject = "How you doin?";

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            builder.TextBody = @"Hey Alice,

                What are you up to this weekend? Monica is throwing one of her parties on
                Saturday and I was hoping you could make it.

                Will you be my +1?

                -- Joey
                ";

            // In order to reference sexy-pose.jpg from the html text, we'll need to add it 
            // to builder.LinkedResources and then use its Content-Id value in the img src.
            //MimeEntity image = builder.LinkedResources.Add(@"C:\Users\gmahota\Pictures\frontimageAccSys.jpg");

            // Set the html version of the message text
            //builder.HtmlBody = string.Format(@"<p>Hey Alice,<br>
            //    <p>What are you up to this weekend? Monica is throwing one of her parties on
            //    Saturday and I was hoping you could make it.<br>
            //    <p>Will you be my +1?<br>
            //    <p>-- Joey<br>
            //    <center><img src=""cid:{0}""></center>", image.ContentId);


            builder.HtmlBody = string.Format(@"<p>Hey Alice,<br>
                <p>What are you up to this weekend? Monica is throwing one of her parties on
                Saturday and I was hoping you could make it.<br>
                <p>Will you be my +1?<br>
                <p>-- Joey<br>
                <center><img src=""cid:{0}""></center>");

            // We may also want to attach a calendar event for Monica's party...
            //builder.Attachments.Add(@"C:\Users\Joey\Documents\party.ics");

            // Now we just need to set the message body and we're done
            message.Body = builder.ToMessageBody();


            using (var client = new SmtpClient())
            {
                client.Connect("smtp.friends.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("joey", "password");

                client.Send(message);
                client.Disconnect(true);
            }







            // Create the email object first, then add the properties.
            //var myMessage = new SendGridMessage();

            //// this defines email and name of the sender
            //myMessage.From = new MailAddress(from, from_name);

            //// set where we are sending the email
            //myMessage.AddTo(to);

            //if (cc != null && cc != "")
            //    myMessage.AddCc(cc);

            //myMessage.Subject = subject;

            //// make sure all your messages are formatted as HTML
            //myMessage.Html = message;
            //myMessage.Text = message;



            //myMessage.EnableTemplateEngine("4ea35a49-5415-4d02-81e3-8adc54650b31");

            //// Create credentials, specifying your SendGrid username and password.
            //var credentials = new NetworkCredential (
            //    "gmahota",
            //    "Accsys2011!"
            //);

            //// Create an Web transport for sending email.
            //var transportWeb = new Web(credentials);

            //// Send the email.
            //await transportWeb.DeliverAsync(myMessage);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }

      
    }
}
