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
        public override bool Equals(object obj)
        {
            Reserva rsv = obj as Reserva;
            return base.Equals(obj) && rsv.Codigo == Codigo;
        }
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Codigo.GetHashCode();
            return hash;
        }

    }
}
