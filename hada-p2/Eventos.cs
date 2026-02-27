using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Eventos
    {

        //Eventos de la practica
        public class TocadoArgs
        {
            private string nombre;
            private Coordenada coordenadaImpacto;
            public TocadoArgs(string s, Coordenada c)
            {
                
                this.nombre = s;
                this.coordenadaImpacto = new Coordenada(c);
            }
        }

        public class HundidoArgs
        {
            private string nombre;

            public HundidoArgs(string s)
            {
                this.nombre = s;
            }

        }
    }
}
