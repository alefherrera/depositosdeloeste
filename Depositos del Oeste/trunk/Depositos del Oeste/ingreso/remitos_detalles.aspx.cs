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
    public partial class _RemitosDetalles : PageBase
    {
        private int idRemito;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Validaciones.isNumeric(Request.QueryString["id"]))
            {
                lbError.Text = "Codigo de pedido erroneo";
                return;
            }
            idRemito = int.Parse(Request.QueryString["id"]);

            Remito oRemito;
            Cliente oCliente;
            try{
                oRemito = ServiceRemitos.cargarRemito(idRemito);
                oCliente = ServiceProductos.cargarCliente(oRemito.IdCliente.ToString());
            }
            catch(ErrorFormException ex)
            {
                lbError.Text = ex.Message;
                return;
            }

            lbCliente.Text = oCliente.Razon_Social;
            lbFechaPedido.Text = oRemito.FechaRemito.ToShortDateString();
            lbPedido.Text = oRemito.Id.ToString();
            lnkimprimir.HRef += idRemito;

            gridPedidoDetalles.DataSource = BusinessObject<Object>.SelectSQL("SELECT articulos_idarticulo, SUM(cantidad) as Cantidad, remitos_id, articulos.nombre FROM depositosoeste.remitos_detalle inner join articulos on articulos_idarticulo = idarticulo Where remitos_id=" + idRemito + " group by articulos_idarticulo;");
            gridPedidoDetalles.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ingreso/remitos.aspx");
        }
    }
}