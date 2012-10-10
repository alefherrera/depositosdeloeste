using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace BackEnd
{
    public class Grupo : BusinessObject<Grupo>
    {
        public virtual int Id { get; set; }
        public virtual string Descripcion { get; set; }
    }
}
