using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using Services;
using BackEnd.Utils;
using System.Data;

namespace Depositos_del_Oeste
{
    public partial class _Reserva : PageBase
    {
        public DataTable dt { 
            get { return (DataTable)ViewState["dt"];}
            set { ViewState["dt"] = value; } 
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ServiceProductos.cargarComboClientes(ddlClientes);
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("index"));
                dt.Columns.Add(new DataColumn("id"));
                dt.Columns.Add(new DataColumn("desc"));
                dt.Columns.Add(new DataColumn("cant"));
                
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
            DataRow dr = dt.NewRow();
            dr["index"] = dt.Rows.Count;
            dr["id"] = ddlArticulo.SelectedValue;
            dr["desc"] = ddlArticulo.Text;
            dr["cant"] = txtCantidad.Text;
            dt.Rows.Add(dr);
            gridArticulos.DataSource = dt;
            gridArticulos.DataBind();

        }

        protected void Eliminar(object sender, EventArgs e)
        {
            dt.Rows.RemoveAt(int.Parse(((Button)sender).CommandArgument));
            RegenerarIndices();
            gridArticulos.DataSource = dt;
            gridArticulos.DataBind();
        }

        protected void RegenerarIndices()
        {
            foreach (DataRow a in dt.Rows)
            {
                a["index"] = dt.Rows.IndexOf(a);
            }
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

    }
}