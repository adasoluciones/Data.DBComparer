using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ada.Framework.Data.DBComparer.Entities
{
    [Serializable]
    public class Tabla
    {
        [XmlIgnore]
        public BaseDatos BaseDatos { get; set; }

        public string Nombre { get; set; }
        public IList<Campo> Campos { get; set; }
    }
}
