using Ada.Framework.Data.DBComparer.DAO;
using Ada.Framework.Data.DBComparer.Entities;
using Ada.Framework.Data.DBConnector;
using Ada.Framework.Data.DBConnector.Connections;
using Ada.Framework.Data.DBConnector.Entities.DataBase;
using Ada.Framework.Data.DBConnector.Exceptions;
using Ada.Framework.Data.Notifications;
using Ada.Framework.Data.Notifications.TO;
using System;
using System.Collections.Generic;

namespace Ada.Framework.Data.DBComparer.Business
{
    internal class ComparadorBO : IComparadorBO
    {
        private ComparadorDAO dao;

        public ComparadorBO()
        {
            IConfiguracionBaseDatos config = ConfiguracionBaseDatosFactory.ObtenerConfiguracionDeBaseDatos();
            ConexionTO db = config.ObtenerConexion("DBComparer");
            ConexionBaseDatos conexion = ConexionBaseDatosFactory.ObtenerConexionBaseDatos(db);
            dao = new ComparadorDAO(conexion);
        }

        public ComparadorBO(ConexionBaseDatos conexion)
        {
            dao = new ComparadorDAO(conexion);
        }

        public ComparadorBO(string nombreConexion)
        {
            IConfiguracionBaseDatos config = ConfiguracionBaseDatosFactory.ObtenerConfiguracionDeBaseDatos();
            ConexionTO db = config.ObtenerConexion(nombreConexion);
            ConexionBaseDatos conexion = ConexionBaseDatosFactory.ObtenerConexionBaseDatos(db);
            dao = new ComparadorDAO(conexion);
        }

        public Notificacion<IList<BaseDatos>> ObtenerBasesDatos(bool incluirBasesSistema)
        {
            Notificacion<IList<BaseDatos>> retorno = new Notificacion<IList<BaseDatos>>();
            try
            {
                retorno.Respuesta = dao.ObtenerBasesDatos(incluirBasesSistema);
            }
            catch (EjecutarException ee)
            {
                retorno.AgregarMensaje(ee.Message, Severidad.Error, null, null, ee);
            }
            catch (ArgumentException ae)
            {
                retorno.AgregarMensaje(ae.Message, Severidad.Error, null, null, ae);
            }
            catch (ConexionException ce)
            {
                retorno.AgregarMensaje(ce.Message, Severidad.Error, null, null, ce);
            }
            return retorno;
        }

        public Notificacion<IList<Tabla>> ObtenerTablas(BaseDatos baseDatos)
        {
            Notificacion<IList<Tabla>> retorno = new Notificacion<IList<Tabla>>();
            try
            {
                retorno.Respuesta = dao.ObtenerTablas(baseDatos);
            }
            catch (EjecutarException ee)
            {
                retorno.AgregarMensaje(ee.Message, Severidad.Error, null, null, ee);
            }
            catch (ArgumentException ae)
            {
                retorno.AgregarMensaje(ae.Message, Severidad.Error, null, null, ae);
            }
            catch (ConexionException ce)
            {
                retorno.AgregarMensaje(ce.Message, Severidad.Error, null, null, ce);
            }
            return retorno;
        }

        public Notificacion<IList<Campo>> ObtenerCampos(Tabla tabla)
        {
            Notificacion<IList<Campo>> retorno = new Notificacion<IList<Campo>>();
            try
            {
                retorno.Respuesta = dao.ObtenerCampos(tabla);
            }
            catch (EjecutarException ee)
            {
                retorno.AgregarMensaje(ee.Message, Severidad.Error, null, null, ee);
            }
            catch (ArgumentException ae)
            {
                retorno.AgregarMensaje(ae.Message, Severidad.Error, null, null, ae);
            }
            catch (ConexionException ce)
            {
                retorno.AgregarMensaje(ce.Message, Severidad.Error, null, null, ce);
            }
            return retorno;
        }
    }
}
