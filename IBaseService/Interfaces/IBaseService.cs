using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Project.Interfaces
{
    public class IBaseService
    {
        private string domain = "ProjectDomain"
        internal string user;

        public IBaseService()
        {
            user = GetUser();
        }

        internal string GetUser()
        {
            string name = this.GetUserName();
            if (!String.IsNullOrWhiteSpace(name))
            {
                DirectoryEntry De = new DirectoryEntry("WinNT://" + domain "/" + name);
                return  De.Properties["FullName"].Value.ToString();
            }
            return "";
        }

        internal bool IsInSecurityGroup(string group)
        {
            user = GetUserName();
            try
            {
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(new System.Security.Principal.WindowsIdentity(user));
                return principal.IsInRole(group);
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal string GetUserEmail()
        {
            try
            {
                var searcher = new DirectorySearcher("LDAP://" + GetUserName())
                {
                    Filter = "(&(ObjectClass=person)(sAMAccountName=" + GetUserName() + "))"
                };

                return searcher.FindOne().Properties["mail"][0].ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        private string GetUserName()
        {
            return HttpContext.Current.User.Identity.Name.Remove(0, 9);
        }


        internal bool SendEmail(string[] toAddresses, string fromAddress, string[] ccAddresses, string subject, string body, IEnumerable<HttpPostedFileBase> attachments)
        {

            MailMessage message;
            try
            {
                // Format email
                message = new MailMessage();
                {
                    foreach (var toAddress in toAddresses)
                    {
                        message.To.Add(new MailAddress(toAddress));
                    }

                    if (ccAddresses.First() != "")
                    {
                        foreach (var ccAddress in ccAddresses)
                        {
                            message.CC.Add(new MailAddress(ccAddress));
                        }
                    }

                    message.From = new MailAddress(fromAddress);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    if (attachments.First() != null)
                    {
                        foreach (HttpPostedFileBase attachment in attachments)
                        {
                            Attachment emailAttachment = new Attachment(attachment.InputStream, attachment.FileName);
                            message.Attachments.Add(emailAttachment);
                        }
                    }
                }

                // Open email client and send email
                string server = "ProjectServer"
                int port = 0
                SmtpClient SMTP = new SmtpClient(server, port);
                SMTP.UseDefaultCredentials = true;
                SMTP.Send(message);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}