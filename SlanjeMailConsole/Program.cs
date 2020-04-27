using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SlanjeMailConsole
{
    class Program
    {
        static void Main(string[] args)
        {


                ////SmtpClient smtpServer = new SmtpClient("mail2.hanfa.hr");
                //SmtpClient smtpServer = new SmtpClient("xamaural.hanfa.local");

                //smtpServer.Port = 25;
                //smtpServer.EnableSsl = true;
                ////smtpServer.UseDefaultCredentials = true;
                //smtpServer.Credentials = new System.Net.NetworkCredential("iborota", "Snikra1234");

                //System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                //mail.From = new MailAddress("igor.borota@hanfa.hr");
                ////mail.To.Add("igor.borota@hanfa.hr");
                //mail.To.Add("borota.igor@gmail.com");

                //mail.Subject = "Generirani PDF dokument";
                //mail.Body = "Generirani PDF dokument";

                //smtpServer.Send(mail);


                 Console.ReadKey();
        }
    }
}
