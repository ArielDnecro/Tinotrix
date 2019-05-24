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
    public class Meses
    {
        private Guid _UidMes;

        public Guid UidMes
        {
            get { return _UidMes; }
            set { _UidMes = value; }
        }

        private string _StrMes;

        public string strMes
        {
            get { return _StrMes; }
            set { _StrMes = value; }
        }

        public class Repositorio
        {

            Conexion Conexion = new Conexion();
            public List<Meses> ConsultarMeses()
            {
                List<Meses> meses = new List<Meses>();

                SqlCommand comando = new SqlCommand();

                try
                {
                    comando.CommandText = "usp_ConsultarMeses";
                    comando.CommandType = CommandType.StoredProcedure;

                    DataTable table = new Connection().ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        Meses mes = new Meses()
                        {
                            UidMes = (Guid)row["UidMes"],
                            strMes = (string)row["VchMes"],
                        };
                        meses.Add(mes);
                    }
                }
                catch (SqlException e)
                {
                    throw;
                }

                return meses;


            }

            public Meses ObtenerMeses(string Dias)
            {
                Meses meses = null;

                DataTable table = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "usp_BuscarMeses";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@VchMeses", SqlDbType.NVarChar, 50);
                comando.Parameters["@VchMeses"].Value = Dias;

                table = Conexion.Busquedas(comando);


                foreach (DataRow row in table.Rows)
                {
                    meses = new Meses()
                    {
                        UidMes = (Guid)row["UidMes"],
                        strMes = (string)row["VchMes"],
                    };

                }


                return meses;


            }
        }
    }
}
