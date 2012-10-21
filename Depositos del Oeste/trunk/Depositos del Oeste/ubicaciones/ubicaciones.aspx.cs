using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BackEnd;
using Services;

namespace Depositos_del_Oeste
{
    public partial class _Ubicaciones : PageBase
    {
         protected void Page_Load(object sender, EventArgs e)
          {
            if (!IsPostBack)
                ServiceUbicaciones.cargarComboEstanteria(ddlEstanteria);
            ServiceUbicaciones.cargarGridEstanteria(gridUbicaciones, ddlEstanteria.SelectedItem.Value);
        }

        protected void ddlEstanteria_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}