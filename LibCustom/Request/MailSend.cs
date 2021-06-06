using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace LibCustom.Request
{
    public class MailSend
    {
        private MailAddress MailAddressFrom { get; }
        private List<MailMessage> MailMessages { get; set; }
        private SmtpClient SmtpClient { get; }
        public MailSend(string mailAddressFrom, string smtpServer, string password, int port)
        {
            MailMessages = new();
            mailAddressFrom = mailAddressFrom.Trim();
            smtpServer = smtpServer.Trim();
            MailAddressFrom = new(mailAddressFrom);
            SmtpClient = new();
            SmtpClient.EnableSsl = true;
            SmtpClient.UseDefaultCredentials = false;
            SmtpClient.Host = smtpServer;
            SmtpClient.Port = port;
            SmtpClient.Credentials = new NetworkCredential(mailAddressFrom.Split('@')[0], password);
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        }
        public Tuple<bool, string> AttachMail(string mailAdressTo, string caption, string message, string attachFile = null)
        {
            try
            {
                mailAdressTo = mailAdressTo.Trim();
                MailMessage mailMessage = new();
                mailMessage.From = MailAddressFrom;
                mailMessage.To.Add(new MailAddress(mailAdressTo));
                mailMessage.Subject = caption;
                mailMessage.Body = message;
                if (attachFile != null)
                    mailMessage.Attachments.Add(new Attachment(attachFile));
                MailMessages.Add(mailMessage);
                return new Tuple<bool, string>(true, null);
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false, "Mail.Attach: " + e.Message);
            }
        }

        public void SendMail()
        {
            foreach(var mailMessage in MailMessages)
            {
                SmtpClient.Send(mailMessage);
                mailMessage.Dispose();
            }
            MailMessages.Clear();
        }
    }
}
