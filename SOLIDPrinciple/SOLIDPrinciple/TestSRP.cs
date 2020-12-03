using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDPrinciple
{
    // class with multiple responsibilities
    /// <summary>
    /// The SendEmail and ValidateEmail methods have nothing to do within the UserService class.
    /// </summary>
    /// 

    /*Every software module should have only one reason to change*/
    public class UserService
    {
        public void Register(string email, string password)
        {
            if (!ValidateEmail(email))
                throw new Exception("Email is not an email");
            var user = new User(email, password);
            user.save();
            SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject = "HEllo foo" });
        }
        public virtual bool ValidateEmail(string email)
        {
            return email.Contains("@");
        }
        public bool SendEmail(MailMessage message)
        {
            return true;
        }
    }

    /// <summary>
    /// After refract and applying SRP
    /// </summary>
    public class UserServiceWithSRP
    {
        EmailService _emailService;
        
        public UserServiceWithSRP(EmailService aEmailService)
        {
            _emailService = aEmailService;
        }
        public void Register(string email, string password)
        {
            if (!_emailService.ValidateEmailSRP(email))
                throw new Exception("Email is not an email");
            var user = new User(email, password);
            user.save();
            _emailService.SendEmail(new MailMessage("myname@mydomain.com", email) { Subject = "Hi. How are you!" });

        }
    }
    public class EmailService
    {
        SmtpClient _smtpClient;
        public EmailService(SmtpClient aSmtpClient)
        {
            _smtpClient = aSmtpClient;
        }
        public bool ValidateEmailSRP(string email)
        {
            return email.Contains("@");
        }
        public bool SendEmail(MailMessage message)
        {
            return true;
        }
    }

    internal class User
    {
        private string email;
        private string password;

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
        public void save()
        {
            Console.WriteLine("User Saved !!!");
        }
    }
}
