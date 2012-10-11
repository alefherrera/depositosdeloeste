using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Reserva : BusinessObject<Reserva>
    {
        public virtual int Id { get; set; }
        public virtual DateTime FechaRetiro { get; set; }
        public virtual int IdCliente { get; set; }
    }
}
