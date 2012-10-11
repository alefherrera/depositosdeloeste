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
        public static bool VerificarPermisos(Usuario user, string perm)
        {
            if (user == null || user.Legajo == -1)
            {
                return false;
            }
            Permiso permiso = new Permiso();
            permiso.IdGrupo = user.IdGrupo;
            permiso.PermisoDesc = perm;

            if (permiso.Select().Count == 0)
                return false;
            else
                return true;
        }

        public static List<Permiso> CargarPermisos(Usuario user)
        {
            Permiso permiso = new Permiso();
            permiso.IdGrupo = user.IdGrupo;
            return permiso.Select();
        }
    }
}
