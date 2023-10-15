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
        public Tamaño Tamaño { get; private set; }

        public Vehiculo(Dueño dueño,double largo, double ancho)
        {
            this.Dueño = dueño;
            this.Tamaño = Tamaños.getTamaño(largo, ancho);
        }
    }
}
