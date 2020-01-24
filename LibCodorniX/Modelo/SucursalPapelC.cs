using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodorniX.Modelo
{
    public class SucursalPapelC
    {
        #region Propiedades
        protected bool _ExistsInDatabase = false;

        protected Guid _UidPapel;
        public Guid UidPapel
        {
            get { return _UidPapel; }
            set { _UidPapel = value; }
        }

        protected string _StrDescripcion;
        public string StrDescripcion
        {
            get { return _StrDescripcion; }
            set { _StrDescripcion = value; }
        }

        protected string _VchAlto;
        public string VchAlto
        {
            get { return _VchAlto; }
            set { _VchAlto = value; }
        }


        protected string _VchAncho;
        public string VchAncho
        {
            get { return _VchAncho; }
            set { _VchAncho = value; }
        }

        protected string _VchSuperior;
        public string VchSuperior
        {
            get { return _VchSuperior; }
            set { _VchSuperior = value; }
        }


        protected string _VchInferior;
        public string VchInferior
        {
            get { return _VchInferior; }
            set { _VchInferior = value; }
        }

        protected string _VchDerecho;
        public string VchDerecho
        {
            get { return _VchDerecho; }
            set { _VchDerecho = value; }
        }


        protected string _VchIzquierdo;
        public string VchIzquierdo
        {
            get { return _VchIzquierdo; }
            set { _VchIzquierdo = value; }
        }

        #endregion Propiedades
        public new class Repository
        {
            protected Connection _Conexion = new Connection();

            public bool Save(SucursalPapelC Papel)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    if (Papel._ExistsInDatabase == true)
                    {
                        //return InternalUpdate(SucursalFoto);
                        comando.CommandText = "usp_SucursalPapelC_Update";
                        //comando.AddParameter("@UidPapel", Papel._UidPapel, SqlDbType.UniqueIdentifier);
                    }
                    else
                    {
                        Papel._ExistsInDatabase = true;
                        //return InternalSave(SucursalFoto);
                        comando.CommandText = "usp_SucursalPapelC_Add";
                    }
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.AddParameter("@UidPapel", Papel._UidPapel, SqlDbType.UniqueIdentifier);
                    comando.AddParameter("@VchDescripcion", Papel._StrDescripcion, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchAlto", Papel._VchAlto, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchAncho", Papel._VchAncho, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchMSuperior", Papel._VchSuperior, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchMInferior", Papel._VchInferior, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchMDerecho", Papel._VchDerecho, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchMIzquierdo", Papel._VchIzquierdo, SqlDbType.VarChar, 50);

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Objeto Papel: No pudo salvar el papel ", e);
                }
            }

            public SucursalPapelC Find(Guid uid)
            {
                DataTable table = new DataTable();
                SucursalPapelC Papel = new SucursalPapelC();
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                try
                {
                    comando.CommandText = "usp_SucursalPapelC_Find";
                    comando.Parameters.Add("@UidPapel", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidPapel"].Value = uid;
                    table = _Conexion.ExecuteQuery(comando);

                    if (int.Parse(table.Rows.Count.ToString()) == 1)
                    {
                        Papel._UidPapel = uid;
                        Papel._StrDescripcion = table.Rows[0]["VchDescripcion"].ToString();
                        Papel._VchAlto = table.Rows[0]["VchAlto"].ToString();
                        Papel._VchAncho = table.Rows[0]["VchAncho"].ToString();
                        Papel._VchSuperior = table.Rows[0]["VchMSuperior"].ToString();
                        Papel._VchInferior = table.Rows[0]["VchMInferior"].ToString();
                        Papel._VchDerecho = table.Rows[0]["VchMDerecho"].ToString();
                        Papel._VchIzquierdo = table.Rows[0]["VchMIzquierdo"].ToString();
                        Papel._ExistsInDatabase = true;
                        //obtener el total de maquinas
                    }
                    else
                    {
                        //if (int.Parse(table.Rows.Count.ToString()) == 0)
                        //{

                        //}
                        //Papel._UidPapel = Guid.Empty;
                        Papel._StrDescripcion = "";
                        Papel._VchAlto = "";
                        Papel._VchAncho = "";
                        Papel._VchSuperior = "";
                        Papel._VchInferior = "";
                        Papel._VchDerecho = "";
                        Papel._VchIzquierdo = "";
                        Papel._ExistsInDatabase = false;
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Objeto Papel: No pudo consultar el papel ", e);
                }

                return Papel;
            }
        }
    }
}
