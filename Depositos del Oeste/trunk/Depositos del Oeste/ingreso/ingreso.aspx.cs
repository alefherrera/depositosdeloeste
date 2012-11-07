using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using Services;
using System.IO;
using System.Data;

namespace Depositos_del_Oeste
{
    public partial class _Ingreso : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbError.Text = "";
        }

        protected void btnCodigo_Click(object sender, EventArgs e)
        {
            try
            {
                Reserva oReserva = ServiceReservas.cargarReserva(txtCodigo.Text);
                Cliente oCliente = ServiceProductos.cargarCliente(oReserva.IdCliente.ToString());
                lbCliente.Text = "Cliente: " + oCliente.Razon_Social;
                lbCodigo.Text = "Codigo: " + oReserva.Codigo;
                lbFechaReserva.Text = "Fecha de Reserva: " + oReserva.FechaReserva.ToShortDateString();
                gridArticulos.DataSource = ServiceReservas.detallesCodigo(oReserva.Codigo);
                gridArticulos.DataBind();

                pnlCodigo.Visible = false;
                pnlReserva.Visible = true;
            }
            catch (ErrorFormException ex)
            {
                lbError.Text = ex.Message;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Valido la fecha de retiro
            DateTime FechaRemito;
            try
            {
                FechaRemito = Validaciones.isDate(txtFechaRemito.Text);
            }
            catch(ErrorFormException)
            {
                lbError.Text = "Fecha de Remito Incorrecta";
                return;
            }
            if (FechaRemito == null || DateTime.Today.CompareTo(FechaRemito) >= 0)
            {
                lbError.Text = "Fecha de Remito Incorrecta";
                return;
            }
            DataTable articulos = ServiceReservas.detallesCodigo(txtCodigo.Text);
            List<Compartimiento> ingresados = new List<Compartimiento>();
            int cliente = 0;
            int ingreso_total = 0;

            for (int i = 0; i <= articulos.Rows.Count - 1; i++)
            {
                DataRow articulo = articulos.Rows[i];
                int cantidadReservada;
                int cantidadRemito;
                cantidadReservada = int.Parse(articulo["Cantidad"].ToString());

                if (((TextBox)gridArticulos.Rows[i].FindControl("txtCantidad")).Text == "")
                    cantidadRemito = 0;
                else
                {
                    if (!Validaciones.isNumeric(((TextBox)gridArticulos.Rows[i].FindControl("txtCantidad")).Text))
                    {
                        lbError.Text = "Cantidad incorrecta";
                        return;
                    }
                    cantidadRemito = int.Parse(((TextBox)gridArticulos.Rows[i].FindControl("txtCantidad")).Text);
                }

                cantidadRemito = int.Parse(((TextBox)gridArticulos.Rows[i].FindControl("txtCantidad")).Text);
                ingreso_total += cantidadRemito;
                if (cantidadRemito > cantidadReservada)
                {
                    lbError.Text = "Cantidad en el remito mayor a la cantidad reservada";
                    return;
                }

                Articulo oArticulo = new Articulo();
                oArticulo.IdArticulo = int.Parse(articulo["IdArticulo"].ToString());
                oArticulo.Load();
                if (!oArticulo.Loaded)
                {
                    lbError.Text = "Ocurrio un error al querer ingresar los articulos de la reserva, intente más tarde";
                    return;
                }
                //Aprovecho que hago un select para traer el cliente
                cliente = oArticulo.IdCliente;

                ingresados.AddRange(ServiceUbicaciones.ingresoUbicaciones(oArticulo, cantidadRemito, txtCodigo.Text));
            }
            if (ingreso_total == 0)
            {
                lbError.Text = "No se ha especificado ningun ingreso";
                return;
            }
            ServiceUbicaciones.registrarIngreso(ingresados, FechaRemito, txtDescripcion.Text, cliente, txtCodigo.Text);
            
            pnlReserva.Visible = false;
            lbSuccess.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Reserva oReserva = ServiceReservas.cargarReserva(txtCodigo.Text);
            ServiceUbicaciones.cancelarReserva(oReserva);

            pnlReserva.Visible = false;
            lbSuccess.Visible = true;
            lbSuccess.Text = "La reserva se ha dado de baja con exito.";
        }
    }
}