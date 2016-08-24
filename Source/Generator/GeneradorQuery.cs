using Ada.Framework.Data.DBComparer.Entities;
using Ada.Framework.Data.DBComparer.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ada.Framework.Data.DBComparer.Generator
{
    public class GeneradorQuery
    {
        public string GenerarCreate(Tabla tabla)
        {
            string retorno = "CREATE TABLE " + tabla.Nombre + " (";

            foreach (Campo campo in tabla.Campos)
            {
                retorno += string.Format("[{0}][{1}]({2}),", campo.Nombre, campo.Tipo, campo.Largo);
            }

            if (tabla.Campos.Count > 0)
            {
                retorno = retorno.Substring(0, retorno.Length - 1);
            }

            return retorno + ")";
        }

        public string GenerarAlter(TablaMerged tabla)
        {
            string retorno = string.Empty;

            foreach (Campo campo in tabla.CamposInexistentesEnObjetivo)
	        {
                retorno += string.Format("ALTER TABLE {0} ADD {1} {2}({3}) {4};\nGO\n\n", tabla.Nombre, campo.Nombre, campo.Tipo, campo.Largo, campo.PermiteNulo ? "NULL" : "NOT NULL");
	        }

            foreach (Campo campo in tabla.CamposInexistentesEnReferencia)
            {
                retorno += string.Format("ALTER TABLE {0} ADD {1} {2}({3}) {4};\nGO\n\n", tabla.Nombre, campo.Nombre, campo.Tipo, campo.Largo, campo.PermiteNulo ? "NULL" : "NOT NULL");
            }

            return retorno;
        }
    }
}
