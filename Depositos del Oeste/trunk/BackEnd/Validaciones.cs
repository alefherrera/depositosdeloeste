using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Validaciones
    {
        public static bool isNumeric(string test)
        {
            try
            {
                int.Parse(test);
                return true;
            }
            catch(Exception){
                return false;
            }
        }
    }
}
