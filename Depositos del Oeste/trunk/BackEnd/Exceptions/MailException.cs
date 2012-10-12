using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class MailException : BackEndExcception
    {
        public MailException(string message) : base(message) { }
    }
}
