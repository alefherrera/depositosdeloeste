using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

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
            List<Menu> menuList = new List<Menu>();
            Menu menu = new Menu();
            menu.Id = 0;
            menu.IdPadre = 0;
            menu.Nombre = "Home";
            menu.Link = "/Default.aspx";

            menuList.Add(menu);

            menu = new Menu();
            menu.Id = 1;
            menu.IdPadre = 0;
            menu.Nombre = "MENU1";
            menu.Link = "#";

            menuList.Add(menu);

            menu = new Menu();
            menu.Id = 2;
            menu.IdPadre = 1;
            menu.Nombre = "SubMenu1";
            menu.Link = "/About.aspx";

            StringBuilder stringMenu = new StringBuilder();  
            stringMenu.Append("<ul>");

            List<Menu> menuPrincipal = new List<Menu>();
            menuPrincipal = menuList.FindAll(
                delegate(Menu mn)
                {
                    return mn.IdPadre == 0;
                }
            );

            for(int i = 0; menuPrincipal.Count > i; i++)
            {
                stringMenu.Append("<li><a href='");
                stringMenu.Append(menuPrincipal[i].Link);
                stringMenu.Append("'><span>");
                stringMenu.Append(menuPrincipal[i].Nombre);
                stringMenu.Append("</span></a></li>");
            }

            stringMenu.Append("</ul>");

            
            //cssmenu.InnerHtml = stringMenu.ToString();
        }
    }


    public class Menu
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int idPadre;
        public int IdPadre
        {
            get { return idPadre; }
            set { idPadre = value; }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string link;
        public string Link
        {
            get { return link; }
            set { link = value; }
        }


    }
}