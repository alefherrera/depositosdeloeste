using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message){}
    }

    public class AleException : Exception
    {
        public AleException(string message) : base(message) { }
    }

}
