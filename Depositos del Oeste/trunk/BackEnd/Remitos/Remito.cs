using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Remito : BusinessObject<Reserva>
    {
        public virtual int Id { get; set; }
        public virtual DateTime FechaRetiro { get; set; }
        public virtual int IdCliente { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual bool Activo { get; set; }
        public virtual DateTime FechaRemito { get; set; }
        /*public virtual List<Compartimiento> Detalles{ get; set; }
        public override void Save()
        {
            base.Save();
        }*/
        public override bool Equals(object obj)
        {
            Remito rsv = obj as Remito;
            return base.Equals(obj) && rsv.Id == Id;
        }
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Id.GetHashCode();
            return hash;
        }

    }
}
