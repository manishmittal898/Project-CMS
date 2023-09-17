using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
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

        public EmailHelper(IOptions<MailSettings> mailSettings)
        {

            // _env = environment;
            _mailSettings = mailSettings.Value;

        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {

                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(mailRequest.ToEmail, mailRequest.ToEmail);
                emailMessage.To.Add(emailTo);
                emailMessage.Subject = mailRequest.Subject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = mailRequest.Body;
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
                            emailBodyBuilder.Attachments.Add(attachmentFile.FileName, attachmentFileByteArray, MimeKit.ContentType.Parse(attachmentFile.ContentType));
                        }
                    }
                }

                MailKit.Net.Smtp.SmtpClient emailClient = new MailKit.Net.Smtp.SmtpClient();

                emailClient.Connect(_mailSettings.Host, _mailSettings.Port, true);
                emailClient.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                var str = await emailClient.SendAsync(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
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
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
