using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
     class Randomizador
    {
        private static Random rnd = new Random();
        private static string letras = "abcdefghijklmnñopqrstuvwxyz";
        private Randomizador() { }
        public static string RandString(int tamaño)
        {
            string strRet = "";
            for (int i = 0; i< tamaño; i++)
            {
                strRet += letras[rnd.Next(letras.Length)];
            }
            return strRet;
        }
        public static Estacionamiento RandEstacionamiento()
        {
            Estacionamiento e = new Estacionamiento();
            int iteraciones = rnd.Next(100);
            for (int i = 0; i < iteraciones; i++)
                e.Aparcar(RandVehiculo());
            return e;
        }
        public static Dueño RandDueño() => new Dueño(rnd.Next(100000).ToString(),RandString(6),rnd.Next(2)==1);
        public static Vehiculo RandVehiculo() => new Vehiculo(RandDueño(),RandString(6),RandString(6),rnd.NextDouble()*6,rnd.NextDouble()*3);
    }
}
