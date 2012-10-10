using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Menu : BusinessObject<Menu>
    {
        public virtual int Id { get; set; }
        public virtual int IdPadre { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Link { get; set; }
    }
}
