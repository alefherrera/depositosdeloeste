using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Reserva : BusinessObject<Reserva>
    {
        public virtual string Codigo { get; set; }
        public virtual DateTime FechaRetiro { get; set; }
        public virtual int IdCliente { get; set; }
        public virtual bool Activo { get; set; }
        public virtual DateTime FechaReserva { get; set; }
        /*public virtual List<Compartimiento> Detalles{ get; set; }
        public override void Save()
        {
            base.Save();

        }*/
    }
}
