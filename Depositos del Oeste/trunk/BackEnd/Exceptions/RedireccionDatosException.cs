using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class RedireccionDatosException : BackEndExcception
    {
        public RedireccionDatosException(string message)
            : base(message)
        {
        }
    }
}
