using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;

namespace Depositos_del_Oeste
{
    public partial class _Reportes : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lbError.Text = "";
            if (!IsPostBack)
            {
                CargarComboMes();
                CargarComboAno();
            }
        }

        private void CargarComboMes()
        {
            for (int i = 1; i < 13; i++)
            {
                ddlMes.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlMes.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void CargarComboAno()
        {
            for (int i = 2010; i < DateTime.Now.Year + 1; i++)
            {
                ddlAno.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlAno.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            int total = 0;
            System.Data.DataRow dr;
            System.Data.DataTable dt = BusinessObject<object>.SelectSQL("Call reportes (" + ddlMes.SelectedValue + "," + ddlAno.SelectedValue + ")");
            if (dt.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    total += Convert.ToInt32(row["porcentaje"].ToString().Replace("%", ""));
                    row["fecha"] = row["fecha"].ToString().Split(' ')[0];
                }
                dr = dt.NewRow();
                dr["fecha"] = "Total";
                dr["porcentaje"] = (total / dt.Rows.Count).ToString() + "%";
                dt.Rows.Add(dr);
                tblReporte.DataSource = dt;
            }
            else
            {
                lbError.Text = "No se encontraron reportes en el mes seleccionado";
            }
            tblReporte.DataBind();
        }

    }
}