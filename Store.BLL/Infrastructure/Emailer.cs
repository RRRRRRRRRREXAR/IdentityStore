using Store.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Store.BLL.Infrastructure
{
    public class Emailer
    {
        public void SentOrder(UserDTO user,OrderDTO order)
        {
            MailAddress from = new MailAddress("eventmanagerkbip@gmail.com", "Tom");
            MailAddress to = new MailAddress(user.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Подтверждение почты";
            m.Body = string.Format(@"{0},{1},{2},{3}", user.FirstName,user.LastName,order.Products,order.Id);
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["mailAccount"], ConfigurationManager.AppSettings["mailPassword"]);
            smtp.EnableSsl = true;
            

            Thread T1 = new Thread(delegate ()
            {
                smtp.Send(m);
            } 
            );

            T1.Start();
        }
    }
}
