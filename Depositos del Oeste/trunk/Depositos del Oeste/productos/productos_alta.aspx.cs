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
            try
            {
                this.cliente = ServiceProductos.cargarCliente(Request.QueryString["id"]);
                lbCliente.Text = cliente.Razon_Social;

                ServiceProductos.insertarArticulo(new Articulo(), new Cliente());
            }
            catch (ErrorFormException ex)
            {
                lbError.Text = ex.Message;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
        }
    }
}