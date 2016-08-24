using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ada.Framework.Data.DBComparer.Entities;
using Ada.Framework.Data.DBComparer.Entities.Response;

namespace Ada.Framework.Data.DBComparer
{
    public class Comparador
    {
        [Obsolete("No terminado todavía.")]
        public InstanciaBaseDatosMerged Comparar(InstanciaBaseDatos referencia, IList<InstanciaBaseDatos> objetivos)
        {
            InstanciaBaseDatosMerged retorno = new InstanciaBaseDatosMerged();

            foreach (BaseDatos baseDatosReferencia in referencia.BasesDatos)
            {
                foreach (InstanciaBaseDatos instanciaObjetivo in objetivos)
                {
                    bool existeBaseDatos = false;
                    foreach (BaseDatos baseDatosObjetivo in instanciaObjetivo.BasesDatos)
                    {
                        if (baseDatosReferencia.Nombre == baseDatosObjetivo.Nombre)
                        {
                            existeBaseDatos = true;

                            BaseDatosMerged dbM = CompararBaseDatos(baseDatosReferencia, baseDatosObjetivo);
                        }
                    }
                    if(!existeBaseDatos)
                    {
                        BaseDatosMerged dbM = new BaseDatosMerged();
                        dbM.Nombre = baseDatosReferencia.Nombre;
                        foreach (Tabla tabla in baseDatosReferencia.Tablas)
                        {
                            TablaMerged tablaMerged = new TablaMerged();
                            tablaMerged.Nombre = tabla.Nombre;
                            foreach (Campo campo in tabla.Campos)
                            {
                                tablaMerged.CamposInexistentesEnObjetivo.Add(campo);

                            }
                            dbM.TablasInexistentesEnObjetivo.Add(tablaMerged);
                        }
                        retorno.BasesDatosInexistentesEnObjetivo.Add(dbM);
                    }
                }
            }

            //IList<BaseDatos> lista = objetivos.Where(c=>c.BasesDatos.Where(z => referencia.BasesDatos.Count(a=> a.Nombre == a.Nombre) == 0).Select(a=>a)).ToList();

            //BaseDatosMerged dbM = new BaseDatosMerged();
            //dbM.Nombre = baseDatosReferencia.Nombre;
            //foreach (Tabla tabla in baseDatosReferencia.Tablas)
            //{
            //    TablaMerged tablaMerged = new TablaMerged();
            //    tablaMerged.Nombre = tabla.Nombre;
            //    foreach (Campo campo in tabla.Campos)
            //    {
            //        tablaMerged.CamposInexistentesEnObjetivo.Add(campo);

            //    }
            //    dbM.TablasInexistentesEnObjetivo.Add(tablaMerged);
            //}
            //retorno.BasesDatosInexistentesEnObjetivo.Add(dbM);


            //retorno.BasesDatosInexistentesEnReferencia = ;

            return retorno;
        }

        public BaseDatosMerged CompararBaseDatos(BaseDatos referencia, BaseDatos objetivo)
        {
            BaseDatosMerged retorno = new BaseDatosMerged();

            foreach(Tabla tablaReferencia in referencia.Tablas)
            {
                bool existeTabla = false;
                foreach (Tabla tablaObjetivo in objetivo.Tablas)
                {
                    if(tablaObjetivo.Nombre == tablaReferencia.Nombre)
                    {
                        existeTabla = true;
                        TablaMerged tabla = CompararTablas(tablaReferencia, tablaObjetivo);

                        if (tabla.CamposInConsistentes.Count > 0 || tabla.CamposInexistentesEnObjetivo.Count > 0 || tabla.CamposInexistentesEnReferencia.Count > 0)
                        {
                            retorno.TablasInConsistentes.Add(tabla);
                        }
                        else
                        {
                            retorno.TablasConsistentes.Add(tabla);
                        }
                    }
                }

                if(!existeTabla)
                {
                    TablaMerged tablaMerged = new TablaMerged();                    
                    tablaMerged.Nombre = tablaReferencia.Nombre;
                    foreach (Campo campo in tablaReferencia.Campos)
                    {
                        tablaMerged.CamposInexistentesEnObjetivo.Add(campo);
                        
                    }
                    retorno.TablasInexistentesEnObjetivo.Add(tablaMerged);
                }
            }

            IList<Tabla> tablas = objetivo.Tablas.Where(c => referencia.Tablas.Count(x => x.Nombre == c.Nombre) == 0).ToList();

            IList<TablaMerged> tablasReferencia = new List<TablaMerged>();
            foreach (Tabla tabla in tablas)
            {
                TablaMerged tablaMerged = new TablaMerged();
                tablaMerged.Nombre = tabla.Nombre;
                foreach (Campo campo in tabla.Campos)
                {
                    tablaMerged.CamposInexistentesEnReferencia.Add(campo);
                }
                retorno.TablasInexistentesEnReferencia.Add(tablaMerged);
            }
            
            retorno.TablasInexistentesEnReferencia = tablasReferencia;

            return retorno;
        }

        public TablaMerged CompararTablas(Tabla referencia, Tabla objetivo)
        {
            TablaMerged retorno = new TablaMerged();
            retorno.Nombre = referencia.Nombre;

            for (int i = 0; i < referencia.Campos.Count; i++)
            {
                Campo campoReferencia = referencia.Campos[i];
                bool existeCampo = false;
                for (int x = 0; x < objetivo.Campos.Count; x++)
                {
                    Campo campoObjetivo = objetivo.Campos[x];

                    if (campoReferencia.Nombre == campoObjetivo.Nombre)
                    {
                        if (campoReferencia.Tipo == campoObjetivo.Tipo)
                        {
                            if (campoReferencia.Largo == campoObjetivo.Largo)
                            {
                                retorno.CamposConsistentes.Add(campoObjetivo);
                            }
                            else
                            {
                                retorno.CamposInConsistentes.Add(campoObjetivo);
                            }
                        }
                        else
                        {
                            retorno.CamposInConsistentes.Add(campoObjetivo);
                        }
                        existeCampo = true;
                    }
                }
                if (!existeCampo)
                {
                    retorno.CamposInexistentesEnObjetivo.Add(campoReferencia);
                }
            }

            retorno.CamposInexistentesEnReferencia = objetivo.Campos.Where(c => referencia.Campos.Count(x => x.Nombre == c.Nombre) == 0).ToList();

            return retorno;
        }
    }
}
