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
                ServiceControles.cargarComboClientes(ddlClientes);
            }
            else
            {
                ServiceControles.cargarGridArticulos(gridArticulos, ddlClientes.SelectedItem.Value);
            }
        }
    }
}