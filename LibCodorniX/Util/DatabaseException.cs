using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace CodorniX.Util
{
    /// <summary>
    /// Represet all error while a table is populating.
    /// </summary>
    [Serializable]
    public class DatabaseException : Exception
    {

        /// <summary>
        /// Create a new <seealso cref="DatabaseException"/>.
        /// </summary>
        public DatabaseException() : base("Error in database operation")
        {
            
        }

        /// <summary>
        /// Create a new <seealso cref="DatabaseException"/> with a error message.
        /// </summary>
        /// <param name="message">Message than contains the reason or error.</param>
        public DatabaseException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// Create a new <seealso cref="DatabaseException"/> with a error message.
        /// </summary>
        /// <param name="message">Message than contains the reason or error.</param>
        /// <param name="baseException">The base exception, usually a <seealso cref="SqlException"/></param>
        public DatabaseException(string message, Exception baseException) : base(message, baseException)
        {

        }

    }
}