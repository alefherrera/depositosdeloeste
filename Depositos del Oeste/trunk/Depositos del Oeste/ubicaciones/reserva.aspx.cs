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
        protected void Page_PreRender(Object sender, EventArgs e)
        {
            panelArticulos();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ServiceProductos.cargarComboClientes(ddlClientes);
            }
        }

        private void panelArticulos()
        {
            if (hdCantidadArticulos.Value == "1")
                linkRemove.Visible = false;
            else
                linkRemove.Visible = true;
            
            for (int i = 0; i < int.Parse(hdCantidadArticulos.Value); i++)
            {
                Articulo articulo = new Articulo();
                articulo.IdCliente = int.Parse(ddlClientes.SelectedItem.Value);

                DropDownList ddlArticulo = new DropDownList();
                ddlArticulo.ID = "ddlArticulo_" + i.ToString();
                ServiceProductos.cargarComboArticulos(ddlArticulo, articulo);
                pnlArticulos.Controls.Add(ddlArticulo);
                
                TextBox txtCantidad = new TextBox();
                txtCantidad.ID="txtCantidad_" + i.ToString();
                pnlArticulos.Controls.Add(txtCantidad);

                Literal ltControl = new Literal();
                ltControl.Text = "<br/>";
                pnlArticulos.Controls.Add(ltControl);
            }
        }


        protected void linkRemove_Click(object sender, EventArgs e)
        {
            hdCantidadArticulos.Value = (int.Parse(hdCantidadArticulos.Value) - 1).ToString();
        }

        protected void linkAdd_Click(object sender, EventArgs e)
        {
            hdCantidadArticulos.Value = (int.Parse(hdCantidadArticulos.Value) + 1).ToString();
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(ddlClientes.SelectedItem.Value) != (int)Enums.Combos.Seleccione)
            {
                hdCantidadArticulos.Value = "1";
                pnlArticulosGeneral.Visible = true;
            }else
            {
                hdCantidadArticulos.Value = "0";
                pnlArticulosGeneral.Visible = false;
            }
        }
    }
}