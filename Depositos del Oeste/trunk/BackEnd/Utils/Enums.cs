using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Utils
{
    public class Enums
    {
        public enum Combos
        {
            Seleccione = -1
        }
        public enum Ubicaciones_Actividad
        {
            Baja =  0,
            Media = 1, 
            Alta = 2
        }
        public static string ActividadDesc(int actividad)
        {
            Enums.Ubicaciones_Actividad enumDisplay = ((Enums.Ubicaciones_Actividad)actividad);
            return enumDisplay.ToString();
        }

        public enum Ubicaciones_Estado
        {
            Libre = 0,
            Reservada = 1,
            Ocupada = 2
        }
        public static string EstadoDesc(int estado)
        {
            Enums.Ubicaciones_Estado enumDisplay = ((Enums.Ubicaciones_Estado)estado);
            return enumDisplay.ToString();
        }
    }
}
