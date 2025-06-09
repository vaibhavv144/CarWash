 
using System.Net;
using System.Net.Mail;

 
 

    public class SMTP : ISMTP
    {
        private readonly IConfiguration _config;
 
        public SMTP(IConfiguration config)
        {
            _config = config;
        }
 
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                // Create email message
 
                var message = new MailMessage();
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.From = new MailAddress(_config["SMTP:Username"]);
 
                using var client = new SmtpClient(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]))
                {
                    EnableSsl = bool.Parse(_config["SMTP:EnableSsl"]),
 
                    Credentials = new NetworkCredential(_config["SMTP:Username"], _config["SMTP:Password"])
                };
 
                await client.SendMailAsync(message);
 
               
 
                // Log success
                Console.WriteLine($"[x] Email successfully sent to {to}");
            }
            catch (Exception ex)
            {
                // Log failure
                Console.WriteLine($"[!] Email sending failed: {ex.Message}");
            }
        }
 
    }

 
 
 

 