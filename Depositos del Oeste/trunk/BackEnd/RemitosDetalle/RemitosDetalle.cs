using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class RemitosDetalle : BusinessObject<ReservaDetalle>
    {
        public virtual int IdCompartimiento{ get; set; }
        public virtual int IdRemito { get; set; }
        public virtual int IdArticulo { get; set; }
        public virtual int Cantidad{ get; set; }

        public override bool Equals(object obj)
        {
            RemitosDetalle remitoD = obj as RemitosDetalle;
            return base.Equals(obj) && IdCompartimiento == remitoD.IdCompartimiento && IdRemito == remitoD.IdRemito && IdArticulo == remitoD.IdArticulo;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + IdCompartimiento.GetHashCode();
            hash = (hash * 7) + IdRemito.GetHashCode();
            hash = (hash * 7) + IdArticulo.GetHashCode();
            return hash;
        }
    }
}