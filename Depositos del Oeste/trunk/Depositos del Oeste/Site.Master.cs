﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using BackEnd;
using Services;

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
            LogHandler();
        }


        protected void login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            //Compruebo si los datos son correctos
            try
            {
                this.user = ServiceLogin.loguear(login.UserName, login.Password);
            }
            catch (LoginException ex)
            {
                errorLogin.Text= ex.Message;
                return;
            }

            Session.Add("Usuario", user);
            Logueado(true);

            if (login.RememberMeSet)
            {
                Response.Cookies["usuarioDepositos"].Value = this.user.Legajo.ToString();
                Response.Cookies["usuarioDepositos"].Expires = DateTime.Now.AddDays(7);
            }
            else
            {
                Response.Cookies["usuarioDepositos"].Value = "";
            }
        }

        private void LogHandler()
        {
            //Verifico si el usuario esta logueado y lo logueo
            if (Session["Usuario"] != null)
            {
                this.user = (Usuario)Session["Usuario"];
                Logueado(true);
            }
            else if (Request.Cookies["usuarioDepositos"] != null && Validaciones.isNumeric(Request.Cookies["usuarioDepositos"].Value) && Request.Cookies["usuarioDepositos"].Value != "")
            {
                this.user.Legajo = int.Parse(Request.Cookies["usuarioDepositos"].Value);
                this.user.Load();
                Session["Usuario"] = this.user;
                Logueado(true);
            }
        }

        private void Logueado(bool log)
        {
            if (log)
            {
                login.Visible = false;
                errorLogin.Text = "";
                logcorrecto.Visible = true;

                legajo.Text = this.user.Legajo.ToString();
                nombre.Text = this.user.Apellido + " " + this.user.Nombre;
            }
            else
            {
                login.Visible = true;
                logcorrecto.Visible = false;
                Session["Usuario"] = null;
                Response.Cookies["usuarioDepositos"].Value = "";
                user = new Usuario();
            }
        }

        protected void desloguear_Click(object sender, EventArgs e)
        {
            Logueado(false);
        }

        protected void MainContent_PreRender(object sender, EventArgs e)
        {
            cssmenu.InnerHtml = ServiceMenu.generarMenu(this.user);
            if (Request.Form["errorRedireccion"] != null)
            {
                errorLogin.Text = Request.Form["errorRedireccion"].ToString();
            }
        }
    }
}