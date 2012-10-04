using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;

namespace Services
{
    public class ServiceLogin
    {
        public static Usuario loguear(string legajo, string dni)
        {
            Usuario user = new Usuario();
            if (!Validaciones.isNumeric(legajo))
            {
                throw new LoginException("Numero de Legajo incorrecto");
            };

            user.Legajo = int.Parse(legajo);
            user.Dni = dni;

            List<Usuario> usuarios = user.Select();
            if (usuarios.Count != 1)
            {
                throw new LoginException("Error, el numero de legajo o la contraseña es incorrecto/a");
            }

            user.Load();

            return user;
        }
    }
}
