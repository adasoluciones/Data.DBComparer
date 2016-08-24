using Ada.Framework.Data.DBComparer.Business;
using Ada.Framework.Data.DBConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ada.Framework.Data.DBComparer
{
    public static class ComparadorBOFactory
    {
        public static IComparadorBO ObtenerNegocio()
        {
            return new ComparadorBO();
        }

        public static IComparadorBO ObtenerNegocio(ConexionBaseDatos conexion)
        {
            return new ComparadorBO(conexion);
        }

        public static IComparadorBO ObtenerNegocio(string nombreConexion)
        {
            return new ComparadorBO(nombreConexion);
        }
    }
}
