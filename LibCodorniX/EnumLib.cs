using System;

namespace CodorniX.Common
{

    /// <summary>
    /// Indica el orden en el que se ordenan los resultados de una consulta.
    /// </summary>
    public enum Orden { ASC, DESC }

    /// <summary>
    /// Indica la Acción actual a realizar.
    /// </summary>
    public enum Accion { Insertar, Modificar, Eliminar, Cancelar, Nada }
}
