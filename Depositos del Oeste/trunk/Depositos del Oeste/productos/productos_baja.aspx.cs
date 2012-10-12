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
    public partial class _Productos_Baja : PageBase
    {
        Articulo articulo;

        protected void Page_Load(object sender, EventArgs e)
        {
            articulo = ServiceProductos.cargarArticulos(Request.QueryString["id"]);
            
            lbNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            lbAlto.Text = articulo.Alto.ToString();
            lbLargo.Text = articulo.Largo.ToString();
            lbAncho.Text = articulo.Ancho.ToString();
            lbPeso.Text = articulo.Peso.ToString();
            lbActividad.Text = articulo.nombre_actividad();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();
            articulo.IdArticulo = int.Parse(Request.QueryString["id"]);
            articulo.BajaLogica();
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/productos/productos.aspx?id=" + articulo.IdCliente);
        }
    }
}