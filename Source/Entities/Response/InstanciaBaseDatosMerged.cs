using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ada.Framework.Data.DBComparer.Entities.Response
{
    public class InstanciaBaseDatosMerged
    {
        public InstanciaBaseDatosMerged()
        {
            BasesDatosInConsistentes = new List<BaseDatosMerged>();
            BasesDatosConsistentes = new List<BaseDatosMerged>();
            BasesDatosInexistentesEnReferencia = new List<BaseDatosMerged>();
            BasesDatosInexistentesEnObjetivo = new List<BaseDatosMerged>();
        }

        public string Nombre { get; set; }
        public IList<BaseDatosMerged> BasesDatosInConsistentes { get; set; }
        public IList<BaseDatosMerged> BasesDatosConsistentes { get; set; }
        public IList<BaseDatosMerged> BasesDatosInexistentesEnReferencia { get; set; }
        public IList<BaseDatosMerged> BasesDatosInexistentesEnObjetivo { get; set; }
    }
}
