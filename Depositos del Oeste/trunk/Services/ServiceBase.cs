using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using System.Collections;
using System.Data;

namespace Services
{
    public class ServiceBase
    {
        protected static BoundField agregarColuma(string header, string field)
        {
            BoundField col = new BoundField();
            col.HeaderText = header;
            col.DataField = field;
            return col;
        }
        protected static TemplateField agregarTemplate(string header, Control[] controles)
        {
            TemplateField col = new TemplateField();
            return col;
        }

        protected static void cargarDropDownList<T>(string valueField, string textField, DropDownList control, List<T> lista)
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

        protected static void cargarDropDownList(string valueField, string textField, DropDownList control, DataTable lista)
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
