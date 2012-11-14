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
    public partial class _Retiro : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lbError.Text = "";
            if(!IsPostBack)
                ServiceProductos.cargarComboClientes(ddlClientes);
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(ddlClientes.SelectedItem.Value) == (int)Enums.Combos.Seleccione)
                return;

            Compartimiento compartimiento = new Compartimiento();
            compartimiento.Estado = (int)Enums.Ubicaciones_Estado.Ocupada;
            DataTable dtIngresos = compartimiento.Select_Detalles(int.Parse(ddlClientes.SelectedItem.Value));
            if (!(dtIngresos.Rows.Count > 0))
            {
                lbError.Text = "El cliente no tiene mercaderia ingresada";
                return;
            }

            gridIngresados.DataSource = dtIngresos;
            gridIngresados.DataBind();

            lbCliente.Text = "Cliente: " + ddlClientes.SelectedItem.Text;

            pnlCliente.Visible = false;
            pnlPedido.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlPedido.Visible = false;
            pnlCliente.Visible = true;

            ddlClientes.SelectedValue = ((int)Enums.Combos.Seleccione).ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Valido la fecha de retiro
            DateTime FechaPedido;
            try
            {
                FechaPedido = Validaciones.isDate(txtFechaPedido.Text);
            }
            catch(ErrorFormException)
            {
                lbError.Text = "Fecha de Pedido Incorrecta";
                return;
            }

            Compartimiento compartimiento = new Compartimiento();
            compartimiento.Estado = (int)Enums.Ubicaciones_Estado.Ocupada;
            DataTable dtIngresos = compartimiento.Select_Detalles(int.Parse(ddlClientes.SelectedItem.Value));
            List<Compartimiento> compartimientos = new List<Compartimiento>();
            int cantidadTotal = 0;
            for (int i = 0; i <= dtIngresos.Rows.Count - 1; i++)
            {
                //Validaciones
                DataRow row = dtIngresos.Rows[i];
                int cantidadPedido = 0;
                if(((TextBox)gridIngresados.Rows[i].FindControl("txtCantidad")).Text == "")
                    cantidadPedido = 0;
                else
                {
                    if (!Validaciones.isNumeric(((TextBox)gridIngresados.Rows[i].FindControl("txtCantidad")).Text))
                    {
                        lbError.Text = "Cantidad incorrecta";
                        return;
                    }
                    cantidadPedido = int.Parse(((TextBox)gridIngresados.Rows[i].FindControl("txtCantidad")).Text);
                }

                int cantidadIngresada = int.Parse(row["cantidad"].ToString());
                cantidadTotal += cantidadPedido; 
                if (cantidadIngresada < cantidadPedido)
                {
                    lbError.Text = "Cantidad en el pedido mayor a cantidad almacenada";
                    return;
                }

                //Registro
                Compartimiento cmp = new Compartimiento();
                cmp.Id = int.Parse(row["idcompartimiento"].ToString());
                cmp.Load();
                cmp.Cantidad_Guardar = cantidadPedido;
                cmp.Cantidad -= cantidadPedido;
                if (cmp.Cantidad == 0)
                {
                    cmp.Estado = (int)Enums.Ubicaciones_Estado.Libre;
                    if (int.Parse(row["reservados"].ToString()) > 0)
                        cmp.Estado = (int)Enums.Ubicaciones_Estado.Reservada;
                }
                    
                compartimientos.Add(cmp);
            }
            if (cantidadTotal == 0)
            {
                lbError.Text = "No se ha especificado ningun retiro";
                return;
            }

            ServiceRetiro.registrarRetiro(compartimientos, int.Parse(ddlClientes.SelectedItem.Value.ToString()), FechaPedido);
            pnlPedido.Visible = false;
            lbSuccess.Visible = true;
        }
    }
}