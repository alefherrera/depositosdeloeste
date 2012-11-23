using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using Services;

namespace Depositos_del_Oeste
{
    public partial class _PedidosDetalles : PageBase
    {
        private int idPedido;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Validaciones.isNumeric(Request.QueryString["id"]))
            {
                lbError.Text = "Codigo de pedido erroneo";
                return;
            }
            idPedido = int.Parse(Request.QueryString["id"]);

            Pedido oPedido;
            Cliente oCliente;
            try{
                oPedido = ServicePedidos.cargarPedido(idPedido);
                oCliente = ServiceProductos.cargarCliente(oPedido.IdCliente.ToString());
            }
            catch(ErrorFormException ex)
            {
                lbError.Text = ex.Message;
                return;
            }

            lbCliente.Text = oCliente.Razon_Social;
            lbFechaPedido.Text = oPedido.FechaPedido.ToShortDateString();
            lbPedido.Text = oPedido.Id.ToString();
            lnkimprimir.HRef += idPedido;

            gridPedidoDetalles.DataSource = BusinessObject<Object>.SelectSQL("SELECT articulos_idarticulo, SUM(cantidad) as Cantidad, pedidos_id, articulos.nombre FROM depositosoeste.pedidos_detalle inner join articulos on articulos_idarticulo = idarticulo Where pedidos_id =" + idPedido + " group by articulos_idarticulo;");
            gridPedidoDetalles.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/retiro/pedidos.aspx");
        }
    }
}