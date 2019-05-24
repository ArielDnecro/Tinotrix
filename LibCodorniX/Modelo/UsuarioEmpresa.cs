using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
    public class UsuarioEmpresa
    {
        public class Repository
        {
            Connection conn = new Connection();

            public List<Empresa> FindAll(Guid uidUsuario)
            {
                List<Empresa> empresas = new List<Empresa>();
                Empresa empresa = null;

                SqlCommand command = new SqlCommand();

                command.CommandText = "usp_UsuarioEmpresa_FindAll";
                command.CommandType = CommandType.StoredProcedure;

                command.AddParameter("@UidUsuario", uidUsuario, SqlDbType.UniqueIdentifier);
                
                try
                {
                    DataTable table = conn.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        empresa = new Empresa()
                        {
                            UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                            StrNombreComercial = row["VchNombreComercial"].ToString(),
                            StrRazonSocial = row["VchRazonSocial"].ToString(),
                            StrGiro = row["VchGiro"].ToString(),
                            StrRFC = row["ChRFC"].ToString(),
                            DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                        };
                        empresas.Add(empresa);
                    }
                
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Empresas from Usuario", e);
                }

                return empresas;
            }
        }
    }
}