using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ReservaDetalle : BusinessObject<ReservaDetalle>
    {
        public virtual int IdCompartimiento{ get; set; }
        public virtual string CodigoReserva { get; set; }
        public virtual int IdArticulo { get; set; }
        public virtual int Cantidad{ get; set; }
    }
}