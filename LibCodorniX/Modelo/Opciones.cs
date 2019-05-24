using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodorniX.Modelo
{
    [Serializable]
    public class Opciones
    {
        protected bool _ExistsInDatabase;

        private Guid _UidOpciones;

        public Guid UidOpciones
        {
            get { return _UidOpciones; }
            set { _UidOpciones = value; }
        }

        private string _StrOpcion;

        public string StrOpciones
        {
            get { return _StrOpcion; }
            set { _StrOpcion = value; }
        }

    }
}
