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
        private Usuario user = new Usuario();

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
            //Verifico si el usuario esta logueado y lo logueo
            if (Session["Usuario"] != null)
            {
                this.user = (Usuario)Session["Usuario"];

                login.Visible = false;
                logcorrecto.Visible = true;

                legajo.Text = this.user.Legajo.ToString();
                nombre.Text = this.user.Apellido + " " + this.user.Nombre;
            }
            else if (Request.Cookies["usuarioDepositos"].Value != null && Validaciones.isNumeric(Request.Cookies["usuarioDepositos"].Value))
            {
                this.user.Legajo = int.Parse(Request.Cookies["usuarioDepositos"].Value);
                this.user.Load();

                login.Visible = false;
                logcorrecto.Visible = true;

                legajo.Text = this.user.Legajo.ToString();
                nombre.Text = this.user.Apellido + " " + this.user.Nombre;
            }

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

            cssmenu.InnerHtml = stringMenu.ToString();
        }

        protected void login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Usuario usuario = new Usuario();
            if (!Validaciones.isNumeric(login.UserName))
            {
                login.FailureText = "Error, el numero de legajo no tiene el formato correcto";
                return;
            }
           
            usuario.Legajo = int.Parse(login.UserName);
            usuario.Dni = login.Password;

            List<Usuario> usuarios = usuario.Select();

            if (usuarios.Count != 1)
            {
                login.FailureText = "Error, el numero de legajo o la contraseña es incorrecto/a";
                return;
            }

            login.Visible = false;
            logcorrecto.Visible = true;

            usuario.Load();

            legajo.Text = usuario.Legajo.ToString();
            nombre.Text = usuario.Apellido + " " + usuario.Nombre;


            Session.Add("Usuario", usuario);

            if (login.RememberMeSet)
            {
                Response.Cookies["usuarioDepositos"].Value = usuario.Legajo.ToString();
                Response.Cookies["usuarioDepositos"].Expires = DateTime.Now.AddDays(7);
            }
        }
    }
}