using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using Services;
using System.IO;

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

        }
    }
}