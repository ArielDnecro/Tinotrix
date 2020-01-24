using CodorniX.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.VistaDelModelo
{
    public class VMHistoricoVentas
    {
        #region Propiedades
        private HTurno.Repository HTurnoRepository = new HTurno.Repository();
        private HImpresiones.Repository HImpresionRepository = new HImpresiones.Repository();
        private List<HTurno> _LHTurnos;
        public List<HTurno> LHTurnos
        {
            get { return _LHTurnos; }
            set { _LHTurnos = value; }
        }

        private HTurno _HTurno;
        public HTurno HTurno
        {
            get { return _HTurno; }
            set { _HTurno = value; }
        }
        #endregion Propiedades
        #region Funciones
        public void BuscarHTurno(string strUidEmpresa, string strUidSucursal, string strUidEncargado
        ,string strOpNoFolRaMe,string strBusNoFolRaMe, string strOpNoFolRaMa,string strBusNoFolRaMa
        ,string strOpCaFotRaMe, string strBusCaFotRaMe, string strOpCaFotRaMa, string strBusCaFotRaMa
        ,string strOpImpRaMe, string strBusImpRaMe, string strOpImpRaMa, string strBusImpRaMa
        ,string strOpFeApRaMe, string strBusFeApRaMe, string strOpFeApRaMa, string strBusFeApRaMa
        , string strOpFeCiRaMe, string strBusFeCiRaMe, string strOpFeCiRaMa, string strBusFeCiRaMa)
        {
            //FORMAT(t.DtFhEntrada, 'dd/MM/yyyy') as DtApertura ,FORMAT(t.DtFhSalida, 'dd/MM/yyyy') as DtCierre
            string HQueryBase = "select"+
            " s.VchNombre as VchSucursal,s.UidSucursal as UidSucursal"+
            " ,u.VchUsuario as VchEncargado, u.UidUsuario as UidEncargado"+
            " , t.UidFolio as UidTurno" +
            " ,t.IntNumFolio as IntFolio"+
            " ,t.IntTotalFotos as IntNoFotos"+
            " ,t.IntTotalCosto as IntImporte"+
            " ,t.DtFhEntrada as DtApertura"+
            " ,t.DtFhSalida as DtCierre"+
            "  from _Turno as t"+
           " inner join Usuario as u on u.UidUsuario = t.UidUsuario"+
           " inner join UsuarioPerfilSucursal as ups on ups.UidUsuario = u.UidUsuario"+
           " inner join Sucursal as s on s.UidSucursal = ups.UidSucursal"+
           " where t.DtFhSalida is not null " +
           " and s.UidEmpresa = "+ "'"+strUidEmpresa + "'";
            //List<string> StrListCondiciones = new List<string>();
            if (strUidSucursal != "00000000-0000-0000-0000-000000000000")
            {
                HQueryBase = HQueryBase + " and s.UidSucursal = " + "'" + strUidSucursal + "'";
            }
            if (strUidEncargado!= "00000000-0000-0000-0000-000000000000")
            {
                HQueryBase = HQueryBase + " and u.UidUsuario = " + "'" + strUidEncargado + "'";
            }
            if(strBusNoFolRaMe != string.Empty)
            {
                HQueryBase = HQueryBase + " and t.IntNumFolio " + strOpNoFolRaMe + " '" + strBusNoFolRaMe + "'";
            }
            if(strBusNoFolRaMa != string.Empty)
            {
                HQueryBase = HQueryBase + " and t.IntNumFolio " + strOpNoFolRaMa + " '" + strBusNoFolRaMa + "'";
            }
            if (strBusCaFotRaMe != string.Empty)
            {
                HQueryBase = HQueryBase + " and t.IntTotalFotos " + strOpCaFotRaMe + " '" + strBusCaFotRaMe + "'";
            }
            if (strBusCaFotRaMa != string.Empty)
            {
                HQueryBase = HQueryBase + " and t.IntTotalFotos " + strOpCaFotRaMa + " '" + strBusCaFotRaMa + "'";
            }
            if (strBusImpRaMe != string.Empty)
            {
                HQueryBase = HQueryBase + " and t.IntTotalCosto " + strOpImpRaMe + " '" + strBusImpRaMe + "'";
            }
            if (strBusImpRaMa != string.Empty)
            {
                HQueryBase = HQueryBase + " and t.IntTotalCosto " + strOpImpRaMa + " '" + strBusImpRaMa + "'";
            }
            if (strBusFeApRaMe != string.Empty)
            {
                DateTime DtBusMinFeAP= DateTime.ParseExact(strBusFeApRaMe, "dd/MM/yyyy", null);
                HQueryBase = HQueryBase + " and t.DtFhEntrada " + strOpFeApRaMe + " '" + DtBusMinFeAP.ToString("yyyy/MM/dd") + "'";
            }
            if (strBusFeApRaMa != string.Empty)
            {
                DateTime DtBusMaxFeAP = DateTime.ParseExact(strBusFeApRaMa, "dd/MM/yyyy", null);
                HQueryBase = HQueryBase + " and t.DtFhEntrada " + strOpFeApRaMa + " '" + DtBusMaxFeAP.ToString("yyyy/MM/dd") + "'";
            }

            if (strBusFeCiRaMe != string.Empty)
            {
                DateTime DtBusMinFeCI = DateTime.ParseExact(strBusFeCiRaMe, "dd/MM/yyyy", null);
                HQueryBase = HQueryBase + " and t.DtFhSalida " + strOpFeCiRaMe + " '" + DtBusMinFeCI.ToString("yyyy/MM/dd") + "'";
            }
            if (strBusFeCiRaMa != string.Empty)
            {
                DateTime DtBusMaxFeCI = DateTime.ParseExact(strBusFeCiRaMa, "dd/MM/yyyy", null);
                HQueryBase = HQueryBase + " and t.DtFhSalida " + strOpFeCiRaMa + " '" + DtBusMaxFeCI.ToString("yyyy/MM/dd") + "'";
            }
            //try
            //{
                _LHTurnos = HTurnoRepository.FindHTurnos(HQueryBase);

            //}
            //catch (Exception e)
            //{
            //    throw new VMHistoricoVentasException("(VM: No se pudo cargar la lista de HISTORICO )\n \n " + e.Message);
            //}
        }

        public void BuscarHImpresion(Guid uidturno) {
            if (_HTurno!= null)
            {
                _HTurno.LHImpresiones = HImpresionRepository.CargarVentas(uidturno);
            }
        }
        #endregion Funciones
        #region Excepciones

        public class VMHistoricoVentasException : Exception
        {
            public VMHistoricoVentasException(string mensaje) : base("(VMHistoricoVentasException) \n \n" + mensaje) { }
        }

        #endregion Excepciones
    }
}