using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;
using BackEnd.Utils;

namespace Services
{
    public class ServicePermisos
    {
        public static bool VerificarPermisos(Usuario user, string perm)
        {
            //Verifico si es publico primero
            Permiso permiso = new Permiso();
            permiso.IdGrupo = (int)Enums.Grupos.Publico;
            permiso.PermisoDesc = perm;

            if (permiso.Select().Count > 0)
                return true;

            //Verifico en base al usuario
            if (user == null || user.Legajo == -1)
            {
                return false;
            }
           
            permiso.IdGrupo = user.IdGrupo;

            if (permiso.Select().Count == 0)
                return false;

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
