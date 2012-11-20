using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class PedidosDetalle : BusinessObject<PedidosDetalle>
    {
        public virtual int IdCompartimiento{ get; set; }
        public virtual int IdPedido { get; set; }
        public virtual int IdArticulo { get; set; }
        public virtual int Cantidad{ get; set; }
        public virtual Compartimiento Compartimiento { get; set; }

        public override bool Load()
        {
            base.Load();
            Compartimiento cmp = new Compartimiento();
            cmp.Id = this.IdCompartimiento;
            cmp.Load();
            Compartimiento = cmp;
            if (this.Loaded && cmp.Loaded)
                return true;
            return false;
        }

        public override List<PedidosDetalle> Select()
        {
            List<PedidosDetalle> lista = base.Select();
            foreach (PedidosDetalle detalle in lista)
            {
                detalle.Compartimiento = new Compartimiento();
                detalle.Compartimiento.Id = detalle.IdCompartimiento;
                detalle.Compartimiento.Load();
            }
            return lista;
        }

        public override bool Equals(object obj)
        {
            PedidosDetalle remitoD = obj as PedidosDetalle;
            return base.Equals(obj) && IdCompartimiento == remitoD.IdCompartimiento && IdPedido == remitoD.IdPedido && IdArticulo == remitoD.IdArticulo;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + IdCompartimiento.GetHashCode();
            hash = (hash * 7) + IdPedido.GetHashCode();
            hash = (hash * 7) + IdArticulo.GetHashCode();
            return hash;
        }
    }
}