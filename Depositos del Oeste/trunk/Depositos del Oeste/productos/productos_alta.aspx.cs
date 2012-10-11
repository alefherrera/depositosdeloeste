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
    public partial class _Productos_Alta : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarCliente();
        }
          

        private void cargarCliente()
        {
            try
            {
                lbCliente.Text = ServiceControles.cargarCliente(Request.QueryString["id"]).ToString();
            }
            catch (CargarDatosException)
            {
                if (Request.UrlReferrer == null)
                {
                    Response.Redirect("/Default.aspx");
                    return;
                }
                //TODO: Si podemos hacer que el page base automaticamente lea el query string para errores y los imprima vendria genial.
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                return;
            }
        }
    }
}