using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
    /// <summary>
    /// Clase de conexión configurable. Permite realizar de forma transparente conexiones a una base de datos
    /// SQL Server y activar modos de transacciones sin necesidad de intervención extra.
    /// Es necesario tener configurada la clase <see cref="Config"/> o registrar un proveedor <see cref="IConfigProvider"/>
    /// a la instancia global <see cref="ConfigProviderManager"/>.
    /// </summary>
    public class Connection : IDisposable
    {
        /// <summary>
        /// Instancia privada de la conexión SQL Server. Se crea con cada nuevo objeto basado en la configuración
        /// proveeida en <see cref="Config"/> o en los proveedores registrados.
        /// </summary>
        private SqlConnection _Connection = new SqlConnection(Config.ConnectionString);

        /// <summary>
        /// Transacción actual de la conexión, usada solamente en caso de inicio de transacciones.
        /// </summary>
        private SqlTransaction CurrentTransaction = null;

        /// <summary>
        /// Libera los recursos utilizados por <see cref="Connection"/>.
        /// </summary>
        public void Dispose()
        {
            _Connection.Dispose();
            if (CurrentTransaction != null)
                CurrentTransaction.Dispose();
        }

        /// <summary>
        /// Inicia una transacción SQL.
        /// </summary>
        public void StartTransaction()
        {
            _Connection.Open();
            CurrentTransaction = _Connection.BeginTransaction();
        }

        /// <summary>
        /// Finaliza y confirma la transacción SQL.
        /// </summary>
        public void FinishTransaction()
        {
            if (CurrentTransaction == null)
            {
                throw new DatabaseException("This Connection instance doesn't have a Transaction active");
            }
            CurrentTransaction.Commit();
            CurrentTransaction.Dispose();
            CurrentTransaction = null;
        }

        /// <summary>
        /// Finaliza y revierte los cambios hechos durante la transacción SQL.
        /// </summary>
        public void CancelTransaction()
        {
            if (CurrentTransaction == null)
            {
                throw new DatabaseException("This Connection instance doesn't have a Transaction active");
            }
            CurrentTransaction.Rollback();
            CurrentTransaction.Dispose();
            CurrentTransaction = null;
        }

        /// <summary>
        /// Execute a SQL DML statement. This method not returns a <seealso cref="DataTable"/> only bool variable indicating the correct execution.
        /// </summary>
        /// <param name="command"><seealso cref="SqlCommand"/> statement</param>
        /// <param name="disponse">If it's true, disponse the <seealso cref="SqlCommand"/> statement; otherwise the caller must free it. By default it's true.</param>
        /// <returns>true if the statement executed successfully, otherwise false.</returns>
        public bool ExecuteCommand(SqlCommand command, bool disponse = true)
        {
            int result = 0;
            try
            {
                if (CurrentTransaction == null)
                    _Connection.Open();

                command.Connection = _Connection;
                if (CurrentTransaction != null)
                    command.Transaction = CurrentTransaction;
                result = command.ExecuteNonQuery();
                if (disponse)
                    command.Dispose();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (CurrentTransaction == null)
                    _Connection.Close();
            }

            return true;
        }

        /// <summary>
        /// Ejecuta una consulta de datos y retorna una instancia <see cref="DataTable"/> con la información obtenida.
        /// Esta variante solo trabaja con la primera tabla de datos que retorna la consulta.
        /// </summary>
        /// <param name="command">Objeto <see cref="SqlCommand"/> con la consulta preparada.</param>
        /// <returns>Una instancia <see cref="DataTable"/> con los resultados.</returns>
        public DataTable ExecuteQuery(SqlCommand command)
        {
            DataTable table = new DataTable();
            SqlDataReader reader;
            try
            {
                _Connection.Open();
                command.Connection = _Connection;
                reader = command.ExecuteReader();
                table.Load(reader);
            }
            catch (SqlException e)
            {
               throw e;
            }
            finally
            {
                _Connection.Close();
            }
            return table;
        }

        /// <summary>
        /// Ejecuta una consulta de datos y retorna una instancia <see cref="DataTable"/> con la información obtenida.
        /// Esta variante solo trabaja con la primera tabla de datos que retorna la consulta.
        /// </summary>
        /// <param name="command">Cadena de texto con la consulta preparada.</param>
        /// <returns>Una instancia <see cref="DataTable"/> con los resultados.</returns>
        public DataTable ExecuteQuery(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command);
            return ExecuteQuery(command);
        }
    }
}