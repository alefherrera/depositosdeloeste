using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace BackEnd
{
    public class Grupo:BusinessObject<Grupo>
    {
        private int id;
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string descripcion;
        public virtual string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}
