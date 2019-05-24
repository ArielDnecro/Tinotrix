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
    /// Clase que representa las direcciones.
    /// </summary>
    [Serializable]
    public class Direccion
    {
        /// <summary>
        /// Indica el estado del objeto. true si es creado por el usuario, false si fue extraido de la base de datos.
        /// </summary>
        protected bool _IsUserCreated = true;

        /// <summary>
        /// Identificador único de dirección
        /// </summary>
        protected Guid _UidDireccion;

        /// <summary>
        /// Identificador único de dirección
        /// </summary>
        public Guid UidDireccion
        {
            get { return _UidDireccion; }
            set { _UidDireccion = value; }
        }

        /// <summary>
        /// Identificador único del país.
        /// </summary>
        protected Guid _UidPais;

        /// <summary>
        /// Identificador único del país.
        /// </summary>
        public Guid UidPais
        {
            get { return _UidPais; }
            set { _UidPais = value; }
        }

        /// <summary>
        /// Identificador único del estado.
        /// </summary>
        protected Guid _UidEstado;

        /// <summary>
        /// Identificador único del estado.
        /// </summary>
        public Guid UidEstado
        {
            get { return _UidEstado; }
            set { _UidEstado = value; }
        }

        /// <summary>
        /// Nombre del municipio.
        /// </summary>
        protected string _StrMunicipio;

        /// <summary>
        /// Nombre del municipio.
        /// </summary>
        public string StrMunicipio
        {
            get { return _StrMunicipio; }
            set { _StrMunicipio = value; }
        }

        /// <summary>
        /// Nombre de la ciudad.
        /// </summary>
        protected string _StrCiudad;

        /// <summary>
        /// Nombre de la ciudad.
        /// </summary>
        public string StrCiudad
        {
            get { return _StrCiudad; }
            set { _StrCiudad = value; }
        }

        /// <summary>
        /// Nombre de la colonia.
        /// </summary>
        protected string _StrColonia;

        /// <summary>
        /// Nombre de la colonia.
        /// </summary>
        public string StrColonia
        {
            get { return _StrColonia; }
            set { _StrColonia = value; }
        }

        /// <summary>
        /// Calle sobre la que se encuentra
        /// </summary>
        protected string _StrCalle;

        /// <summary>
        /// Calle sobre la que se encunetra.
        /// </summary>
        public string StrCalle
        {
            get { return _StrCalle; }
            set { _StrCalle = value; }
        }

        /// <summary>
        /// Calle lateral
        /// </summary>
        protected string _StrConCalle;

        /// <summary>
        /// Calle lateral
        /// </summary>
        public string StrConCalle
        {
            get { return _StrConCalle; }
            set { _StrConCalle = value; }
        }

        /// <summary>
        /// Calle lateral
        /// </summary>
        protected string _StrYCalle;

        /// <summary>
        /// Calle lateral
        /// </summary>
        public string StrYCalle
        {
            get { return _StrYCalle; }
            set { _StrYCalle = value; }
        }

        /// <summary>
        /// Número exterior.
        /// </summary>
        protected string _StrNoExt;

        /// <summary>
        /// Número exterior.
        /// </summary>
        public string StrNoExt
        {
            get { return _StrNoExt; }
            set { _StrNoExt = value; }
        }

        /// <summary>
        /// Número interior.
        /// </summary>
        protected string _StrNoInt;

        /// <summary>
        /// Número interior.
        /// </summary>
        public string StrNoInt
        {
            get { return _StrNoInt; }
            set { _StrNoInt = value; }
        }

        /// <summary>
        /// Referencia para la ubicación.
        /// </summary>
        protected string _StrReferencia;

        /// <summary>
        /// Referencia para la ubicación.
        /// </summary>
        public string StrReferencia
        {
            get { return _StrReferencia; }
            set { _StrReferencia = value; }
        }

        /// <summary>
        /// Dirección en formato largo.
        /// </summary>
        public string LongDirection
        {
            get
            {
                string d;

                d = _StrCalle + " con " + _StrConCalle + " y " + _StrYCalle + " No.Ext: " + _StrNoExt + ", Col. " + _StrColonia + ", " + _StrCiudad;

                return d;
            }
        }

        /// <summary>
        /// Clase repositorio de <see cref="Direccion"/>.
        /// </summary>
        public class Repository
        {
            /// <summary>
            /// Instancia de conexión.
            /// </summary>
            protected Connection _Conexion = new Connection();

            /// <summary>
            /// Actualización de una dirección.
            /// </summary>
            /// <param name="direccion">objeto dirección a actualizar.</param>
            /// <returns>true si se realizo correctamente, false en caso contrario.</returns>
            protected bool InternalUpdate(Direccion direccion)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Direccion_Update";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidDireccion", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidDireccion"].Value = direccion._UidDireccion;

                    comando.Parameters.Add("@UidPais", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidPais"].Value = direccion._UidPais;

                    comando.Parameters.Add("@UidEstado", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEstado"].Value = direccion._UidEstado;

                    comando.Parameters.Add("@VchMunicipio", SqlDbType.NVarChar, 30);
                    comando.Parameters["@VchMunicipio"].Value = direccion._StrMunicipio;

                    comando.Parameters.Add("@VchCiudad", SqlDbType.NVarChar, 30);
                    comando.Parameters["@VchCiudad"].Value = direccion._StrCiudad;

                    comando.Parameters.Add("@VchColonia", SqlDbType.NVarChar, 30);
                    comando.Parameters["@VchColonia"].Value = direccion._StrColonia;

                    comando.Parameters.Add("@VchCalle", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchCalle"].Value = direccion._StrCalle;

                    comando.Parameters.Add("@VchConCalle", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchConCalle"].Value = direccion._StrConCalle;

                    comando.Parameters.Add("@VchYCalle", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchYCalle"].Value = direccion._StrYCalle;

                    comando.Parameters.Add("@VchNoExt", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchNoExt"].Value = direccion._StrNoExt;

                    comando.Parameters.Add("@VchNoInt", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchNoInt"].Value = direccion._StrNoInt;

                    comando.Parameters.Add("@VchReferencia", SqlDbType.NVarChar, 020);
                    comando.Parameters["@VchReferencia"].Value = direccion._StrReferencia;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot Update a Direccion", e);
                }

            }

            /// <summary>
            /// Guarda una dirección en la base de datos. En este caso solo actualiza.
            /// </summary>
            /// <param name="direccion">objeto dirección a actualizar.</param>
            /// <returns>true si se guardo correctamente, false en caso contrario.</returns>
            public bool Save(Direccion direccion)
            {
                if (direccion._IsUserCreated)
                    throw new DatabaseException("Cannot create a Direccion using this Repository");

                return InternalUpdate(direccion);
            }

            /// <summary>
            /// Obtiene una dirección a partir de su identificador único.
            /// </summary>
            /// <param name="uid">Identificador único.</param>
            /// <returns>objeto dirección.</returns>
            public Direccion Find(Guid uid)
            {
                DataTable table = new DataTable();
                Direccion direccion = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Direccion_Find";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidDireccion", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidDireccion"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        direccion = new Direccion()
                        {
                            _UidDireccion = new Guid(row["UidDireccion"].ToString()),
                            _UidPais = new Guid(row["UidPais"].ToString()),
                            _UidEstado = new Guid(row["UidEstado"].ToString()),
                            _StrMunicipio = row["VchMunicipio"].ToString(),
                            _StrCiudad = row["VchCiudad"].ToString(),
                            _StrColonia = row["VchColonia"].ToString(),
                            _StrCalle = row["VchCalle"].ToString(),
                            _StrConCalle = row["VchConCalle"].ToString(),
                            _StrYCalle = row["VchYCalle"].ToString(),
                            _StrNoExt = row["VchNoExt"].ToString(),
                            _StrNoInt = row["VchNoInt"].ToString(),
                            _StrReferencia = row["VchReferencia"].ToString(),
                            _IsUserCreated = false,
                        };
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return direccion;
            }
        }
    }
}
