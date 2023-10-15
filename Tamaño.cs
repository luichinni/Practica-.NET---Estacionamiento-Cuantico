using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
    enum Tamaño
    {
        Mini,
        Standard,
        Max
    }
    class Tamaños
    {
        // primer numero limite largo, segundo limite ancho, todo en metros
        public static double[] mini { get; private set; } = { 4, 1.5 };
        public static double[] standard { get; private set; } = { 5, 2 };
        public static double[] max { get; private set; } = { double.MaxValue, double.MaxValue};

        private Tamaños() { }

        public static Tamaño getTamaño(double largo,double ancho)
        {
            Tamaño tamañoRet;
            if(largo < mini[0] && ancho < mini[1])
            {
                tamañoRet = Tamaño.Mini;
            }else if(mini[0] < largo && largo < standard[0] && mini[1] < ancho && ancho < standard[1]){
                tamañoRet = Tamaño.Standard;
            }else // si no cumple con ningun tamaño, es max
            {
                tamañoRet = Tamaño.Max;
            }
            return tamañoRet;
        }
    }
}
