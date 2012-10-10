using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;

namespace Services
{
    public class ServicePermisos
    {
        public static bool VerificarPermisos(Usuario user, int menu)
        {
            Permiso permiso = new Permiso();
            permiso.IdGrupo = user.IdGrupo;
            permiso.IdMenu = menu;

            if (permiso.Select().Count == 0)
                return true;
            else
                return false;
        }

        public static List<Permiso> CargarPermisos(Usuario user)
        {
            Permiso permiso = new Permiso();
            permiso.IdGrupo = user.IdGrupo;
            return permiso.Select();
        }
    }
}
