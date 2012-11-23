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

        public override List<ReservaDetalle> Select()
        {
            List<ReservaDetalle> lista = base.Select();
            foreach (ReservaDetalle detalle in lista)
            {
                detalle.Compartimiento = new Compartimiento();
                detalle.Compartimiento.Id = detalle.IdCompartimiento;
                detalle.Compartimiento.Load();
            }
            return lista;
        }

        public override bool Equals(object obj)
        {
            ReservaDetalle reservaD = obj as ReservaDetalle;
            return base.Equals(obj) && IdCompartimiento == reservaD.IdCompartimiento && CodigoReserva == reservaD.CodigoReserva && IdArticulo == reservaD.IdArticulo;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + IdCompartimiento.GetHashCode();
            hash = (hash * 7) + CodigoReserva.GetHashCode();
            hash = (hash * 7) + IdArticulo.GetHashCode();
            return hash;
        }
    }
}