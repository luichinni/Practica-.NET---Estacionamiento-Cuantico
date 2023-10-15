using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
    class Vehiculo
    {
        public Dueño Dueño { get; private set; }
        public string Modelo { get; private set; }
        public string Matricula { get; private set; }
        public double Largo { get; private set; }
        public double Ancho { get; private set; }

        public Vehiculo(Dueño dueño, string modelo, string matricula, double largo, double ancho)
        {
            this.Dueño = dueño;
            this.Largo = largo;
            this.Ancho = ancho;
            this.Modelo = modelo;
            this.Matricula = matricula;
        }
        public Tamaño GetTamaño() => Tamaños.getTamaño(Largo, Ancho);
        public override string ToString()
        {
            return $"Dueño: {this.Dueño.Nombre}\t Matricula: {this.Matricula} \t Tamaño: {this.GetTamaño()}";
        }
    }
}
