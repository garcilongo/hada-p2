using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {
        private int _tamTablero;
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private Dictionary<Coordenada, string> casillasTablero;

        public event EventHandler<EventArgs> eventoFinPartida;

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

        private void InicializaCasillasTablero()
        {
            for (int fila = 0; fila < this.TamTablero; fila++)
            {

                for (int columna = 0; columna < this.TamTablero; columna++)
                {
                    Coordenada aux = new Coordenada(fila, columna);

                    String valor = "AGUA";

                    for (int i = 0; i < this.barcos.Count; i++)
                    {
                        if (this.barcos[i].CoordenadasBarco.ContainsKey(aux))
                        {
                            valor = this.barcos[i].Nombre;
                            break;
                        }
                    }

                    this.casillasTablero.Add(aux, valor);
                }
            }
        }

        private void CuandoEventoTocado(TocadoArgs t)
        {
            this.coordenadasTocadas.Add(t.CoordenadaImpacto);

            Console.WriteLine($"TABLERO: Barco {t.Nombre} tocado en Coordenada: [{t.CoordenadaImpacto}] ");
        }

        private void CuandoEventoHundido(HundidoArgs h)
        {
            Console.WriteLine($"TABLERO: Barco {h.Nombre} hundido!! ");


        }

        public Tablero(int tamtablero, List<Barco> barcos)
        {
            this._tamTablero = tamtablero;
            this.barcos = new List<Barco>();
            this.casillasTablero = new Dictionary<Coordenada, string>();
            this.coordenadasDisparadas = new List<Coordenada>();
            this.coordenadasTocadas = new List<Coordenada>();


            for (int i = 0; i < barcos.Count; i++)
            {
                this.barcos.Add(barcos[i]);
            }
        }

        public void Disparar(Coordenada c)
        {
           
        }

        public string DibujarTablero()
        {
            String tablero = "";

            for (int i = 0; i < this.TamTablero; i++)
            {
                for (int j = 0; j < this.TamTablero; j++)
                {
                    tablero = tablero + $"[{casillasTablero[new Coordenada(i, j)]}]";
                }

                tablero = tablero + "\n";
            }

            return tablero;
        }

        public string ToString()
        {

        }

    }
}
