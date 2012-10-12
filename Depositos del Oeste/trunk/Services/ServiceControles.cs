using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;

namespace Services
{
    public class ServiceControles
    {
        public static Cliente cargarCliente(string idCliente){
            if(!Validaciones.isNumeric(idCliente))
                throw new CargarDatosException("Id de cliente incorrecta");

            int id = int.Parse(idCliente);

            if (id < 0)
                throw new Exception("Id de cliente incorrecta");

            Cliente cliente = new Cliente();
            cliente.Id = id;
            cliente.Load();
            if (cliente.Loaded)
                return cliente;
            else
                throw new Exception("Los datos del cliente no se pudieron cargar");
        }

        public static void cargarGridArticulos(GridView gridArticulos, string seleccion)
        {
            int sel = 0;
            if (Validaciones.isNumeric(seleccion))
                sel = int.Parse(seleccion);

            if (sel < 0)
            {
                return;
            }

            Articulo articulo = new Articulo();
            articulo.IdCliente = sel;
            gridArticulos.AutoGenerateColumns = false;
            gridArticulos.ShowHeader = true;
            gridArticulos.DataSource = articulo.Select();
            gridArticulos.DataBind();
        }
        private static BoundField agregarColuma(string header, string field)
        {
            BoundField col = new BoundField();
            col.HeaderText = header;
            col.DataField = field;
            return col; 
        }
        private static TemplateField agregarTemplate(string header, Control[] controles)
        {
            TemplateField col = new TemplateField();
            return col;
        }

        public static void cargarComboClientes(DropDownList ddlClientes)
        {
            string textField = "Id";
            string dataField = "Razon_Social";
            Cliente cliente = new Cliente();

            cargarDropDownList<Cliente>(textField, dataField, ddlClientes, cliente.Select());
            ddlClientes.Items.Insert(0, new ListItem("Seleccione Cliente", "-1"));
        }
        private static void cargarDropDownList<T>(string valueField, string textField, DropDownList control, List<T> lista)
        {
            if (valueField == null || textField == null)
            {
                return;
            }
            control.DataTextField = textField;
            control.DataValueField = valueField;
            control.DataSource = lista;
            control.DataBind();
            return;
        }
    }
}
