using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using System.Data;
using System.Collections;
using BackEnd.Utils;

namespace Services
{
    public class ServiceUbicaciones : ServiceBase
    {
        public static void cargarComboEstanteria(DropDownList ddlUbicaciones)
        {
            string textField = "NroEstanteria";
            string dataField = "NroEstanteria";
            string query = "SELECT distinct NroEstanteria AS NroEstanteria FROM Compartimiento;";

            Compartimiento compartimiento = new Compartimiento();

            List<Hashtable> lista;
            lista = (List<Hashtable>)compartimiento.Select(query);

            cargarDropDownList(dataField, textField, ddlUbicaciones, lista);
            ddlUbicaciones.Items.Insert(0, new ListItem("Seleccione Estanteria", "-1"));
        }

        public static void cargarGridEstanteria(GridView gridEstanteria, string seleccion)
        {
            int sel = 0;
            if (Validaciones.isNumeric(seleccion))
                sel = int.Parse(seleccion);

            if (sel < 0)
            {
                return;
            }

            Compartimiento compartimiento = new Compartimiento();
            compartimiento.NroEstanteria = sel;
            gridEstanteria.AutoGenerateColumns = false;
            gridEstanteria.ShowHeader = true;
            gridEstanteria.DataSource = compartimiento.Select();
            gridEstanteria.DataBind();
        }

        /// <summary>
        /// Trae las posibles ubicaciones para determinada actividad, solo las libres y reservadas del mismo articulo.
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static List<Compartimiento> posiblesUbicaciones(Articulo articulo, int cantidad)
        {
            if (articulo == null)
                throw new Exception("Articulo es nulo");
            if (cantidad == null || cantidad == 0)
                throw new Exception("Cantidad Incorrecta");
            List<Compartimiento> compartimientos_posibles = new List<Compartimiento>();
            List<int> actividad = new List<int>();
            actividad.Add((int)Enums.Ubicaciones_Actividad.Alta);
            actividad.Add((int)Enums.Ubicaciones_Actividad.Media);
            actividad.Add((int)Enums.Ubicaciones_Actividad.Baja);

            actividad.Remove(articulo.Actividad);

            //Busco en las ubicaciones ocupadas si hay alguna con espacios libres, no tomo en cuenta la actividad
            //Lista de Compartimientos ocupados con el mismo articulo
            Compartimiento oCompartimiento = new Compartimiento();
            oCompartimiento.Estado = (int)Enums.Ubicaciones_Estado.Ocupada;
            oCompartimiento.IdArticulo = articulo.IdArticulo;

            List<Compartimiento> compartimientos = oCompartimiento.Select();
            foreach (Compartimiento compartimiento in compartimientos)
            {
                int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                int guardar;
                if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                {
                    guardar = cantidadPallet - compartimiento.Cantidad;
                    if (guardar > cantidad)
                        guardar = cantidad;
                    cantidad = cantidad - guardar;

                    compartimiento.Cantidad = cantidad;
                    compartimientos_posibles.Add(compartimiento);
                }
            }

            //Busco en las ubicaciones reservadas si hay alguna con espacios libres, no tomo en cuenta la actividad
            //Lista de Compartimientos ocupados con el mismo articulo
            oCompartimiento = new Compartimiento();
            oCompartimiento.Estado = (int)Enums.Ubicaciones_Estado.Reservada;
            oCompartimiento.IdArticulo = articulo.IdArticulo;

            compartimientos = oCompartimiento.Select();
            foreach (Compartimiento compartimiento in compartimientos)
            {
                int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                int guardar;
                if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                {
                    guardar = cantidadPallet - compartimiento.Cantidad;
                    if (guardar > cantidad)
                        guardar = cantidad;
                    cantidad = cantidad - guardar;

                    compartimiento.Cantidad = cantidad;
                    compartimientos_posibles.Add(compartimiento);
                }
            }

            //Busco los libres por indice de actividad igual al articulo
            oCompartimiento = new Compartimiento();
            oCompartimiento.Estado = (int)Enums.Ubicaciones_Estado.Libre;
            oCompartimiento.IdArticulo = articulo.IdArticulo;
            oCompartimiento.Actividad = articulo.Actividad;

            compartimientos = oCompartimiento.Select();
            foreach (Compartimiento compartimiento in compartimientos)
            {
                int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                int guardar;
                if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                {
                    guardar = cantidadPallet - compartimiento.Cantidad;
                    if (guardar > cantidad)
                        guardar = cantidad;
                    cantidad = cantidad - guardar;

                    compartimiento.Cantidad = cantidad;
                    compartimiento.Estado = (int)Enums.Ubicaciones_Estado.Reservada
                    compartimientos_posibles.Add(compartimiento);
                }
            }

            //Busco los libres por indice de actividad distintos al articulo
            oCompartimiento = new Compartimiento();
            oCompartimiento.Estado = (int)Enums.Ubicaciones_Estado.Libre;
            oCompartimiento.IdArticulo = actividad[0];
            oCompartimiento.Actividad = articulo.Actividad;

            compartimientos = oCompartimiento.Select();
            foreach (Compartimiento compartimiento in compartimientos)
            {
                int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                int guardar;
                if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                {
                    guardar = cantidadPallet - compartimiento.Cantidad;
                    if (guardar > cantidad)
                        guardar = cantidad;
                    cantidad = cantidad - guardar;

                    compartimiento.Cantidad = cantidad;
                    compartimiento.Estado = (int)Enums.Ubicaciones_Estado.Reservada
                    compartimientos_posibles.Add(compartimiento);
                }
            }

            //Busco los libres por indice de actividad distintos al articulo
            oCompartimiento = new Compartimiento();
            oCompartimiento.Estado = (int)Enums.Ubicaciones_Estado.Libre;
            oCompartimiento.IdArticulo = articulo.IdArticulo;
            oCompartimiento.Actividad = actividad[1];

            compartimientos = oCompartimiento.Select();
            foreach (Compartimiento compartimiento in compartimientos)
            {
                int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                int guardar;
                if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                {
                    guardar = cantidadPallet - compartimiento.Cantidad;
                    if (guardar > cantidad)
                        guardar = cantidad;
                    cantidad = cantidad - guardar;

                    compartimiento.Cantidad = cantidad;
                    compartimiento.Estado = (int)Enums.Ubicaciones_Estado.Reservada;
                    compartimientos_posibles.Add(compartimiento);
                }
            }

            return compartimientos_posibles;
        }
    }
}
