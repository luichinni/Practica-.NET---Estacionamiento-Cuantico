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
        private static string numeros = "1234567890";
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
        public static string RandStringNum(int tamaño)
        {
            string strRet = "";
            for (int i = 0; i < tamaño; i++)
            {
                strRet += numeros[rnd.Next(numeros.Length)];
            }
            return strRet;
        }
        public static Estacionamiento RandEstacionamiento()
        {
            Estacionamiento e = new Estacionamiento();
            int iteraciones = rnd.Next(150);
            for (int i = 0; i < iteraciones; i++)
            {
                e.Aparcar(RandVehiculo());
            }
            return e;
        }
        public static bool RandBool() => rnd.Next(2) == 1;
        public static Dueño RandDueño()
        {
            return new Dueño(RandStringNum(6), RandString(6), RandBool());
        }
        public static Vehiculo RandVehiculo()
        {
            return new Vehiculo(RandDueño(), RandString(6), RandString(6), rnd.NextDouble() * 6, rnd.NextDouble() * 3);
        }
    }
}
