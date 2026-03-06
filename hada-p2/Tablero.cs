using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hada {
    public class Tablero {
        private int _tamTablero;
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcosEliminados;
        private List<Barco> barcos;
        private Dictionary<Coordenada, string> casillasTablero;

        public event EventHandler<EventArgs> eventoFinPartida;

        public int TamTablero {
            get {
                return _tamTablero;
            }

            set {
                if (value < 4 || value > 9) {
                    throw new ArgumentOutOfRangeException();
                }

                _tamTablero = value;
            }


        }

        private void InicializaCasillasTablero() {
            for (int fila = 0; fila < this.TamTablero; fila++) {

                for (int columna = 0; columna < this.TamTablero; columna++) {
                    Coordenada aux = new Coordenada(fila, columna);

                    String valor = "AGUA";

                    for (int i = 0; i < this.barcos.Count; i++) {
                        if (this.barcos[i].CoordenadasBarco.ContainsKey(aux)) {
                            valor = this.barcos[i].Nombre;
                            break;
                        }
                    }

                    this.casillasTablero.Add(aux, valor);
                }
            }
        }

        private void CuandoEventoTocado(object sender, TocadoArgs e) {
            // 1. Añadimos a la lista de coordenadas que han acertado (sin repetir)
            if (!coordenadasTocadas.Contains(e.CoordenadaImpacto)) {
                coordenadasTocadas.Add(e.CoordenadaImpacto);
            }

            // 2. Actualizamos la etiqueta en el diccionario del tablero
            casillasTablero[e.CoordenadaImpacto] = e.Nombre + "_T";

            // 3. Mensaje por pantalla
            Console.WriteLine($"TABLERO: Barco {e.Nombre} tocado en Coordenada: [{e.CoordenadaImpacto.ToString()}]");
        }

        private void CuandoEventoHundido(object sender, HundidoArgs e) {
            // Mensaje de hundimiento
            Console.WriteLine($"TABLERO: Barco {e.Nombre} hundido!!");

            // Añadimos a la lista de barcos eliminados (si no está ya)
            Barco barcoHundido = (Barco)sender;
            if (!barcosEliminados.Contains(barcoHundido)) {
                barcosEliminados.Add(barcoHundido);
            }

            // Comprobamos si todos los barcos están hundidos
            if (barcosEliminados.Count == barcos.Count) {
                if (eventoFinPartida != null) {
                    eventoFinPartida(this, EventArgs.Empty);
                }
            }
        }

        public Tablero(int tamtablero, List<Barco> barcos)
        {
            this._tamTablero = tamtablero;
            this.barcos = new List<Barco>();
            this.casillasTablero = new Dictionary<Coordenada, string>();
            this.barcosEliminados = new List<Barco>();
            this.coordenadasDisparadas = new List<Coordenada>();
            this.coordenadasTocadas = new List<Coordenada>();

            foreach (Barco b in barcos) {
                this.barcos.Add(b);
                b.eventoTocado += CuandoEventoTocado;
                b.eventoHundido += CuandoEventoHundido;
            }

            // Inicializar casillas
            InicializaCasillasTablero();
        }

        public void Disparar(Coordenada c) {
            // Comprobamos si está fuera de los límites del tablero actual
            if (c.Fila >= TamTablero || c.Columna >= TamTablero || c.Fila < 0 || c.Columna < 0) {
                Console.WriteLine($"La coordenada {c.ToString()} está fuera de las dimensiones del tablero.");
            } else {
                // Añadimos a la lista de disparos realizados
                coordenadasDisparadas.Add(c);

                // Notificamos a cada barco del disparo
                foreach (Barco b in barcos) {
                    b.Disparo(c);
                }
            }
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

        public override string ToString() {
            string cadena = "";

            // 1. Información de cada uno de los Barcos 
            foreach (Barco b in barcos) {
                cadena += b.ToString() + "\n";
            }

            // 2. Lista de 'Coordenadas Disparadas' 
            cadena += "\nCoordenadas disparadas: ";
            foreach (Coordenada c in coordenadasDisparadas) {
                cadena += c.ToString() + " ";
            }

            // 3. Lista de 'Coordenadas Tocadas' 
            cadena += "\nCoordenadas tocadas: ";
            foreach (Coordenada c in coordenadasTocadas) {
                cadena += c.ToString() + " ";
            }

            // 4. El tablero 
            cadena += "\n\nCASILLAS TABLERO\n----------\n";
            cadena += DibujarTablero();

            return cadena;
        }

    }
}
