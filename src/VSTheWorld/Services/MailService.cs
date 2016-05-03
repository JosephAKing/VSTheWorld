//using Limilabs.Mail;
//using Limilabs.Mail.Headers;
//using Limilabs.Client.SMTP;

namespace VSTheWorld.Services
{
    class MailService : IMailService
    {
        public bool SendMail(string to, string from, string subject, string body)
        {
            return false;
           // bool success = false;

           //MailBuilder builder = new MailBuilder();
           //builder.From.Add(new MailBox(from, from));
           //builder.To.Add(new MailBox(to, to));
           //builder.Subject = subject;
           //builder.Text = body;

           // IMail email = builder.Create();
           

           // using (Smtp smtp = new Smtp())
           // {
           //     smtp.Connect(Startup.Configuration["SMTPSettings:ServerName"]);    // or ConnectSSL for SSL
           //     smtp.UseBestLogin(Startup.Configuration["SMTPSettings:UserName"], Startup.Configuration["SMTPSettings:Password"]); // remove if authentication is not needed

           //     ISendMessageResult result = smtp.SendMessage(email);
           //     if (result.Status == SendMessageStatus.Success)
           //     {
           //         success = true;
           //     }

           //     smtp.Close();
           // }

           // return success;
 
        }
    }
}
