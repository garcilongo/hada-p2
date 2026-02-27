using System;
using System.Collections.Generic;
using System.Linq;
using Hada;

namespace Hada {
    public class Barco {
        // Propiedades públicas requeridas [cite: 168]
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }
        public string Nombre { get; private set; }
        public int NumDanyos { get; private set; }

        // Definición de los eventos
         public event EventHandler<TocadoArgs> eventoTocado;
         public event EventHandler<HundidoArgs> eventoHundido;

        // Constructor
        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio) {
             this.Nombre = nombre;
             this.NumDanyos = 0;
             this.CoordenadasBarco = new Dictionary<Coordenada, string>();

            // Calculamos las coordenadas según longitud y orientación
            for (int i = 0; i < longitud; i++) {
                Coordenada nueva;
                 if (orientacion == 'h') // Horizontal: aumenta la columna 
                {
                    nueva = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i);
                }
                else // Vertical: aumenta la fila
                {
                    nueva = new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna);
                }

                // Inicializamos con el nombre del barco
                this.CoordenadasBarco.Add(nueva, nombre);
            }
        }

        // Método para procesar un disparo
        public void Disparo(Coordenada c) {
            // Comprobamos si la coordenada pertenece al barco
            if (this.CoordenadasBarco.ContainsKey(c)) {
                string etiquetaActual = this.CoordenadasBarco[c];

                // Solo procesamos si no ha sido tocada antes 
                if (!etiquetaActual.EndsWith("_T")) {
                    // Actualizamos etiqueta con el sufijo _T
                    this.CoordenadasBarco[c] = etiquetaActual + "_T";
                     this.NumDanyos++;

                    // Lanzamos el evento de TOCADO 
                    if (eventoTocado != null) {
                        eventoTocado(this, new TocadoArgs(this.Nombre, c));
                    }

                    // Comprobamos si tras el impacto se ha hundido
                    if (this.hundido()) {
                        // Lanzamos el evento de HUNDIDO 
                        if (eventoHundido != null) {
                             eventoHundido(this, new HundidoArgs(this.Nombre));
                        }
                    }
                }
            }
        }

        // Verifica si todas las casillas están tocadas
        public bool hundido() {
            // Si todas las etiquetas contienen "_T", está hundido
            return CoordenadasBarco.Values.All(v => v.EndsWith("_T"));
        }

        // Formato de texto para el estado del barco
        public override string ToString() {
             string estado = $"[{this.Nombre}] - DAÑOS: [{this.NumDanyos}] - HUNDIDO: [{this.hundido()}] - COORDENADAS: ";
            
            foreach (var par in CoordenadasBarco) {
                 estado += $"[{par.Key.ToString()} : {par.Value}] ";
            }

            return estado.Trim();
        }
    }
}