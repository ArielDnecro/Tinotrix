using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CodorniX.ConexionDB;

namespace CodorniX.Modelo
{
    /// <summary>
    /// Clase que representa los días de la semana.
    /// </summary>
    public class Dias
    {
        private Guid _UidDias;

        /// <summary>
        /// Identificador único del día.
        /// </summary>
        public Guid UidDias
        {
            get { return _UidDias; }
            set { _UidDias = value; }
        }

        private string _StrDias;

        /// <summary>
        /// Cadena de texto representativa del día, por ejemplo: lunes, martes, etc.
        /// </summary>
        public string StrDias
        {
            get { return _StrDias; }
            set { _StrDias = value; }
        }

        /// <summary>
        /// Clase repositorio de <see cref="Dias"/>.
        /// </summary>
        public class Repositorio
        {
            Conexion Conexion = new Conexion();

            /// <summary>
            /// Obtiene todos los días registrados.
            /// </summary>
            /// <returns>Lista de los días.</returns>
            public List<Dias> ConsultarDias()
            {
                List<Dias> dias = new List<Dias>();

                SqlCommand comando = new SqlCommand();

                try
                {
                    comando.CommandText = "usp_ConsultarDias";
                    comando.CommandType = CommandType.StoredProcedure;

                    DataTable table = new Connection().ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        Dias dia = new Dias()
                        {
                            UidDias = (Guid)row["UidDias"],
                            StrDias = (string)row["VchDias"],
                        };
                        dias.Add(dia);
                    }
                }
                catch (SqlException e)
                {
                    throw;
                }

                return dias;
            }

            /// <summary>
            /// Obtiene un día a partir del nombre.
            /// </summary>
            /// <param name="Dias">cadena de texto con el día</param>
            /// <returns>Objeto <see cref="Dias"/>.</returns>
            public Dias ObtenerDias(string Dias)
            {
                Dias dias = null;

                DataTable table = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "usp_BuscarDias";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@VchDias", SqlDbType.NVarChar, 50);
                comando.Parameters["@VchDias"].Value = Dias;

                table = Conexion.Busquedas(comando);
                
                foreach (DataRow row in table.Rows)
                {
                    dias = new Dias()
                    {
                        UidDias = (Guid)row["UidDias"],
                        StrDias = (string)row["VchDias"],
                    };
                }
                
                return dias;
            }
        }
    }
}
