using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class BackEndExcception : Exception
    {
        public BackEndExcception(string message) : base(message) { }
    }
}
