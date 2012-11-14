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
    public class ServiceRemitos : ServiceBase
    {
        public static void cargarGridRemitos(GridView gridRemitos, string seleccion)
        {
            int sel = 0;
            if (Validaciones.isNumeric(seleccion))
                sel = int.Parse(seleccion);

            if (sel < 0)
            {
                return;
            }

            Remito remito = new Remito();
            remito.IdCliente = sel;

            gridRemitos.AutoGenerateColumns = false;
            gridRemitos.ShowHeader = true;
            List<Remito> remitos = remito.Select();
            remitos.Sort(
                delegate(Remito p1, Remito p2)
                {
                    return p2.Id.CompareTo(p1.Id);
                }
            );
            gridRemitos.DataSource = remitos;
            gridRemitos.DataBind();
        }

        public static Remito cargarRemito(int idRemito)
        {
            if (idRemito <= 0)
                throw new ErrorFormException("Codigo de Remito Incorrecto");
            Remito oRemito = new Remito();
            oRemito.Id = idRemito;
            oRemito.Load();
            if (oRemito.Loaded)
                return oRemito;
            throw new ErrorFormException("Remito inexistente");
        }
    }
}
