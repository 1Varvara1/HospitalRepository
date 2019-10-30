using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Net;
using System.Net.Mail;



namespace HospitalBLL.Infrastructure
{
    // The class is dedicated to provide work with messages sanding
    public class MassageSender
    {
        private static ILog Log = LogManager.GetLogger(typeof(MassageSender));
        private string FromEmail;
        private string ToEmail;
        private string FromEmailPassword;
        private string Title;
        private string Body;
        private string Host;
        private int Port;
        private string Attachment;


        public MassageSender(string fromEmail, string toEmail, string fromEmailPassword,
            string title, string body, string host, int port, string attachment = null)
        {
            FromEmail = fromEmail;
            ToEmail = toEmail;
            FromEmailPassword = fromEmailPassword;
            Title = title;
            Body = body;
            Host = host;
            Port = port;
            Attachment = attachment;
        }

        public async Task<OperationDetails> SendMail()
        {
            using (MailMessage message = new MailMessage(FromEmail, ToEmail))
            {
                message.Subject = Title;
                message.Body = Body;
                message.IsBodyHtml = true;

                // In case the message should consist an attachment
                if (Attachment != null)
                {
                    message.Attachments.Add(new Attachment(Attachment));
                }

                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = true,
                    Host = Host,
                    Port = Port,
                    Credentials = new NetworkCredential(FromEmail, FromEmailPassword)
                })
                {
                    try
                    {
                        await client.SendMailAsync(message);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        return new OperationDetails(false, "Ошибка отправления email", "");
                    }
                }
            }

            return new OperationDetails(true, "Email отправлен", "");
        }

    }
}
