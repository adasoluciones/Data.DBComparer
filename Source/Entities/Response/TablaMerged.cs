using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ada.Framework.Data.DBComparer.Entities.Response
{
    public class TablaMerged
    {
        public TablaMerged()
        {
            CamposInConsistentes = new List<Campo>();
            CamposConsistentes = new List<Campo>();
            CamposInexistentesEnReferencia = new List<Campo>();
            CamposInexistentesEnObjetivo = new List<Campo>();
        }

        public BaseDatosMerged BaseDatos { get; set; }
        public string Nombre { get; set; }
        public IList<Campo> CamposInConsistentes { get; set; }
        public IList<Campo> CamposConsistentes { get; set; }
        public IList<Campo> CamposInexistentesEnReferencia { get; set; }
        public IList<Campo> CamposInexistentesEnObjetivo { get; set; }
    }
}
