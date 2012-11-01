using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using System.Data;
using System.Collections;
using BackEnd.Utils;

namespace Services
{
    public class ServiceReservas : ServiceBase
    {

        public static List<ReservaDetalle> cargarReservaDetalle(string codigo)
        {
            if (codigo == null || codigo.Length < (int)Enums.Codigo_Ubicacion.longitud)
                throw new ErrorFormException("Codigo incorrecto");

            ReservaDetalle oReserva = new ReservaDetalle();
            oReserva.CodigoReserva = codigo;
            List<ReservaDetalle> Detalles = oReserva.Select();

            return Detalles;
        }

        public static Reserva cargarReserva(string codigo)
        {
            if(codigo == null || codigo.Length < (int)Enums.Codigo_Ubicacion.longitud)
                throw new ErrorFormException("Codigo incorrecto");
            
            Reserva oReserva = new Reserva();
            oReserva.Codigo = codigo;
            oReserva.Load();
            if (!oReserva.Loaded)
                throw new ErrorFormException("Reserva no encontrada");

            return oReserva;
        }
        public static string generarCodigo()
        {
            Random rnd = new Random();
            string charPool
            = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder rs = new StringBuilder();

            for (int i = 0; i < (int)Enums.Codigo_Ubicacion.longitud; i++)
                rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);

            Reserva oReserva = new Reserva();
            oReserva.Codigo = rs.ToString();
            oReserva.Load();
            if (oReserva.Loaded)
                return generarCodigo();

            return rs.ToString();
        }
    }
}
