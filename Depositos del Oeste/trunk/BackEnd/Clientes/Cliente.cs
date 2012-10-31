using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Cliente : BusinessObject<Cliente>
    {
        public virtual int Id { get; set; }
        public virtual string Razon_Social { get; set; }
        public virtual string Cuit { get; set; }
        public virtual int Estado { get; set; }
        public virtual string Mail { get; set; }
    }
}
