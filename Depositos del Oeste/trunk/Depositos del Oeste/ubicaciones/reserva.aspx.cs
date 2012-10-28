using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using Services;
using BackEnd.Utils;

namespace Depositos_del_Oeste
{
    public partial class _Reserva : PageBase
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ServiceProductos.cargarComboClientes(ddlClientes);
                
            }
            if (int.Parse(ddlClientes.SelectedItem.Value) == (int)Enums.Combos.Seleccione)
                pnlArticulosGeneral.Visible = false;
            else
            {
                Articulo articulo = new Articulo();
                articulo.IdCliente = int.Parse(ddlClientes.SelectedItem.Value);
                ServiceProductos.cargarComboArticulos(ddlArticulo, articulo);
                pnlArticulosGeneral.Visible = true;
            }
        }

        protected void linkAdd_Click(object sender, EventArgs e)
        {
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}