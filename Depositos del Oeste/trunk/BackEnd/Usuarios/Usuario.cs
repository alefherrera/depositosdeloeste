using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace BackEnd
{
    public class Usuario:BusinessObject<Usuario>
    {
        private int legajo;
        public virtual int Legajo
        {
            get { return legajo; }
            set { legajo = value; }
        }

        private string nombre;
        public virtual string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string apellido;
        public virtual string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        private string dni;
        public virtual string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        private int idGrupo;
        public virtual int IdGrupo
        {
            get { return idGrupo; }
            set { idGrupo = value; }
        }


        private bool activo;
        public virtual bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }
    }
}
