using Ada.Framework.Data.DBComparer.Entities;
using Ada.Framework.Data.DBConnector;
using Ada.Framework.Data.DBConnector.Entities.Query;
using Ada.Framework.Data.DBConnector.Exceptions;
using Ada.Framework.Data.DBConnector.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ada.Framework.Data.DBComparer.DAO
{
    internal class ComparadorDAO
    {
        private ConexionBaseDatos Conexion;
        
        public ComparadorDAO(ConexionBaseDatos conexion)
        {
            conexion.AutoConectarse = true;
            Conexion = conexion;
        }

        public IList<BaseDatos> ObtenerBasesDatos(bool incluirBasesSistema)
        {
            IList<BaseDatos> retorno = new List<BaseDatos>();
            try
            {
                Query consulta = Conexion.CrearQuery();
                if (incluirBasesSistema)
                {
                    consulta.Consulta = "SELECT name FROM sys.databases;";
                }
                else
                {
                    consulta.Consulta = "SELECT * FROM sys.sysdatabases WHERE sid <> 0x01";
                }

                Conexion.Abrir();
                IList<RespuestaEjecucion<string>> respuesta = consulta.Consultar<string>();

                foreach (RespuestaEjecucion<string> registro in respuesta)
                {
                    BaseDatos baseDatos = new BaseDatos() { Nombre = registro.Respuesta };
                    baseDatos.Tablas = ObtenerTablas(baseDatos);
                    retorno.Add(baseDatos);
                }
            }
            catch (EjecutarException ee)
            {
                throw ee;
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (ConexionException ce)
            {
                throw ce;
            }
            finally
            {
                Conexion.Cerrar();
            }
            return retorno;
        }

        public IList<Tabla> ObtenerTablas(BaseDatos baseDatos)
        {
            IList<Tabla> retorno = new List<Tabla>();
            try
            {
                Query consulta = Conexion.CrearQuery();
                consulta.Consulta = string.Format("SELECT name FROM {0}.sys.tables", baseDatos.Nombre);

                Conexion.Abrir();
                IList<RespuestaEjecucion<string>> respuesta = consulta.Consultar<string>();

                foreach (RespuestaEjecucion<string> registro in respuesta)
                {
                    Tabla tabla = new Tabla() { Nombre = registro.Respuesta };
                    tabla.BaseDatos = baseDatos;
                    tabla.Campos = ObtenerCampos(tabla);
                    retorno.Add(tabla);
                }
            }
            catch (EjecutarException ee)
            {
                throw ee;
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (ConexionException ce)
            {
                throw ce;
            }
            finally
            {
                Conexion.Cerrar();
            }
            return retorno;
        }

        public IList<Campo> ObtenerCampos(Tabla tabla)
        {
            IList<Campo> retorno = new List<Campo>();
            try
            {
                Query consulta = Conexion.CrearQuery();
                consulta.Consulta = string.Format(@"USE {0} SELECT name, (SELECT [name]
                                                                  FROM sys.types tp
                                                                  WHERE tp.user_type_id  = col.system_type_id) as type, max_length, is_nullable
                                                                  FROM {0}.sys.columns col
                                                                  WHERE object_id IN(
                                                                                    SELECT DISTINCT object_id
                                                                                    FROM sys.tables
                                                                                    WHERE name='{1}') ;"
                   , tabla.BaseDatos.Nombre, tabla.Nombre);

                Conexion.Abrir();
                consulta.CampoPropiedad.Add("name", "Nombre");
                consulta.CampoPropiedad.Add("type", "Tipo");
                consulta.CampoPropiedad.Add("is_nullable", "PermiteNulo");
                consulta.CampoPropiedad.Add("max_length", "Largo");
                IList<RespuestaEjecucion<Campo>> respuesta = consulta.Consultar<Campo>();

                foreach (RespuestaEjecucion<Campo> registro in respuesta)
                {
                    registro.Respuesta.Tabla = tabla;
                }

                retorno = respuesta.Select(c => c.Respuesta).ToList();
            }
            catch (EjecutarException ee)
            {
                throw ee;
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (ConexionException ce)
            {
                throw ce;
            }
            finally
            {
                Conexion.Cerrar();
            }
            return retorno;
        }
    }
}
