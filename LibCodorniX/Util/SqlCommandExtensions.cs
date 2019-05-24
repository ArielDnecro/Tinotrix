using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Util
{
    public static class SqlCommandExtensions
    {
        public static SqlParameter AddParameter(this SqlCommand command, string parameterName, object value, SqlDbType sqlDbType, int size)
        {
            SqlParameter parameter = command.Parameters.Add(parameterName, sqlDbType, size);
            parameter.Value = value;

            return parameter;
        }

        public static SqlParameter AddParameter(this SqlCommand command, string parameterName, object value, SqlDbType sqlDbType)
        {
            SqlParameter parameter = command.Parameters.Add(parameterName, sqlDbType);
            parameter.Value = value;

            return parameter;
        }
    }
}