﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class LoginException : BackEndExcception
    {
        public LoginException(string message) : base(message){}
    }
}
