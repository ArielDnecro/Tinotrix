using CodorniX.ConexionDB;
using CodorniX.Modelo;
using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoTriXxX.ConexionBaseDatos;

namespace TinoTriXxX.Modelo
{
    public class Turno
    {
        #region Propiedades
        protected Guid _UidUsuario;
        public Guid UidUsusario
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; }
        }

        protected Guid _UidFolio;
        public Guid UidFolio
        {
            get { return _UidFolio; }
            set { _UidFolio = value; }
        }

        protected int _IntNoFolio;
        public int IntNoFolio
        {
            get { return _IntNoFolio; }
            set { _IntNoFolio = value; }
        }

        protected DateTime _DtHrInicio;
        public DateTime DtHrInicio
        {
            get { return _DtHrInicio; }
            set { _DtHrInicio = value; }
        }

        protected DateTime _DtFhInicio;
        public DateTime DtFhInicio
        {
            get { return _DtFhInicio; }
            set { _DtFhInicio = value; }
        }

        protected int _IntTFotos;
        public int IntTFotos
        {
            get { return _IntTFotos; }
            set { _IntTFotos = value; }
        }

        protected int _IntTCosto;
        public int IntTCosto
        {
            get { return _IntTCosto; }
            set { _IntTCosto = value; }
        }
#endregion propiedades
        public new class Repository
        {
            //protected Konection _Conexion = new Konection();
            //public Turno Find()
            //{
            //    DataTable table = new DataTable();
            //    Turno Turno = new Turno();
            //    try
            //    {
            //        SqlCommand comando = new SqlCommand();
            //        comando.CommandType = CommandType.StoredProcedure;
            //        comando.CommandText = "Wpf_VerificarExistenciaEncargado";
            //        table = _Conexion.ExecuteQuery(comando);
            //        if (int.Parse(table.Rows[0]["IntNoEncargados"].ToString()) == 1)
            //        {
            //            comando.CommandText = "Wpf_Encargado_Find";
            //            table = _Conexion.ExecuteQuery(comando);
            //            Turno._UidUsuario = new Guid(table.Rows[0]["UidEncargado"].ToString());
            //            comando.CommandText = "Wpf_Turno_Find";
            //            table = _Conexion.ExecuteQuery(comando);
            //            Turno._IntNoFolio = int.Parse(table.Rows[0]["IntNoFolio"].ToString());
            //            Turno._UidFolio = new Guid(table.Rows[0]["UidFolio"].ToString());
            //            Turno._DtHrInicio = DateTime.Parse(table.Rows[0]["DtHrEntrada"].ToString());
            //            Turno._DtFhInicio = DateTime.Parse(table.Rows[0]["DtFhEntrada"].ToString());
            //            Turno._IntTFotos = int.Parse(table.Rows[0]["IntTFotos"].ToString());
            //            Turno._IntTCosto = int.Parse(table.Rows[0]["IntTCosto"].ToString());
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        throw new UsuarioLocalException("(Obtener El usuario local)" + e.Message);
            //    }

            //    return Turno;
            //}
            //public bool RevocarEncargado()
            //{
            //    try
            //    {
            //        SqlCommand comando = new SqlCommand();
            //        comando.CommandType = CommandType.StoredProcedure;
            //        comando.CommandText = "Wpf_RevocarEncargado";
            //        return _Conexion.ExecuteCommand(comando);
                   
            //    }
            //    catch (Exception e)
            //    {
            //        throw new UsuarioLocalException("(Revocar Encargado local) " + e.Message);
            //    }


            //}
            //public bool Revocar()
            //{
            //    try
            //    {
            //        SqlCommand comando = new SqlCommand();
            //        comando.CommandType = CommandType.StoredProcedure;
            //        comando.CommandText = "Wpf_CerrarTurno";
            //        return _Conexion.ExecuteCommand(comando);
            //    }
            //    catch (Exception e)
            //    {
            //        throw new UsuarioLocalException("(Cerrar Turno local) " + e.Message);
            //    }


            //}
            //public bool ActualizarUsuario(Guid UsuarioNuevo)
            //{
            //    try
            //    {
            //        SqlCommand comando = new SqlCommand();
            //        comando.CommandType = CommandType.StoredProcedure;
            //        comando.CommandText = "Wpf_ActualizarEncargado";
            //        comando.Parameters.Add("@UidEncargado", SqlDbType.UniqueIdentifier);
            //        comando.Parameters["@UidEncargado"].Value = UsuarioNuevo;
            //        return _Conexion.ExecuteCommand(comando);
            //    }
            //    catch (Exception e)
            //    {
            //        throw new UsuarioLocalException("(Actualizar el usuario local) " + e.Message);
            //    }

            //}
            //public bool IniciarTurnoLocal( Guid uidfolio, int nofolio, String hrentrada, String fhentrada) {
            //    try {
            //        SqlCommand comando = new SqlCommand();
            //        comando.CommandType = CommandType.StoredProcedure;
            //        comando.CommandText = "Wpf_IniciarTurno";
            //        comando.AddParameter("@IntNoFolio", nofolio, SqlDbType.Int);
            //        comando.AddParameter("@UidFolio", uidfolio, SqlDbType.UniqueIdentifier);
            //        comando.AddParameter("@DtHrEntrada", hrentrada, SqlDbType.Time);
            //        comando.AddParameter("@DtFhEntrada", fhentrada, SqlDbType.Date);
            //        comando.AddParameter("@IntTFotos", 0, SqlDbType.Int);
            //        comando.AddParameter("@IntTCosto", 0, SqlDbType.Int);
            //        return _Conexion.ExecuteCommand(comando);
            //    }
            //    catch (Exception e) { throw new UsuarioLocalException("(IniciarTurnoLocal) " + e.Message); }
            //}


            //Conexion Conexionhost = new Conexion();
            //public int IniciarTurnoHost(Guid uidsucursal, Guid uidfolio, Guid uidusuario, String hrentrada, String fhentrada) {
            //    Turno turno = null;
            //    SqlCommand command = new SqlCommand();

            //    command.CommandText = "Wpf_IniciarTurno_Server";
            //    command.CommandType = CommandType.StoredProcedure;

            //    command.AddParameter("@UidSucursal", uidsucursal, SqlDbType.UniqueIdentifier);
            //    command.AddParameter("@UidFolio", uidfolio, SqlDbType.UniqueIdentifier);
            //    command.AddParameter("@UidUsuario", uidusuario, SqlDbType.UniqueIdentifier);
            //    command.AddParameter("@DtHrEntrada", hrentrada, SqlDbType.Time);
            //    command.AddParameter("@DtFhEntrada", fhentrada, SqlDbType.Date);

            //    DataTable table = new Connection().ExecuteQuery(command);

            //    foreach (DataRow row in table.Rows)
            //    {
            //        turno = new Turno()
            //        {
            //            _IntNoFolio = (int)row["IntNumFolio"]
            //        };
            //    }

            //    return turno.IntNoFolio;
            //}
            //public bool RevocarHost(Guid UidTurno,  String HrSalida, String FhSalida, int TFotos, int TCosto)
            //{
            //    try
            //    {
            //        bool Resultado = false;
            //        SqlCommand command = new SqlCommand();

            //        command.CommandText = "Wpf_CerrarTurno_Server";
            //        command.CommandType = CommandType.StoredProcedure;

            //        command.AddParameter("@UidTurno", UidTurno, SqlDbType.UniqueIdentifier);
            //        command.AddParameter("@IntTFotos", TFotos, SqlDbType.Int);
            //        command.AddParameter("@IntTCosto", TCosto, SqlDbType.Int);
            //        command.AddParameter("@DtHrSalida", HrSalida, SqlDbType.Time);
            //        command.AddParameter("@DtFhSalida", FhSalida, SqlDbType.Date);

            //        Resultado = Conexionhost.ManipilacionDeDatos(command);

            //        return Resultado;
            //    }
            //    catch (Exception e)
            //    {
            //        throw new UsuarioLocalException("(Revocar Encargado Host) " + e.Message);
            //    }
            //}

            public bool TurnoServidorAbierto(Guid Sucursal)
            {
                try
                {
                    bool Resultado = false;
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "Wpf_TurnoServidorAbierto";
                    command.CommandType = CommandType.StoredProcedure;
                    command.AddParameter("@UidSucursal", Sucursal, SqlDbType.UniqueIdentifier);

                    DataTable table = new Connection().ExecuteQuery(command);
                    if (table.Rows.Count==1) {
                        Resultado = Convert.ToBoolean( table.Rows[0]["IsBTech"]);
                    }
                    //Resultado = Conexionhost.ManipilacionDeDatos(command);
                    return Resultado;
                }
                catch (Exception e)
                {
                    throw new UsuarioLocalException("(TurnoServidorAbierto) " + e.Message);
                }
            }

            #region Excepciones
            public class UsuarioLocalException : Exception
            {
                public UsuarioLocalException(string mensaje) : base("(UsuarioLocalException):  " + mensaje) { }
            }
            #endregion Excepciones
        }
    }
}
