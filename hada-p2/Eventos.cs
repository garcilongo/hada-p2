using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{

    //Eventos de la practica
    public class TocadoArgs
    {
        private string _nombre;
        private Coordenada _coordenadaImpacto;

        public string Nombre
        {
            get
            {
                return this._nombre;
            }
        }

        public Coordenada CoordenadaImpacto
        {
            get
            {
                return this._coordenadaImpacto;
            }
        }

        public TocadoArgs(string s, Coordenada c)
        {
                
            this._nombre = s;
            this._coordenadaImpacto = new Coordenada(c);
        }

            
    }

    public class HundidoArgs
    {
        private string _nombre;

        public string Nombre
        {
            get
            {
                return this._nombre;
            }
        }

        public HundidoArgs(string s)
        {
            this._nombre = s;
        }

    }
}
