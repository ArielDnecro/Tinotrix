using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TinoTriXxX.Modelo
{
    [Serializable]
    public class TicketCliente
    {
        #region propiedades
        protected string _StrEnc1Linea;
        public string StrEnc1Linea
        {
            get { return _StrEnc1Linea; }
            set { _StrEnc1Linea = value; }
        }
        protected string _StrEnc2Linea;
        public string StrEnc2Linea
        {
            get { return _StrEnc2Linea; }
            set { _StrEnc2Linea = value; }
        }
        protected string _StrEnc3Linea;
        public string StrEnc3Linea
        {
            get { return _StrEnc3Linea; }
            set { _StrEnc3Linea = value; }
        }
        protected string _StrEnc4Linea;
        public string StrEnc4Linea
        {
            get { return _StrEnc4Linea; }
            set { _StrEnc4Linea = value; }
        }
        protected string _StrEnc5Linea;
        public string StrEnc5Linea
        {
            get { return _StrEnc5Linea; }
            set { _StrEnc5Linea = value; }
        }
        protected string _StrPie1Linea;
        public string StrPie1Linea
        {
            get { return _StrPie1Linea; }
            set { _StrPie1Linea = value; }
        }
        protected string _StrPie2Linea;
        public string StrPie2Linea
        {
            get { return _StrPie2Linea; }
            set { _StrPie2Linea = value; }
        }
        protected string _StrPie3Linea;
        public string StrPie3Linea
        {
            get { return _StrPie3Linea; }
            set { _StrPie3Linea = value; }
        }

        #endregion propiedades
        public new class Repository
        {
            #region funciones
            public TicketCliente FindEncPie()
            {
                TicketCliente ticket = new TicketCliente();
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("TinotrixServer");
                    var Data = (byte[])key.GetValue("Conf-TC-EP", null);
                    key.Close();
                    byte[] bytes = new byte[0];
                    if (Data != null)
                    {
                        bytes = Data;
                        //Convertir bytes to object list
                        BinaryFormatter bf = new BinaryFormatter();
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            ticket = (TicketCliente)bf.Deserialize(ms);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new TicketClienteException("(Obtener configuracion de encabezado pie de pagina de ticket cliente local)" + e.Message);
                }

                return ticket;
            }


            public bool ActualizarEncPie(TicketCliente Tiketito)
            {
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        var formatter = new BinaryFormatter();//si quieres guardar solo texto, esto no es necesario.
                        formatter.Serialize(ms, Tiketito);//si quieres guardar solo texto, esto no es necesario.
                        var data = ms.ToArray();//si quieres guardar solo texto, esto no es necesario.
                        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("TinotrixServer", true);
                        registryKey.SetValue("Conf-TC-EP", data, RegistryValueKind.Binary);//data-> "texto" ->sustituir data por el texto string que quieras guardar
                    }
                }
                catch (Exception e)
                {
                    throw new TicketClienteException("(Actualizar ticket cliente encabezado y pie de pagina local) " + e.Message);
                }
                return true;
            }
            #endregion funciones

            #region Excepcion
            public class TicketClienteException : Exception
            {
                public TicketClienteException(string mensaje) : base("(TicketClienteException):  " + mensaje) { }
            }
            #endregion Excepcion
        }
    }
}
