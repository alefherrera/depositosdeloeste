using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using Services;
using System.Text;

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
            catch (CargarDatosException ex)
            {
                string postbackUrl;

                Response.Clear();

                if (Request.UrlReferrer == null)
                {
                    postbackUrl = "/Default.aspx";
                }
                else
                {
                    postbackUrl = Request.UrlReferrer.AbsoluteUri;
                }
                //TODO: Si podemos hacer que el page base automaticamente lea el query string para errores y los imprima vendria genial.


                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                sb.AppendFormat("<form name='form' action='{0}' method='post'>", postbackUrl);
                sb.AppendFormat("<input type='hidden' name='error' value='{0}'>", ex.Message);
                // Other params go here
                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");

                Response.Write(sb.ToString());
                Response.End();
                return;
            }
        }
    }
}