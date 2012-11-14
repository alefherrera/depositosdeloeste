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
    public partial class _RemitosVer : PageBase
    {
        private int idRemito;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Validaciones.isNumeric(Request.QueryString["id"]))
            {
                lbError.Text = "Codigo de remito erroneo";
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
            ltTitle.Text = "Remito: " + oRemito.Id.ToString();
            lbCliente.Text = oCliente.Razon_Social;
            lbFechaRemito.Text = oRemito.FechaRemito.ToShortDateString();
            lbRemito.Text = oRemito.Id.ToString();
        }
    }
}