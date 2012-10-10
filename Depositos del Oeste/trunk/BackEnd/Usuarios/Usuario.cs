using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace BackEnd
{
    public class Usuario : BusinessObject<Usuario>
    {
        public virtual int Legajo { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellido { get; set; }
        public virtual string Dni { get; set; }
        public virtual int IdGrupo { get; set; }
        public virtual bool Activo { get; set; }
    }
}
