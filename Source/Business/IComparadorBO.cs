using Ada.Framework.Data.DBComparer.Entities;
using Ada.Framework.Data.Notifications;
using System.Collections.Generic;

namespace Ada.Framework.Data.DBComparer.Business
{
    public interface IComparadorBO
    {
        Notificacion<IList<BaseDatos>> ObtenerBasesDatos(bool incluirBasesSistema);
        Notificacion<IList<Tabla>> ObtenerTablas(BaseDatos baseDatos);
        Notificacion<IList<Campo>> ObtenerCampos(Tabla tabla);
    }
}
