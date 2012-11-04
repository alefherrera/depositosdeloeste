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
            DateTime FechaRemito = Validaciones.isDate(txtFechaRemito.Text);
            if (FechaRemito == null || DateTime.Today.CompareTo(FechaRemito) >= 0)
            {
                lbError.Text = "Fecha de Remito Incorrecta";
                return;
            }
            DataTable articulos = ServiceReservas.detallesCodigo(txtCodigo.Text);
            List<Compartimiento> ingresados = new List<Compartimiento>();
            int cliente = 0;

            foreach (DataRow articulo in articulos.Rows)
            {
                int cantidadReservada;
                if (articulo["Cantidad"].ToString() == "")
                    cantidadReservada = 0;
                else
                    cantidadReservada = int.Parse(articulo["Cantidad"].ToString());

                //TODO: Leer la cantidad del textbox
                int cantidadRemito = cantidadReservada;

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

            ServiceIngreso.registrarIngreso(ingresados, FechaRemito, txtDescripcion.Text, cliente, txtCodigo.Text);
        }
    }
}