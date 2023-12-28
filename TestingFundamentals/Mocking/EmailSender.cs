using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace TestingFundamentals.Mocking;

public interface IEmailSender
{
    void EmailFile(string emailAddress, string emailBody, string fileName, string subject);
}

public class EmailSender : IEmailSender
{
    public void EmailFile(string emailAddress, string emailBody, string fileName, string subject)
    {
        var client = new SmtpClient(SystemSettingsHelper.EmailSmtpHost)
        {
            Port = SystemSettingsHelper.EmailPort,
            Credentials = new NetworkCredential(
                SystemSettingsHelper.EmailUserName,
                SystemSettingsHelper.EmailPassword
                )
        };

        var from = new MailAddress(SystemSettingsHelper.EmailFromEmail);

        var to = new MailAddress(emailAddress);

        var message = new MailMessage(from, to)
        {
            Subject = subject,
            SubjectEncoding = Encoding.UTF8,
            Body = emailBody,
            BodyEncoding = Encoding.UTF8
        };

        message.Attachments.Add(new Attachment(fileName));
        client.Send(message);
        message.Dispose();

        File.Delete(fileName);
    }
}


public class SystemSettingsHelper
{
    public static string? EmailSmtpHost { get; internal set; }
    public static int EmailPort { get; internal set; }
    public static string? EmailUserName { get; internal set; }
    public static SecureString? EmailPassword { get; internal set; }
    public static string EmailFromEmail { get; internal set; }
}
