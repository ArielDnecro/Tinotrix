using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Citas.modelo
{
    public class cita
    {
        #region propiedades
        private bool _ExistsInDatabase;

        private int _intcita;
        public int intcita
        {
            get { return _intcita; }
            set { _intcita = value; }
        }


        private string _VchDes;
        public string VchDes
        {
            get { return _VchDes; }
            set { _VchDes = value; }
        }

        private int _intidusuario;
        public int intidusuario
        {
            get { return _intidusuario; }
            set { _intidusuario = value; }
        }

        #endregion propiedades


    }
}