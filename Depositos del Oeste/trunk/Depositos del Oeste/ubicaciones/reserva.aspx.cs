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
    public partial class _Reserva : PageBase
    {
        public DataTable dt { 
            get { return (DataTable)ViewState["dt"];}
            set { ViewState["dt"] = value; } 
        }
        protected List<Compartimiento> compartimientos_posibles = new List<Compartimiento>();

        protected void Page_Load(object sender, EventArgs e)
        {
            lbError.Text = "";
            if (!IsPostBack)
            {
                ServiceProductos.cargarComboClientes(ddlClientes);
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("index"));
                dt.Columns.Add(new DataColumn("id"));
                dt.Columns.Add(new DataColumn("desc"));
                dt.Columns.Add(new DataColumn("cant"));
                
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            gridArticulos.DataSource = dt;
            gridArticulos.DataBind();
        }

        protected void RegenerarIndices()
        {
            foreach (DataRow a in dt.Rows)
                a["index"] = dt.Rows.IndexOf(a);
        }

        protected void Eliminar(object sender, EventArgs e)
        {
            dt.Rows.RemoveAt(int.Parse(((LinkButton)sender).CommandArgument));
            RegenerarIndices();
        }
        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dt.Rows.Clear();

                if (int.Parse(ddlClientes.SelectedItem.Value) == (int)Enums.Combos.Seleccione)
                    pnlArticulosGeneral.Visible = false;
                else
                {
                    Articulo articulo = new Articulo();
                    articulo.IdCliente = int.Parse(ddlClientes.SelectedItem.Value);
                    ServiceProductos.cargarComboArticulos(ddlArticulo, articulo);
                    pnlArticulosGeneral.Visible = true;
                
                    Cliente cliente = ServiceProductos.cargarCliente(ddlClientes.SelectedItem.Value);
                    if (cliente.Estado == (int)Enums.Clientes_Estado.Moroso)
                        lbError.Text = "Aviso: Cliente Moroso";
                }
            }
            catch(ErrorFormException ex){
                lbError.Text = ex.Message;
            }
        }
        protected void linkAdd_Click(object sender, EventArgs e)
        {
            if (int.Parse(ddlArticulo.SelectedItem.Value) == (int)Enums.Combos.Seleccione)
            {
                lbError.Text = "Seleccione un articulo";
                return;
            }
            if (!Validaciones.isNumeric(txtCantidad.Text) || txtCantidad.Text == "" || txtCantidad.Text == "0")
            {
                lbError.Text = "Ingrese una cantidad correcta";
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row["id"].ToString() == ddlArticulo.SelectedItem.Value)
                {
                    if (!Validaciones.isNumeric(row["cant"].ToString()))
                    {
                        lbError.Text = "Error, cantidad incorrecta en el articulo";
                        return;
                    }
                    row["cant"] = int.Parse(row["cant"].ToString()) + int.Parse(txtCantidad.Text);
                    return;
                }

            }

            DataRow dr = dt.NewRow();
            dr["index"] = dt.Rows.Count;
            dr["id"] = ddlArticulo.SelectedValue;
            dr["desc"] = ddlArticulo.SelectedItem.Text;
            dr["cant"] = txtCantidad.Text;
            dt.Rows.Add(dr);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try{
                if (dt.Rows.Count == 0)
                {
                    lbError.Text = "Debe agregar articulos para registrar la reserva";
                    return;
                }

                List<Compartimiento> compartimientos_posibles;
                try
                {
                    compartimientos_posibles = posibles_Ubicaciones();
                }
                catch(ErrorFormException)
                {
                    return;
                }
                

                pnlSeleccion.Visible = false;
                pnlPreview.Visible = true;

                
                gridUbicaciones.DataSource = compartimientos_posibles;
                gridUbicaciones.DataBind();
            }
            catch(ErrorFormException ex)
            {
                lbError.Text = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            gridUbicaciones.Dispose();
            pnlSeleccion.Visible = true;
            pnlPreview.Visible = false;
        }

        

        protected List<Compartimiento> posibles_Ubicaciones()
        {
            bool error = false;

            foreach (DataRow articulo in dt.Rows)
            {
                try
                {
                    //Articulo que vamos a guardar
                    Articulo oArticulo = ServiceProductos.cargarArticulos(articulo["id"].ToString());
                    compartimientos_posibles = ServiceUbicaciones.posiblesUbicaciones(oArticulo, int.Parse(articulo["cant"].ToString()), compartimientos_posibles);
                }
                catch (ErrorFormException ex)
                {
                    lbError.Text += ex.Message + "<br/>";
                }
            }
            if (error)
                throw new ErrorFormException("");

            return compartimientos_posibles;

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            //Valido la fecha de retiro
            DateTime FechaRetiro = Validaciones.isDate(txtFechaRetiro.Text);
            if (FechaRetiro == null || DateTime.Today.CompareTo(FechaRetiro) >= 0)
            {
                lbError.Text = "Fecha de Retiro Incorrecta";
                return;
            }

            btnSubmit_Click(this, new EventArgs());

            pnlPreviewControles.Visible = false;

            string codigo = ServiceUbicaciones.generarCodigo();
            Cliente cliente = ServiceProductos.cargarCliente(ddlClientes.SelectedItem.Value);

            lbNota.Text = "Código: " + codigo;
            lbCorreo.Text = "Un correo fue enviado a " + cliente.Mail + " indicando el codigo de reserva";

            ServiceUbicaciones.registrarReserva(compartimientos_posibles, cliente, FechaRetiro, codigo);

            Lista_Mails.Codigo_Reserva(codigo, cliente.Mail);

            //TODO: En vez de imprimir el IdArticulo en la grilla imprimir el nombre de articulo
        }
    }
}