using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;

namespace Services
{
    public class ServicePedidos : ServiceBase
    {
        public static void cargarGridPedido(GridView gridpedidos, string seleccion)
        {
            int sel = 0;
            if (Validaciones.isNumeric(seleccion))
                sel = int.Parse(seleccion);

            if (sel < 0)
            {
                return;
            }

            Pedido pedido = new Pedido();
            pedido.IdCliente = sel;

            gridpedidos.AutoGenerateColumns = false;
            gridpedidos.ShowHeader = true;
            List<Pedido> pedidos = pedido.Select();
            pedidos.Sort(
                delegate(Pedido p1, Pedido p2)
                {
                    return p2.Id.CompareTo(p1.Id);
                }
            );
            gridpedidos.DataSource = pedidos;
            gridpedidos.DataBind();
        }

        public static Pedido cargarPedido(int idpedido)
        {
            if (idpedido <= 0)
                throw new ErrorFormException("Codigo de pedido Incorrecto");
            Pedido opedido = new Pedido();
            opedido.Id = idpedido;
            opedido.Load();
            if (opedido.Loaded)
                return opedido;
            throw new ErrorFormException("pedido inexistente");
        }
    }
}
