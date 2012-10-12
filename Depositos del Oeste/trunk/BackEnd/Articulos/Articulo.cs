using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Articulo : BusinessObject<Articulo>
    {
        public virtual int IdArticulo { get; set; }
        public virtual int IdCliente { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual int Alto { get; set; }
        public virtual int Largo { get; set; }
        public virtual int Ancho { get; set; }
        public virtual int Peso { get; set; }
        public virtual int Activo { get; set; }
    }
}
