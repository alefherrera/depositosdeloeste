using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace BackEnd
{
    class Mail
    {
        public bool enviar(string mensaje)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add("ely_ff_64_@hotmail.com");
            message.Subject = "HOLA";
            message.From = new System.Net.Mail.MailAddress("depositosdeloeste@gmail.com");
            message.Body = mensaje;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
            smtp.Send(message);

            return true;
        }
    }
}
