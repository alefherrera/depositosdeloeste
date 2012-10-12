using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class CargarDatosException : BackEndExcception
    {
        public CargarDatosException(string message) : base(message) { }
    }
}
