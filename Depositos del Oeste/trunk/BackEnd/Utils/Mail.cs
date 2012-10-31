using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace BackEnd
{
    public class Mail
    {
        public string Para { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public string Copia_Oculta { get; set; }
        public string Nombre_Para { get; set; }

        public bool enviar()
        {
            var fromAddress = new MailAddress("depositosdeloeste@gmail.com", "Depositos del Oeste");
            var toAddress = new MailAddress(Para, Nombre_Para);
            const string fromPassword = "depositos";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            var message = new MailMessage(fromAddress, toAddress);
            message.Subject = Asunto;
            message.Body = Cuerpo;
            message.IsBodyHtml = true;
            
            message.CC.Add(Copia_Oculta);
            smtp.Send(message);
            return true;
        }
    }
}
