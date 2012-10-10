using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Ubicacion : BusinessObject<Ubicacion>
    {
        public virtual int Id { get; set; }
        public virtual int IdNivel { get; set; }
        public virtual int idEspacio { get; set; }
        public virtual int IdEstante { get; set; }
    }
}
