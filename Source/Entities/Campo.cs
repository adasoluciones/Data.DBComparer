using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ada.Framework.Data.DBComparer.Entities
{
    public class Campo
    {
        public Tabla Tabla { get; set; }

        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public Int32 Largo { get; set; }
        public bool PermiteNulo { get; set; }
    }
}
