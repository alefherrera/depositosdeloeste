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
using BackEnd.Utils;

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
            if (FechaRemito == null)
            {
                lbError.Text = "Fecha de Remito Incorrecta";
                return;
            }
            if (FechaRemito.CompareTo(DateTime.Today) == 1)
            {
                lbError.Text = "Fecha de Remito Incorrecta, no puede ser mayor a la fecha actual";
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
                    if (cantidadRemito < 0)
                    {
                        lbError.Text = "Cantidad de articulos negativa";
                        return;
                    }
                    cantidadRemito = int.Parse(((TextBox)gridArticulos.Rows[i].FindControl("txtCantidad")).Text);
                }

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

            List<Compartimiento> comp_mail = new List<Compartimiento>();
            foreach (Compartimiento cmp in ingresados)
            if (cmp.IdArticulo == 0)
                comp_mail.Add(cmp);

            Cliente oCliente = ServiceProductos.cargarCliente(cliente.ToString());
            Lista_Mails.Facturacion(comp_mail, oCliente.Razon_Social);
            ServiceUbicaciones.registrarIngreso(ingresados, FechaRemito, txtDescripcion.Text, cliente, txtCodigo.Text);
            


            pnlReserva.Visible = false;
            lbSuccess.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Reserva oReserva = ServiceReservas.cargarReserva(txtCodigo.Text);

            ReservaDetalle oRsvDetalle = new ReservaDetalle();
            oRsvDetalle.CodigoReserva = oReserva.Codigo;

            List<Compartimiento> comp_mail = new List<Compartimiento>();

            List<ReservaDetalle> lsDetalle = oRsvDetalle.Select();
            foreach (ReservaDetalle detalle in lsDetalle)
            {
                Compartimiento cmp = new Compartimiento();
                cmp.Id = detalle.IdCompartimiento;
                cmp.Load();
                if (cmp.Estado == (int)Enums.Ubicaciones_Estado.Reservada && cmp.Cantidad == detalle.Cantidad)
                    comp_mail.Add(cmp);
            }

            ServiceUbicaciones.cancelarReserva(oReserva);
            Cliente oCliente = ServiceProductos.cargarCliente(oReserva.IdCliente.ToString());
            Lista_Mails.Facturacion(comp_mail, oCliente.Razon_Social);

            pnlReserva.Visible = false;
            lbSuccess.Visible = true;
            lbSuccess.Text = "La reserva se ha dado de baja con exito.";
        }
    }
}