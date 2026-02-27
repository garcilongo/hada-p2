using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {
        private int _tamTablero;
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private Dictionary<Coordenada, string> casillasTablero;
        public int TamTablero
        {
            get
            {
                return _tamTablero;
            }

            set
            {
                if (value < 4 || value > 9)
                {
                    throw new ArgumentOutOfRangeException(); 
                }
                
                _tamTablero = value;
            }


        }

        public Tablero(int tamtablero, List<Barco> barcos)
        {
            this.tamTablero = tamtablero;
            this.barcos = new List<Barcos>();

            for (int i = 0; i < barcos.Count; i++)
            {
                this.barcos.Add(barcos[i]);
            }
        }
    }
}
