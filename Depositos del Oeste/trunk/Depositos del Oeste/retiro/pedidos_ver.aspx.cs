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
    public partial class _PedidosVer : PageBase
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
            ltTitle.Text = "Pedido: " + oPedido.Id.ToString();
            lbCliente.Text = oCliente.Razon_Social;
            lbFechaPedido.Text = oPedido.FechaPedido.ToShortDateString();
            lbPedido.Text = oPedido.Id.ToString();


            PedidosDetalle oPedidosDetalle = new PedidosDetalle();
            oPedidosDetalle.IdPedido = oPedido.Id;
            gridPedidoDetalles.DataSource = oPedidosDetalle.Select();
            gridPedidoDetalles.DataBind();
        }
    }
}