using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;

namespace Services
{
    public class ServiceMenu
    {
        //TODO: Implementar el tema de los privilegios para el usuario
        public static string generarMenu(Usuario user){
            //Traigo todos los menues
            BackEnd.Menu menu = new BackEnd.Menu();
            List<BackEnd.Menu> menuList = menu.Select();

            StringBuilder stringMenu = new StringBuilder();
            new List<BackEnd.Menu>();

            //Extraigo los padres
            List<BackEnd.Menu> menuPrincipal = new List<BackEnd.Menu>();
            menuPrincipal = menuList.FindAll(
                delegate(BackEnd.Menu mn)
                {
                    return mn.IdPadre == 0;
                }
            );

            //Empieza aca, se puede hacer recursivo pero son solo dos niveles estaticos, no hace falta
            stringMenu.Append("<ul>");
            for (int i = 0; menuPrincipal.Count > i; i++)
            {
                //Agarro todos los que son hijos del padre que voy iterando
                List<BackEnd.Menu> menuSecundario = new List<BackEnd.Menu>();
                menuSecundario = new List<BackEnd.Menu>();
                menuSecundario = menuList.FindAll(
                    delegate(BackEnd.Menu mn)
                    {
                        return mn.IdPadre == menuPrincipal[i].Id;
                    }
                );


                stringMenu.Append("<li class='has-sub '><a href='");
                stringMenu.Append(menuPrincipal[i].Link);
                stringMenu.Append("'><span>");
                stringMenu.Append(menuPrincipal[i].Nombre);
                stringMenu.Append("</span></a>");

                if (menuSecundario.Count > 0)
                {
                    stringMenu.Append("<ul>");
                    for (int j = 0; menuSecundario.Count > j; j++)
                    {
                        stringMenu.Append("<li class='has-sub'><a href='");
                        stringMenu.Append(menuSecundario[j].Link);
                        stringMenu.Append("'><span>");
                        stringMenu.Append(menuSecundario[j].Nombre);
                        stringMenu.Append("</span></a>");
                        stringMenu.Append("</li>");
                    }
                    stringMenu.Append("</ul>");
                }


                stringMenu.Append("</li>");
            }

            stringMenu.Append("</ul>");
            return stringMenu.ToString();
        }
    }
}
