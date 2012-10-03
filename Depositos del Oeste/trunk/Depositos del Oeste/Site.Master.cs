using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using BackEnd;

namespace Depositos_del_Oeste
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Traigo todos los menues
            BackEnd.Menu menu = new BackEnd.Menu();
            List<BackEnd.Menu> menuList = menu.Select();

            StringBuilder stringMenu = new StringBuilder();
            new List<BackEnd.Menu>();

            //Extraigo los padres
            List<BackEnd.Menu> menuPrincipal = new List<BackEnd.Menu>();
            menuPrincipal = menuList.FindAll(
                delegate(BackEnd.Menu mn)
                {
                    return mn.IdPadre == 0;
                }
            );

            //Empieza aca, se puede hacer recursivo pero son solo dos niveles estaticos, no hace falta
            stringMenu.Append("<ul>");
            for(int i = 0; menuPrincipal.Count > i; i++)
            {
                //Agarro todos los que son hijos del padre que voy iterando
                List<BackEnd.Menu> menuSecundario = new List<BackEnd.Menu>();
                menuSecundario = new List<BackEnd.Menu>();
                menuSecundario = menuList.FindAll(
                    delegate(BackEnd.Menu mn)
                    {
                        return mn.IdPadre == menuPrincipal[i].Id;
                    }
                );


                stringMenu.Append("<li class='has-sub '><a href='");
                stringMenu.Append(menuPrincipal[i].Link);
                stringMenu.Append("'><span>");
                stringMenu.Append(menuPrincipal[i].Nombre);
                stringMenu.Append("</span></a>");

                if (menuSecundario.Count > 0)
                {
                    stringMenu.Append("<ul>");
                    for (int j = 0; menuSecundario.Count > j; j++)
                    {
                        stringMenu.Append("<li class='has-sub'><a href='");
                        stringMenu.Append(menuSecundario[j].Link);
                        stringMenu.Append("'><span>");
                        stringMenu.Append(menuSecundario[j].Nombre);
                        stringMenu.Append("</span></a>");
                        stringMenu.Append("</li>");
                    }
                    stringMenu.Append("</ul>");
                }

          
                stringMenu.Append("</li>");
            }

            stringMenu.Append("</ul>");



            //menu.TestMethod();
            cssmenu.InnerHtml = stringMenu.ToString();
            Usuario usuario = new Usuario();
            //usuario.Legajo = 12;
            //suario.Save();
            usuario.Legajo = 16600;
            List<Usuario> usuarios = usuario.Select();//usuario.Select("from Usuario where legajo = 16600");
            usuarios.Count();
        }


    }


}