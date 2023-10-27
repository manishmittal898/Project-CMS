using CMS.Core.FixedValue;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CMS.Core.ServiceHelper.Method
{
    public class EmailHelper
    {
        // static IHostingEnvironment _env;
        private readonly MailSettings _mailSettings;
        private readonly IConfiguration _configuration;

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailSettings = _configuration.GetSection(Constants.SMTP_SERVER).Get<MailSettings>();

        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {

                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.UserName);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(mailRequest.ToEmail, mailRequest.ToEmail);
                emailMessage.To.Add(emailTo);
                emailMessage.Subject = mailRequest.Subject;
                BodyBuilder emailBodyBuilder = new BodyBuilder
                {
                    TextBody = mailRequest.Body
                };
                emailMessage.Body = emailBodyBuilder.ToMessageBody();

                if (mailRequest.Attachments != null)
                {
                    byte[] attachmentFileByteArray;
                    foreach (IFormFile attachmentFile in mailRequest.Attachments)
                    {
                        if (attachmentFile.Length > 0)
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                attachmentFile.CopyTo(memoryStream);
                                attachmentFileByteArray = memoryStream.ToArray();
                            }
                            _ = emailBodyBuilder.Attachments.Add(attachmentFile.FileName, attachmentFileByteArray, MimeKit.ContentType.Parse(attachmentFile.ContentType));
                        }
                    }
                }

                MailKit.Net.Smtp.SmtpClient emailClient = new MailKit.Net.Smtp.SmtpClient();

                emailClient.Connect(_mailSettings.Host, _mailSettings.Port, true);
                emailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                string str = await emailClient.SendAsync(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        //public async Task SendEmailAsync(MailRequest mailRequest)
        //{
        //    try
        //    {

        //        MailMessage message = new MailMessage
        //        {
        //            From = new MailAddress(_mailSettings.UserName, _mailSettings.DisplayName),
        //            Subject = mailRequest.Subject,
        //            Body = mailRequest.Body,

        //        };
        //        message.IsBodyHtml = false;
        //        // Add recipients
        //        message.To.Add(mailRequest.ToEmail);
        //        if (mailRequest.Attachments != null && mailRequest.Attachments.Count > 0)
        //        {
        //            foreach (IFormFile attachmentFile in mailRequest.Attachments)
        //            {
        //                if (attachmentFile.Length > 0)
        //                {
        //                    using (MemoryStream memoryStream = new MemoryStream())
        //                    {
        //                        attachmentFile.CopyTo(memoryStream);
        //                        Attachment attachment = new Attachment(memoryStream, MediaTypeNames.Application.Pdf);
        //                        message.Attachments.Add(attachment);

        //                    }

        //                }

        //            }

        //        }

        //        // Create an instance of the SmtpClient
        //        using (SmtpClient client = new SmtpClient())
        //        {
        //            client.Host = _mailSettings.Host;
        //            client.Timeout = 60000;
        //              client.EnableSsl = false;
        //            client.Port = 25;
        //            client.Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password);
        //            //  client.UseDefaultCredentials = true;


        //            client.Send(message);

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
    public class MailSettings
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
