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
    public class ServiceIngreso : ServiceBase
    {
        public static void registrarIngreso(List<Compartimiento> ingresados, DateTime fechaRemito, string descripcion, int cliente, string codigo)
        {
            Remito oRemito = new Remito();
            oRemito.FechaRemito = fechaRemito;
            oRemito.Descripcion = descripcion;
            oRemito.IdCliente = cliente;
            oRemito.Save();
            
            foreach (Compartimiento ingreso in ingresados)
            {
                if (ingreso.Cantidad_Guardar > 0)
                {
                    RemitosDetalle oRemitoDetalle = new RemitosDetalle();
                    oRemitoDetalle.IdRemito = oRemito.Id;
                    oRemitoDetalle.IdArticulo = ingreso.IdArticulo;
                    oRemitoDetalle.Cantidad = ingreso.Cantidad_Guardar;
                    oRemitoDetalle.IdCompartimiento = ingreso.Id;
                    oRemitoDetalle.Save();
                }

                ingreso.Update();
            }

            Reserva oReserva = new Reserva();
            oReserva.Codigo = codigo;
            oReserva.Load();
            oReserva.BajaLogica();

            //Limpio los compartimientos reservados que sobran
            Compartimiento cmp = new Compartimiento();
            
            return;
        }
    }
}
