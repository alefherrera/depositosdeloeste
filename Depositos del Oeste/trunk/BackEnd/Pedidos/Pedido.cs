using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Pedido : BusinessObject<Reserva>
    {
        public virtual int Id { get; set; }
        public virtual DateTime FechaPedido { get; set; }
        public virtual int IdCliente { get; set; }
        /*public virtual List<Compartimiento> Detalles{ get; set; }
        public override void Save()
        {
            base.Save();

        }*/
        public override bool Equals(object obj)
        {
            Pedido rsv = obj as Pedido;
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
