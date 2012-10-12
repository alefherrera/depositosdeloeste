using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ErrorFormException : BackEndExcception
    {
        public ErrorFormException(string message)
            : base(message)
        {
        }
    }
}
