using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace BackEnd
{
    public class Permiso : BusinessObject<Permiso>
    {
        private int id;
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int idGrupo;
        public virtual int IdGrupo
        {
            get { return idGrupo; }
            set { idGrupo = value; }
        }

        private string permiso;
        public virtual string PermisoDesc
        {
            get { return permiso; }
            set { permiso = value; }
        }
    }
}
