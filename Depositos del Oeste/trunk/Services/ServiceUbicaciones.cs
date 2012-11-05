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
        public static void registrarReserva(List<Compartimiento> compartimientos_posibles, Cliente cliente, DateTime FechaRetiro, string codigo)
        {
            //TODO: Registrar la reserva
            //Registro la Reserva
            Reserva oReserva = new Reserva();
            oReserva.IdCliente = cliente.Id;
            oReserva.Codigo = codigo;
            oReserva.FechaReserva = DateTime.Today;
            oReserva.FechaRetiro = FechaRetiro.Date;
            oReserva.Activo = true;
            oReserva.Save();

            //Registro los compartimientos y su ocupacion
            foreach (Compartimiento compartimiento in compartimientos_posibles)
            {
                ReservaDetalle oReservaDetalle = new ReservaDetalle();
                oReservaDetalle.IdArticulo = compartimiento.IdArticulo;
                oReservaDetalle.IdCompartimiento = compartimiento.Id;
                oReservaDetalle.CodigoReserva = codigo;
                oReservaDetalle.IdArticulo = compartimiento.IdArticulo;
                oReservaDetalle.Cantidad = compartimiento.Cantidad;

                if (compartimiento.Estado == (int)Enums.Ubicaciones_Estado.Libre)
                    compartimiento.Estado = (int)Enums.Ubicaciones_Estado.Reservada;
                else
                {
                    Compartimiento cmpAnt = new Compartimiento();
                    cmpAnt.Id = compartimiento.Id;
                    cmpAnt.Load();
                    compartimiento.Cantidad += cmpAnt.Cantidad;
                }
                
                compartimiento.Update();


                oReservaDetalle.Save();
            }
        }
      
        public static void cargarComboEstanteria(DropDownList ddlUbicaciones)
        {
            string textField = "NroEstanteria";
            string dataField = "NroEstanteria";
            string query = "SELECT distinct NroEstanteria AS NroEstanteria FROM Compartimiento;";

            Compartimiento compartimiento = new Compartimiento();

            DataTable lista = compartimiento.Select(query);

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
        public static List<Compartimiento> posiblesUbicaciones(Articulo articulo, int cantidad, List<Compartimiento> compartimientos_posibles)
        {
            if (articulo == null)
                throw new Exception("Articulo es nulo");
            if (cantidad == 0)
                throw new Exception("Cantidad Incorrecta");
            if (compartimientos_posibles == null)
                throw new Exception("Compartimientos_Posibles es nulo");

            List<int> actividad = new List<int>();
            actividad.Add((int)Enums.Ubicaciones_Actividad.Alta);
            actividad.Add((int)Enums.Ubicaciones_Actividad.Media);
            actividad.Add((int)Enums.Ubicaciones_Actividad.Baja);

            actividad.Remove(articulo.Actividad);

            //Trato de llenar los bloques enteros
            //Busco los libres por indice de actividad igual al articulo
            Compartimiento oCompartimiento = new Compartimiento();
            oCompartimiento.Estado = (int)Enums.Ubicaciones_Estado.Libre;
            oCompartimiento.Actividad = articulo.Actividad;

            List<Compartimiento> compartimientos = oCompartimiento.Select();
            foreach (Compartimiento compartimiento in compartimientos)
            {
                //Me fijo en todos si que el compartimiento agarrado no tenga uno de los posibles anteriores
                if (compartimientos_posibles.Find(
                    delegate (Compartimiento cm)
                    {
                        return cm.Id == compartimiento.Id;
                    }
                    ) == null)
                {
                    int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                    int cantidadEntera = cantidadPallet * (cantidad / cantidadPallet);
                    if (compartimiento.Cantidad == 0 && cantidadEntera > 0)
                    {
                        cantidad = cantidad - cantidadPallet;
                        compartimiento.Cantidad = cantidadPallet;
                        compartimiento.IdArticulo = articulo.IdArticulo;
                        compartimientos_posibles.Add(compartimiento);
                    }
                }
            }

            //Busco en las ubicaciones ocupadas si hay alguna con espacios libres, no tomo en cuenta la actividad
            //Lista de Compartimientos ocupados con el mismo articulo
            oCompartimiento = new Compartimiento();
            oCompartimiento.Estado = (int)Enums.Ubicaciones_Estado.Ocupada;
            oCompartimiento.IdArticulo = articulo.IdArticulo;

            compartimientos = oCompartimiento.Select();
            foreach (Compartimiento compartimiento in compartimientos)
            {
                if (compartimientos_posibles.Find(
                    delegate(Compartimiento cm)
                    {
                        return cm.Id == compartimiento.Id;
                    }
                    ) == null)
                {
                    int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                    int guardar;
                    if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                    {
                        guardar = cantidadPallet - compartimiento.Cantidad;
                        if (guardar > cantidad)
                            guardar = cantidad;
                        cantidad = cantidad - guardar;
                        compartimiento.IdArticulo = articulo.IdArticulo;
                        compartimiento.Cantidad = guardar;
                        compartimientos_posibles.Add(compartimiento);
                    }
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
                if (compartimientos_posibles.Find(
                    delegate(Compartimiento cm)
                    {
                        return cm.Id == compartimiento.Id;
                    }
                    ) == null)
                {
                    int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                    int guardar;
                    if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                    {
                        guardar = cantidadPallet - compartimiento.Cantidad;
                        if (guardar > cantidad)
                            guardar = cantidad;
                        cantidad = cantidad - guardar;

                        compartimiento.IdArticulo = articulo.IdArticulo;
                        compartimiento.Cantidad = guardar;
                        compartimientos_posibles.Add(compartimiento);
                    }
                }
            }

            //Busco los libres por indice de actividad igual al articulo
            oCompartimiento = new Compartimiento();
            oCompartimiento.Estado = (int)Enums.Ubicaciones_Estado.Libre;
            oCompartimiento.Actividad = articulo.Actividad;

            compartimientos = oCompartimiento.Select();
            foreach (Compartimiento compartimiento in compartimientos)
            {
                if (!compartimientos_posibles.Contains(compartimiento))
                {
                    if (compartimientos_posibles.Find(
                    delegate(Compartimiento cm)
                    {
                        return cm.Id == compartimiento.Id;
                    }
                    ) == null)
                    {
                        int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                        int guardar;
                        if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                        {
                            guardar = cantidadPallet - compartimiento.Cantidad;
                            if (guardar > cantidad)
                                guardar = cantidad;
                            cantidad = cantidad - guardar;

                            compartimiento.IdArticulo = articulo.IdArticulo;
                            compartimiento.Cantidad = guardar;
                            compartimientos_posibles.Add(compartimiento);
                        }
                    }
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
                if (compartimientos_posibles.Find(
                    delegate(Compartimiento cm)
                    {
                        return cm.Id == compartimiento.Id;
                    }
                    ) == null)
                {
                    int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                    int guardar;
                    if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                    {
                        guardar = cantidadPallet - compartimiento.Cantidad;
                        if (guardar > cantidad)
                            guardar = cantidad;
                        cantidad = cantidad - guardar;

                        compartimiento.IdArticulo = articulo.IdArticulo;
                        compartimiento.Cantidad = guardar;
                        compartimientos_posibles.Add(compartimiento);
                    }
                }
            }

            //Busco los libres por indice de actividad distintos al articulo
            oCompartimiento = new Compartimiento();
            oCompartimiento.Estado = (int)Enums.Ubicaciones_Estado.Libre;
            oCompartimiento.Actividad = actividad[1];

            compartimientos = oCompartimiento.Select();
            foreach (Compartimiento compartimiento in compartimientos)
            {
                if (compartimientos_posibles.Find(
                    delegate(Compartimiento cm)
                    {
                        return cm.Id == compartimiento.Id;
                    }
                    ) == null)
                {
                    int cantidadPallet = compartimiento.cantidad_maxima(articulo);
                    int guardar;
                    if (compartimiento.Cantidad < cantidadPallet && cantidad > 0)
                    {
                        guardar = cantidadPallet - compartimiento.Cantidad;
                        if (guardar > cantidad)
                            guardar = cantidad;
                        cantidad = cantidad - guardar;

                        compartimiento.IdArticulo = articulo.IdArticulo;
                        compartimiento.Cantidad = guardar;
                        compartimientos_posibles.Add(compartimiento);
                    }
                }
            }
            if(cantidad > 0){
                throw new ErrorFormException("No se pueden almacenar " + cantidad.ToString() + " " + articulo.Nombre + "/s");
            }
            return compartimientos_posibles;
        }
        public static List<Compartimiento> ingresoUbicaciones(Articulo articulo, int cantidad, string codigo)
        {
            if (cantidad == 0)
            {
                return new List<Compartimiento>();
            }
            ReservaDetalle oReservaDetalle = new ReservaDetalle();
            oReservaDetalle.CodigoReserva = codigo;
            oReservaDetalle.IdArticulo = articulo.IdArticulo;
            List<ReservaDetalle> reservadetalles = oReservaDetalle.Select();
            reservadetalles.Sort(
                delegate(ReservaDetalle p1, ReservaDetalle p2)
                {
                    return p2.Cantidad.CompareTo(p1.Cantidad);
                }
            );

            List<Compartimiento> compartimientos = new List<Compartimiento>();
            foreach (ReservaDetalle detalle in reservadetalles)
            {
                Compartimiento compartimiento = new Compartimiento();
                compartimiento.Id = detalle.IdCompartimiento;
                compartimiento.Load();

                if (cantidad > 0)
                {
                    compartimiento.IdArticulo = articulo.IdArticulo;
                    compartimiento.Estado = (int)Enums.Ubicaciones_Estado.Ocupada;

                    if (detalle.Cantidad <= cantidad)
                    {
                        cantidad -= detalle.Cantidad;
                        compartimiento.Cantidad_Guardar = detalle.Cantidad;
                    }
                    else
                    {
                        compartimiento.Cantidad_Guardar = cantidad;
                        compartimiento.Cantidad += cantidad;
                        compartimiento.Cantidad -= detalle.Cantidad;
                        cantidad = 0;
                    }
                }
                else
                {
                    if (compartimiento.Estado == (int)Enums.Ubicaciones_Estado.Reservada && compartimiento.Cantidad == detalle.Cantidad)
                    {
                        compartimiento.Estado = (int)Enums.Ubicaciones_Estado.Libre;
                        compartimiento.IdArticulo = 0;
                    }
                    compartimiento.Cantidad -= detalle.Cantidad;
                    compartimiento.Cantidad_Guardar = -detalle.Cantidad;
                }

                compartimientos.Add(compartimiento);
            }

            return compartimientos;
        }
    }
}
