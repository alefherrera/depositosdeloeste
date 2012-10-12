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
    public class ServiceProductos : ServiceBase
    {
        public static Cliente cargarCliente(string idCliente){
            if(!Validaciones.isNumeric(idCliente))
                throw new RedireccionDatosException("Id de cliente incorrecta");

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
            articulo.Activo = 1;
            gridArticulos.AutoGenerateColumns = false;
            gridArticulos.ShowHeader = true;
            gridArticulos.DataSource = articulo.Select();
            gridArticulos.DataBind();
        }
 
        public static void cargarComboClientes(DropDownList ddlClientes)
        {
            string textField = "Id";
            string dataField = "Razon_Social";
            Cliente cliente = new Cliente();

            cargarDropDownList<Cliente>(textField, dataField, ddlClientes, cliente.Select());
            ddlClientes.Items.Insert(0, new ListItem("Seleccione Cliente", "-1"));
        }

        public static int insertarArticulo(Articulo articulo, Cliente cliente)
        {
            if (articulo == null || cliente == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append((articulo==null)?"Articulo es nulo <br />":"");
                sb.Append((cliente==null)?"Cliente es nulo <br />":"");
                sb.Append("insertarArticulo <br />");

                throw new MailException(sb.ToString());
            }
            
            if (cliente.Load())
            {
                throw new ErrorFormException("Cliente incorrecto");
            }

            return 1;
        }
    }
}
