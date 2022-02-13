using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Helper
{
    public class MailLogic
    {
        public async void Send(User user)
        {
            //labwork15kafis@gmail.com
            MailAddress from = new MailAddress("science.magazine.dip@gmail.com", "Научный журнал");
            //MailAddress from = new MailAddress("travelagensy.ivansusanin@gmail.com");

            MailAddress to = new MailAddress(user.Email);
            MailMessage message = new MailMessage(from, to);
            //Тема сообщения
            message.Subject = "Регистрация в научном журнале";
            //Текст сообщения
            message.Body = "Уважаемый(ая) "+user.LastName+" "+user.Name+" "+user.Otch
                            +", благодарим за регистрацию в системе научного журнала.\n" +
                            "Ваш логин: "+user.Login+"\nВаш пароль: "+ user.Password;
            //Вложения
            //message.Attachments.Add(new Attachment(pas.GetPass()));
            //message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");//, 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("science.magazine.dip@gmail.com", "Shetinina2000!");
            await smtp.SendMailAsync(message);
        }
    }
}
