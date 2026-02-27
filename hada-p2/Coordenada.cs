using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HADA {
    public class Coordenada {
        // Campos de respaldo (backing fields) [cite: 141]
        private int _fila;
        private int _columna;

        // Propiedades con validación (mínimo 0, máximo 9) [cite: 142, 143]
        public int Fila {
            get { return _fila; }
            set {
                if (value < 0) _fila = 0;
                else if (value > 9) _fila = 9;
                else _fila = value;
            }
        }

        public int Columna {
            get { return _columna; }
            set {
                if (value < 0) _columna = 0;
                else if (value > 9) _columna = 9;
                else _columna = value;
            }
        }

        // 1. Constructor por defecto 
        public Coordenada() {
            this.Fila = 0;
            this.Columna = 0;
        }

        // 2. Constructor con parámetros int 
        public Coordenada(int fila, int columna) {
            this.Fila = fila;
            this.Columna = columna;
        }

        // 3. Constructor con parámetros string 
        public Coordenada(string fila, string columna) {
            this.Fila = int.Parse(fila);
            this.Columna = int.Parse(columna);
        }

        // 4. Constructor de copia 
        public Coordenada(Coordenada otra) {
            this.Fila = otra.Fila;
            this.Columna = otra.Columna;
        }

        // Devuelve el formato (fila,columna) [cite: 154]
        public override string ToString() {
            return $"({this.Fila},{this.Columna})";
        }

        // Sobreescribe GetHashCode para usar la clase como clave en diccionarios [cite: 156]
        public override int GetHashCode() {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
    }

        // Sobreescribe Equals para comparar por valor de Fila y Columna [cite: 161, 162]
        public override bool Equals(object obj) {
            if (obj == null || !(obj is Coordenada)) {
                return false;
            }
            return Equals((Coordenada)obj);
        }

        // Sobrecarga de Equals para el tipo Coordenada [cite: 163, 165]
        public bool Equals(Coordenada coordenada) {
            if (coordenada == null) return false;
            return this.Fila == coordenada.Fila && this.Columna == coordenada.Columna;
        }
    }
}
