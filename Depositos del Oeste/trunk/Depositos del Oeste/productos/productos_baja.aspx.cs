using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;

namespace Depositos_del_Oeste
{
    public partial class _Productos_Baja : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarProducto();
        }

        private void cargarProducto()
        {
            Articulo articulo = new Articulo();
            articulo.IdArticulo = int.Parse(Request.QueryString["id"]);
            articulo.Load();
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtAlto.Text = articulo.Alto.ToString();
            txtLargo.Text = articulo.Largo.ToString();
            txtAncho.Text = articulo.Ancho.ToString();
            txtPeso.Text = articulo.Peso.ToString();
            rblActividad.SelectedValue = articulo.Actividad.ToString();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();
            articulo.IdArticulo = int.Parse(Request.QueryString["id"]);
            articulo.BajaLogica();
        }

    }
}