using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Utils;

namespace BackEnd
{
    public class Compartimiento : BusinessObject<Compartimiento>
    {
        public virtual int Id { get; set; }
        public virtual int NroEstanteria { get; set; }
        public virtual int Nivel { get; set; }
        public virtual int NroCompartimiento { get; set; }
        public virtual int Actividad { get; set; }
        public virtual int Estado { get; set; }
       
    }
}

