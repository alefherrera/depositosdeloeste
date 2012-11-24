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
    public class ServiceRetiro : ServiceBase
    {
        public static void registrarRetiro(List<Compartimiento> compartimientos, int cliente, DateTime fechapedido)
        {
            Pedido pedido = new Pedido();
            pedido.IdCliente = cliente;
            pedido.FechaPedido = fechapedido;
            pedido.Save();

            foreach (Compartimiento cmp in compartimientos)
            {
                PedidosDetalle pedidosDetalle = new PedidosDetalle();
                pedidosDetalle.IdPedido = pedido.Id;
                pedidosDetalle.IdArticulo = cmp.IdArticulo;
                pedidosDetalle.IdCompartimiento = cmp.Id;
                pedidosDetalle.Cantidad = cmp.Cantidad_Guardar;

                if (cmp.Estado == (int)Enums.Ubicaciones_Estado.Libre)
                {
                    cmp.IdArticulo = 0;
                    cmp.FechaRetiroProbable = DateTime.Parse("1900-01-01");
                    cmp.FechaReserva = DateTime.Parse("1900-01-01");

                }
                cmp.Update();

                pedidosDetalle.Save();
            }
        }
    }
}
