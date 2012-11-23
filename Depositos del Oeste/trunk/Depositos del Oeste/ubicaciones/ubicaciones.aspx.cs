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
    public partial class _Ubicaciones : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbError.Text = "";
            if (!IsPostBack)
            {
                ServiceUbicaciones.cargarComboEstanteria(ddlEstanteria);
                ServiceProductos.cargarComboArticulos(ddlArticulo, new Articulo() { Activo = true });
            }
        }


        protected void btnBuscarEstanteria_Click(object sender, EventArgs e)
        {
            ServiceUbicaciones.cargarGridEstanteria(gridUbicaciones, ddlEstanteria.SelectedValue, "-1");
            ddlArticulo.SelectedIndex = 0;
        }

        protected void btnBuscarArt_Click(object sender, EventArgs e)
        {
            ServiceUbicaciones.cargarGridEstanteria(gridUbicaciones, "-1", ddlArticulo.SelectedValue);
            if (gridUbicaciones.Rows.Count == 0)
                lbError.Text = "No se encontro el articulo seleccionado";
            ddlEstanteria.SelectedIndex = 0;
        }
    }
}