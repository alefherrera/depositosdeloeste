using System;
using System.Collections.Generic;
using System.Globalization;
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
        public static DateTime isDate(string date)
        {
            DateTime Result; 
            DateTimeFormatInfo info = new DateTimeFormatInfo(); 
            CultureInfo culture; culture = CultureInfo.CreateSpecificCulture("es-ARG"); 
            info.ShortDatePattern = "dd/MM/yyyy";
            DateTime.TryParse(date, info, DateTimeStyles.None, out Result);
            return Result;
        }
    }
}
