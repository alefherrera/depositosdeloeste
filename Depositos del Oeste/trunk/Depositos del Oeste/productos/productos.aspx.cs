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
    public partial class _Productos : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ServiceProductos.cargarComboClientes(ddlClientes);
                if (Request.QueryString["id"] != null)
                {
                    string id = Request.QueryString["id"];
                    ddlClientes.SelectedValue = id;
                }
            }
            ServiceProductos.cargarGridArticulos(gridArticulos, ddlClientes.SelectedItem.Value);
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            if (ddl.SelectedValue != "-1")
            {
                location.HRef = "productos_alta.aspx?id=" + ddl.SelectedValue; 
            }
        }
    }
}