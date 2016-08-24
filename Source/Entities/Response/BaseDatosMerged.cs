using System.Collections.Generic;

namespace Ada.Framework.Data.DBComparer.Entities.Response
{
    public class BaseDatosMerged
    {
        public BaseDatosMerged()
        {
            TablasInConsistentes = new List<TablaMerged>();
            TablasConsistentes = new List<TablaMerged>();
            TablasInexistentesEnReferencia = new List<TablaMerged>();
            TablasInexistentesEnObjetivo = new List<TablaMerged>();
        }

        public InstanciaBaseDatosMerged InstanciaBaseDatos { get; set; }
        public string Nombre { get; set; }
        public IList<TablaMerged> TablasInConsistentes { get; set; }
        public IList<TablaMerged> TablasConsistentes { get; set; }
        public IList<TablaMerged> TablasInexistentesEnReferencia { get; set; }
        public IList<TablaMerged> TablasInexistentesEnObjetivo { get; set; }
    }
}
