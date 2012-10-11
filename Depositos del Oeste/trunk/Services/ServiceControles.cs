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
        public static void cargarArticulos(GridView gridArticulos, string seleccion)
        {
            List<string> DataKeyNames = new List<string>();
            DataKeyNames.Add("Nombre");
            DataKeyNames.Add("Descripcion");


            int sel = 0;
            if (Validaciones.isNumeric(seleccion))
                sel = int.Parse(seleccion);

            if (sel < 0)
            {
                gridArticulos.Visible = false;
                return;
            }

            Articulo articulo = new Articulo();
            articulo.IdCliente = sel;
            gridArticulos.AutoGenerateColumns = false;
            gridArticulos.ShowHeader = true;
            gridArticulos.DataKeyNames = DataKeyNames.ToArray();
            gridArticulos.DataSource = articulo.Select();
            gridArticulos.DataBind();
        }
        
        public static void cargarClientes(DropDownList ddlClientes)
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
