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
                throw new LoginException("Numero de Legajo incorrecto, tiene que ser numerico");
            };

            user.Legajo = int.Parse(legajo);

            List<Usuario> usuarios = user.Select();
            if (usuarios.Count != 1)
                throw new LoginException("Error, numero de legajo inexistente");

            user.Dni = dni;
            usuarios = user.Select();

            if (usuarios.Count != 1)
                throw new LoginException("Error, contraseña incorrecta");

            user.Load();

            return user;
        }
    }
}
