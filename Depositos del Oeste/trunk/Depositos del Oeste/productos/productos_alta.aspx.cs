using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using Services;
using System.Text;

namespace Depositos_del_Oeste
{
    public partial class _Productos_Alta : PageBase
    {
        Cliente cliente;
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarCliente();
        }

        private void cargarCliente()
        {
            this.cliente = ServiceProductos.cargarCliente(Request.QueryString["id"]);
            lbCliente.Text = cliente.Razon_Social;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceProductos.insertarArticulo(cargarArticulo(),this.cliente);
                Response.Redirect("/productos/productos.aspx?id=" + cliente.Id);
            }
            catch(ErrorFormException ex)
            {
                lbError.Text = ex.Message;
            }
        }

        private Articulo cargarArticulo()
        {
            Articulo articulo = new Articulo();
            articulo.Nombre = txtNombre.Text;
            articulo.Descripcion = txtDescripcion.Text;

            if(!Validaciones.isNumeric(txtAlto.Text))
                throw new ErrorFormException("Altura incorrecta");
            articulo.Alto = int.Parse(txtAlto.Text);

            if(!Validaciones.isNumeric(txtLargo.Text))
                throw new ErrorFormException("Largo incorrecto");
            articulo.Largo = int.Parse(txtLargo.Text);

            if(!Validaciones.isNumeric(txtAncho.Text))
                throw new ErrorFormException("Ancho incorrecto");
            articulo.Ancho = int.Parse(txtAncho.Text);

            if(!Validaciones.isNumeric(txtPeso.Text))
                throw new ErrorFormException("Peso incorrecto");
            articulo.Peso = int.Parse(txtPeso.Text);

            if (!Validaciones.isNumeric(rblActividad.SelectedValue))
                throw new ErrorFormException("Indice de actividad incorrecto");
            articulo.Actividad = int.Parse(rblActividad.SelectedValue);
            
            return articulo;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/productos/productos.aspx?id=" + cliente.Id);
        }
    }
}