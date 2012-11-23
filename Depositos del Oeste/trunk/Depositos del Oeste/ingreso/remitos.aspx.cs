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
    public partial class _Remitos : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbError.Text = "";            
            if (!IsPostBack)
                ServiceProductos.cargarComboClientes(ddlClientes);
            ServiceRemitos.cargarGridRemitos(gridRemitos, ddlClientes.SelectedItem.Value);
            if (gridRemitos.Rows.Count == 0 && ddlClientes.SelectedValue != "-1")
                lbError.Text = "El cliente no tiene remitos";

        }
    }
}