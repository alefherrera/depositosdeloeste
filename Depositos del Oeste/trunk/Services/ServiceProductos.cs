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

        public static Articulo cargarArticulos(string id)
        {
            Articulo articulo = new Articulo();
            if (!Validaciones.isNumeric(id))
                throw new RedireccionDatosException("Articulo incorrecto");

            articulo.IdArticulo = int.Parse(id);
            if (!articulo.Load())
                throw new RedireccionDatosException("Articulo incorrecto");
           
            return articulo;
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

        public static void insertarArticulo(Articulo articulo, Cliente cliente)
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
                throw new RedireccionDatosException("Cliente incorrecto");

            //TODO: Deshardcodear la actividad, un enum o algo que te guste, despues iterarlo para saber la cantidad o algo asi.1
            if (articulo.Actividad < 0 || articulo.Actividad >= 3)
                throw new ErrorFormException("La actividad es incorrecta");

            if (articulo.Nombre == "" || articulo.Nombre == null)
                throw new ErrorFormException("El nombre del articulo es incorrecto");

            if (articulo.Alto <= 0)
                throw new ErrorFormException("El alto del articulo es incorrecto");

            if (articulo.Largo <= 0)
                throw new ErrorFormException("El largo del articulo es incorrecto");

            if (articulo.Ancho <= 0)
                throw new ErrorFormException("El ancho del articulo es incorrecto");

            if (articulo.Peso <= 0)
                throw new ErrorFormException("El peso del articulo es incorrecto");

            if (articulo.Actividad < 0 || articulo.Actividad >= 3)
                throw new ErrorFormException("La actividad es incorrecta");

            articulo.IdCliente = cliente.Id;
            articulo.Activo = 1;

            articulo.Save();
        }
    }
}
