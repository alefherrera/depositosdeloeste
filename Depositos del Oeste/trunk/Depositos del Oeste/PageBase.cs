using BackEnd;
using System.Web;

namespace Depositos_del_Oeste
{
    public class PageBase : System.Web.UI.Page
    {
        protected Usuario user = new Usuario();
        
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            user = (Usuario)Session["Usuario"];
        }
        
        protected override void OnPreLoad(System.EventArgs e)
        {
            base.OnPreLoad(e);
        
            //esta hardcodeado porque me pedia un numero, vos hacete la funcionciata de verificar ;)
            
            string context = Request.AppRelativeCurrentExecutionFilePath.Replace("~", "").ToLower();
            if (context == "/default.aspx")
            {
                return;
            }
            if (!Services.ServicePermisos.VerificarPermisos(user, context))
            {
                Response.Redirect("/Default.aspx");
            }
        }
    }
}