using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Game {
        private bool finPartida;
        private Tablero tablero;

        public Game() {
            // Inicializar propiedad de control
            this.finPartida = false;
            
            // Inicializar bucle del juego
            GameLoop();
        }

        private void GameLoop() {
            // Crear lista de barcos (mínimo 3)
            List<Barco> listaBarcos = new List<Barco>;

            // Ejemplo de creación
            listaBarcos.Add(new Barco("THOR", 1, 'h', new Coordenada(0, 0)));
            listaBarcos.Add(new Barco("LOKI", 2, 'h', new Coordenada(1, 2)));
            listaBarcos.Add(new Barco("MAYA", 3, 'h', new Coordenada(3, 1)));

            // Inicializar el tablero con un tamaño
            tablero = new Tablero(4, listaBarcos);

            // Suscribirse al evento de fin de partida del tablero
            tablero.eventoFinPartida += CuandoEventoFinPartida;

            while (!finPartida) {

                // Mostrar por consola estado completo
                Console.WriteLine(tablero.ToString());

                // Pedimos la coordenada al usuario
                Console.Write("Introduce la coordenada a la que disparar FILA COLUMNA ('S' para salir): ");
                string entrada = Console.ReadLine();

                // Comprobar si el usuario quiere abandonar la partida
                if (entrada != null && entrada.ToUpper() == "S") {
                    finPartida = true;
                } else {

                }
            }
        }
    }
}
