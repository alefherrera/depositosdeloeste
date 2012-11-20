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
    public partial class _Pedidos : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ServiceProductos.cargarComboClientes(ddlClientes);
            ServicePedidos.cargarGridPedido(gridPedidos, ddlClientes.SelectedItem.Value);
        }
    }
}