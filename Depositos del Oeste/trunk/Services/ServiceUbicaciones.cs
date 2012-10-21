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
    }
}
