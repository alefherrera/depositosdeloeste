﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace BackEnd
{
    public class Permiso : BusinessObject<Permiso>
    {
        public virtual int Id { get; set; }
        public virtual int IdGrupo { get; set; }
        public virtual string PermisoDesc { get; set; }
    }
}
