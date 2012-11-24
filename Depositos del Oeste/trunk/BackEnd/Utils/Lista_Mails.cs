using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace BackEnd
{
    public class Lista_Mails
    {
        public static void Codigo_Reserva(string codigo, string mail_cliente)
        {
            string asunto = "Su Reserva (Depósitos del Oeste)";
            string cuerpo;

            StringBuilder sb = new StringBuilder();
            sb.Append("Gracias por hacer su reserva con nosotros.<br/>");
            sb.Append("Recuerde que el siguiente código deberá ser presentado al momento de hacer su depósito.<br/>");
            sb.Append("Su código es: " + codigo + "<br/>");
            sb.Append("<br/>Atentamente Leandro Ferreyro, gerente encargado.");

            cuerpo = sb.ToString();

            Mail oMail = new Mail();
            oMail.Asunto = asunto;
            oMail.Cuerpo = cuerpo;
            oMail.Para = mail_cliente;
            oMail.Copia_Oculta = "nacho692@gmail.com";
            oMail.enviar();
        }

        public static void Facturacion(List<Compartimiento> compartimientos, string cliente)
        {
            if (compartimientos.Count == 0)
                return;

            string asunto = "Facturacion ubicaciones";
            string cuerpo;

            StringBuilder sb = new StringBuilder();
            sb.Append("Los siguientes compartimientos fueron reservados por " + cliente + "<br/>");
            foreach (Compartimiento cmp in compartimientos)
            {
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td>Estanteria:</td><td>" + cmp.NroEstanteria);
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>Nivel:</td><td>" + cmp.Nivel);
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>Compartimiento:</td><td>" + cmp.NroCompartimiento);
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>Fecha de Reserva:</td><td>" + cmp.FechaReserva.ToShortDateString());
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>Fecha de Fin de Reserva:</td><td>" + DateTime.Today.ToShortDateString());
                sb.Append("</tr>");
                sb.Append("</table><br/>");
            }

            cuerpo = sb.ToString();

            Mail oMail = new Mail();
            oMail.Asunto = asunto;
            oMail.Cuerpo = cuerpo;
            oMail.Para = "alef.herrera@gmail.com";
            oMail.Copia_Oculta = "nacho692@gmail.com";
            oMail.enviar();
        }
     
     
    }
}
