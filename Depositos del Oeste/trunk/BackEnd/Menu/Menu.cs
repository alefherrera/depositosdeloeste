using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Menu:BusinessObject<Menu>
    {
        private int id;
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int idPadre;
        public virtual int IdPadre
        {
            get { return idPadre; }
            set { idPadre = value; }
        }

        private string nombre;
        public virtual string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string link;
        public virtual string Link
        {
            get { return link; }
            set { link = value; }
        }
    }
}
