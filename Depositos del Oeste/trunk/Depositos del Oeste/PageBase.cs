using BackEnd;
using System;
using System.Text;
using System.Web;

namespace Depositos_del_Oeste
{
    public class PageBase : System.Web.UI.Page
    {
        protected Usuario user = new Usuario();
        
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
        }
        
        protected override void OnPreLoad(System.EventArgs e)
        {
            base.OnPreLoad(e);
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            user = (Usuario)Session["Usuario"];

            string context = Request.AppRelativeCurrentExecutionFilePath.Replace("~", "").ToLower();

            //Cargo Page Title
            Page.Title = Services.ServiceMenu.cargarNombre(context);


            //Verifico Permisos
            if (context == "/default.aspx")
            {
                return;
            }
            if (!Services.ServicePermisos.VerificarPermisos(user, context))
            {
                Response.Redirect("/Default.aspx");
            }
        }

        protected override void OnError(System.EventArgs e)
        {
            base.OnError(e);
            Exception ex = Server.GetLastError();
            if (ex is RedireccionDatosException)
            {
                string postbackUrl;

                Response.Clear();

                string name = "errorRedireccion";
                if (Request.UrlReferrer == null)
                {
                    postbackUrl = "/Default.aspx";
                }
                else
                {
                    postbackUrl = Request.UrlReferrer.AbsoluteUri;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                sb.AppendFormat("<form name='form' action='{0}' method='post'>", postbackUrl);
                sb.AppendFormat("<input type='hidden' name='{1}' value='{0}'>", ex.Message, name);
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