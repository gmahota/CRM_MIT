using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CRM.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {


        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public class Email
    {
        internal void sendEmail(string empresa,string message,string to,string cc="")
        {
            try
            {

                string filename = null;
                string mailboy = message;

                bool enviado = false;
                enviado = false;


                //imprimirPdf(codigoCliente);

                try
                {
                    filename = (@"wwwroot/Content/Signature.html");
                    // "~/Content/Reports/template.htm";

                    mailboy = System.IO.File.ReadAllText(filename);
                    //mailboy = mailboy.Replace("##FirstName##", objContacto.Titulo + " " + objContacto.PrimeiroNome + " " + objContacto.UltimoNome);
                    //mailboy = mailboy.Replace("##cliente##", objContacto.Nome);
                    ////mailboy = mailboy.Replace("##quantidade##", ds.Tables[0].Rows[0]["Quantidade"].ToString());
                    //mailboy = mailboy.Replace("##divida##", objContacto.ValorPendente.ToString());
                    //mailboy = mailboy.Replace("##empresa##", empresadb.CodEmpresaPri);

                    SmtpClient Smtp_Server = new SmtpClient();
                    MailMessage e_mail = new MailMessage();

                    //Smtp_Server.UseDefaultCredentials = true;
                    //Smtp_Server.Credentials = new System.Net.NetworkCredential("gmahota@mit.co.mz", "Agnes27012015");
                    //Smtp_Server.Port = 587;
                    //Smtp_Server.EnableSsl = true;
                    //Smtp_Server.Host = "smtp.gmail.com";



                    //Smtp_Server.UseDefaultCredentials = empresadb.UseDefaultCredentials.Value;
                    //Smtp_Server.Credentials = new System.Net.NetworkCredential(empresadb.Email, empresadb.Credentials);
                    //Smtp_Server.Port = empresadb.Port.Value;
                    //Smtp_Server.EnableSsl = empresadb.EnableSsl.Value;
                    //Smtp_Server.Host = empresadb.Host;

                    e_mail = new MailMessage();
                    e_mail.From = new MailAddress("avisos@accsys.co.mz");
                    e_mail.To.Add(to);
                    e_mail.CC.Add(cc);

                    e_mail.Subject = "Facturas Pendentes - " + empresa;

                    e_mail.IsBodyHtml = true;


                    e_mail.Body = mailboy;
                    try
                    {
                        // e_mail.Attachments.Add(new System.Net.Mail.Attachment(imprimirPdf(objContacto.Cliente), "Extrato Pendentes.pdf"));

                    }
                    catch (Exception e)
                    {
                        e_mail.Body = mailboy + "/n" + e.Message;
                    }


                    //if (files != null)
                    //{
                    //    foreach (var file in files)
                    //    {
                    //        e_mail.Attachments.Add(new System.Net.Mail.Attachment(file.InputStream, Path.GetFileName(file.FileName)));
                    //    }
                    //}

                    //if (!string.IsNullOrEmpty(anexo1))
                    //    e_mail.Attachments.Add(new System.Net.Mail.Attachment(anexo1));
                    //if (!string.IsNullOrEmpty(anexo2))
                    //    e_mail.Attachments.Add(new System.Net.Mail.Attachment(anexo2));

                    Smtp_Server.Send(e_mail);

                    enviado = true;
                }
                catch (Exception error_t)
                {
                    //Interaction.MsgBox(error_t.ToString());
                    enviado = false;
                }
            }
            catch (Exception ex)
            {
            }
        }


        // Use NuGet to install SendGrid (Basic C# client lib) 
        internal void configSendGridasync(string empresa,string email, string to, string cc, string ficheiro, string folderServer)
        {
            string filename = Path.Combine(folderServer, "Content\\template.html");// "~/Content/Reports/template.htm";

            string template = Path.Combine(folderServer, "Content\\Signature.html");

            string mailboy = System.IO.File.ReadAllText(filename);
            //filename = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/Reports/emailTemplate.htm");// "~/Content/Reports/template.htm";

            string emailTemplate = System.IO.File.ReadAllText(template);

            var myMessage = new SendGridMessage();
            //myMessage.AddTo("guimaraesmahota@gmail.com");

            var emails = to.Split(';');

            foreach(string e in emails)
            {
                if (e !="" && e!=" ")
                    myMessage.AddTo(e);
            }

            emails = cc.Split(';');

            foreach (string e in emails)
            {
                if (e != "" && e != " ")
                    myMessage.AddCc(e);
            }

            myMessage.From = new System.Net.Mail.MailAddress(
                                email, "Email Avisos Accsys ");
            myMessage.Subject = "Avisos De Pendentes - " + empresa;

            if (ficheiro != null)
            {
                using (var attachmentFileStream = new FileStream(ficheiro, FileMode.Open))
                {
                    myMessage.AddAttachment(attachmentFileStream, attachmentFileStream.Name);
                }
            }

            //myMessage.AddAttachment(@ficheiro);
            myMessage.Text = mailboy;
            myMessage.Html = mailboy;

            //myMessage.EnableTemplate(emailTemplate);

            myMessage.EnableTemplateEngine("4ea35a49-5415-4d02-81e3-8adc54650b31");

            var credentials = new NetworkCredential(
                       "gmahota",
                       "Accsys2011!"
                       );

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                System.Diagnostics.Trace.TraceError("Failed to create Web transport.");

            }
        }

    }
}
