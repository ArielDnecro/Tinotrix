using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CodorniX.ConexionDB;
using CodorniX.Util;

namespace CodorniX.Modelo
{
    [Serializable]
    public class IniciarTurno
    {
        [NonSerialized]
        Conexion conexion = new Conexion();
        private Guid _UidInicioTurno;

        public Guid UidInicioTurno
        {
            get { return _UidInicioTurno; }
            set { _UidInicioTurno = value; }
        }

        private Guid _UidUsuario;

        public Guid UidUsuario
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; }
        }

        private DateTime _DtFechaHoraInicio;

        public DateTime DtFechaHoraInicio
        {
            get { return _DtFechaHoraInicio; }
            set { _DtFechaHoraInicio = value; }
        }

        private DateTime? _DtFechaHoraFin;

        public DateTime? DtFechaHoraFin
        {
            get { return _DtFechaHoraFin; }
            set { _DtFechaHoraFin = value; }
        }

        private int? _IntNoCompleto;

        public int? IntNoCompleto
        {
            get { return _IntNoCompleto; }
            set { _IntNoCompleto = value; }
        }

        private Guid _UidDepartamento;

        public Guid UidDepartamento
        {
            get { return _UidDepartamento; }
            set { _UidDepartamento = value; }
        }

        private Guid? _UidPeriodo;

        public Guid? UidPeriodo
        {
            get { return _UidPeriodo; }
            set { _UidPeriodo = value; }
        }

        private Guid _UidTurno;

        public Guid UidTurno
        {
            get { return _UidTurno; }
            set { _UidTurno = value; }
        }


        public bool GuardarDatos()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            try
            {
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.CommandText = "usp_IniciarTurno_Add";

                Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUsuario"].Value = UidUsuario;

                Comando.Parameters.Add("@DtFechaHoraInicio", SqlDbType.DateTime);
                Comando.Parameters["@DtFechaHoraInicio"].Value = DtFechaHoraInicio;

                Comando.Parameters.Add("@UidPeriodo", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPeriodo"].Value = UidPeriodo;

                Comando.Parameters.Add("@UidInicioTurno", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidInicioTurno"].Direction = ParameterDirection.Output;

                Resultado = conexion.ManipilacionDeDatos(Comando);
                _UidInicioTurno = (Guid)Comando.Parameters["@UidInicioTurno"].Value;
                Comando.Dispose();




            }
            catch (Exception)
            {
                throw;
            }
            return Resultado;
        }

        public bool ModificarDatos()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = "usp_ModificarInicioTurno";

            Comando.Parameters.Add("@UidInicioTurno", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidInicioTurno"].Value = _UidInicioTurno;

            Comando.Parameters.Add("@DtFechaHoraFin", SqlDbType.DateTime);
            Comando.Parameters["@DtFechaHoraFin"].Value = DtFechaHoraFin;


            Comando.Parameters.Add("@IntNoCumplido", SqlDbType.Int);
            Comando.Parameters["@IntNoCumplido"].Value = IntNoCompleto;

            Resultado = conexion.ManipilacionDeDatos(Comando);

            return Resultado;
        }

        public class Repositorio
        {
            Conexion conexion = new Conexion();
            public IniciarTurno ObtenerInicioTurno(Guid uid)
            {
                IniciarTurno inicioturno = null;
                DataTable table = null;

                SqlCommand command = new SqlCommand();
                command.CommandText = "usp_ObtenerInicioTurno";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidUsuario"].Value = uid;
                table = conexion.Busquedas(command);

                foreach (DataRow row in table.Rows)
                {
                    inicioturno = new IniciarTurno()
                    {
                        _UidInicioTurno = new Guid(row["UidInicioTurno"].ToString()),
                        _DtFechaHoraInicio = Convert.ToDateTime(row["DtFechaHoraInicio"].ToString()),
                        _UidUsuario = new Guid(row["UidUsuario"].ToString()),
                        _DtFechaHoraFin = row.IsNull("DtFechaHoraFin") ? (DateTime?)null : Convert.ToDateTime (row["DtFechaHoraFin"].ToString()),
                        _IntNoCompleto = row.IsNull("IntNoCompletado") ? (int?)null : Convert.ToInt32(row["IntNoCompletado"].ToString()),
                        _UidPeriodo = new Guid(row["UidPeriodo"].ToString()),
                    };
                }

                return inicioturno;
            }
            public IniciarTurno ObtenerInicioPorPeriodo(Guid uid, Guid periodo, DateTime fecha)
            {
                IniciarTurno inicioturno = null;
                DataTable table = null;

                SqlCommand command = new SqlCommand();
                command.CommandText = "usp_InicioTurnoPorPeriodo";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidUsuario"].Value = uid;

                command.Parameters.Add("@UidPeriodo", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidPeriodo"].Value = periodo;
                

                command.Parameters.Add("@DtFecha", SqlDbType.Date);
                command.Parameters["@DtFecha"].Value = fecha;
                table = conexion.Busquedas(command);

                foreach (DataRow row in table.Rows)
                {
                    inicioturno = new IniciarTurno()
                    {
                        _UidInicioTurno = new Guid(row["UidInicioTurno"].ToString()),
                        _DtFechaHoraInicio = Convert.ToDateTime(row["DtFechaHoraInicio"].ToString()),
                        _UidUsuario = new Guid(row["UidUsuario"].ToString()),
                        _DtFechaHoraFin = row.IsNull("DtFechaHoraFin") ? (DateTime?)null : Convert.ToDateTime(row["DtFechaHoraFin"].ToString()),
                        _IntNoCompleto = row.IsNull("IntNoCompletado") ? (int?)null : Convert.ToInt32(row["IntNoCompletado"].ToString()),
                        _UidPeriodo = new Guid(row["UidPeriodo"].ToString()),
                    };
                }

                return inicioturno;
            }
            public IniciarTurno ObtenerHora(DateTime fecha, Guid UidUsuario, Guid uidperiodo)
            {
                IniciarTurno inicioturno = null;
                DataTable table = null;

                SqlCommand command = new SqlCommand();
                command.CommandText = "usp_ConsultarInicioTurno";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidUsuario"].Value = UidUsuario;

                command.Parameters.Add("@UidPeriodo", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidPeriodo"].Value = uidperiodo;

                command.Parameters.Add("@DtFecha", SqlDbType.Date);
                command.Parameters["@DtFecha"].Value = fecha;

                table = conexion.Busquedas(command);

                foreach (DataRow row in table.Rows)
                {
                    inicioturno = new IniciarTurno()
                    {
                        _UidInicioTurno = new Guid(row["UidInicioTurno"].ToString()),
                        _DtFechaHoraInicio = Convert.ToDateTime(row["DtFechaHoraInicio"].ToString()),
                        _UidUsuario = new Guid(row["UidUsuario"].ToString()),
                        _DtFechaHoraFin = row.IsNull("DtFechaHoraFin") ? (DateTime?)null :  Convert.ToDateTime(row["DtFechaHoraFin"].ToString()),
                        _IntNoCompleto = row.IsNull("IntNoCompletado") ? (int?)null : Convert.ToInt32(row["IntNoCompletado"].ToString()),
                        _UidPeriodo = row.IsNull("UidPeriodo")?(Guid?)null: new Guid(row["UidPeriodo"].ToString()),
                    };
                }

                return inicioturno;
            }
            public IniciarTurno ObtenerTurnoUsuario(Guid uid, Guid UidTurno, DateTime fecha)
            {
                IniciarTurno inicioturno = null;
                DataTable table = null;

                SqlCommand command = new SqlCommand();
                command.CommandText = "usp_ObtenerTurno_Usuario";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidUsuario"].Value = uid;

                command.Parameters.Add("@UidTurno", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidTurno"].Value = UidTurno;
                command.Parameters.Add("@DtFecha", SqlDbType.Date);
                command.Parameters["@DtFecha"].Value = fecha;


                table = conexion.Busquedas(command);

                foreach (DataRow row in table.Rows)
                {
                    inicioturno = new IniciarTurno()
                    {
                        _UidInicioTurno = new Guid(row["UidInicioTurno"].ToString()),
                        _DtFechaHoraInicio = Convert.ToDateTime(row["DtFechaHoraInicio"].ToString()),
                        _UidUsuario = new Guid(row["UidUsuario"].ToString()),
                        _DtFechaHoraFin = row.IsNull("DtFechaHoraFin") ? (DateTime?)null : Convert.ToDateTime(row["DtFechaHoraFin"].ToString()),
                        _IntNoCompleto = row.IsNull("IntNoCompletado") ? (int?)null : Convert.ToInt32(row["IntNoCompletado"].ToString()),
                        _UidPeriodo = new Guid(row["UidPeriodo"].ToString()),
                        _UidTurno = new Guid(row["UidTurno"].ToString())
                    };
                }

                return inicioturno;
            }
        }

    }
}
