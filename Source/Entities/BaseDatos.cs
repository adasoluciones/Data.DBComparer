using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ada.Framework.Data.DBComparer.Entities
{
    public class BaseDatos
    {
        public string Nombre { get; set; }
        public IList<Tabla> Tablas { get; set; }
    }
}
