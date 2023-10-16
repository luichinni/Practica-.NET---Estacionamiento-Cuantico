using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
    class Estacionamiento
    {
        private int tamañoZonaFinita = 12;
        private Vehiculo[] zonaFinita;
        private bool[] plazasFinitasOcupadas;
        private List<int> plazasVip = new List<int>() { 3, 7, 12 }; // de esta manera podemos tener más vip

        private List<Vehiculo> zonaCuantica;
        private List<Tamaño> tamañoZonaCuantica;

        public Estacionamiento() 
        {
            this.zonaFinita = new Vehiculo[tamañoZonaFinita];
            this.plazasFinitasOcupadas = new bool[tamañoZonaFinita];
            this.zonaCuantica = new List<Vehiculo>();
            this.tamañoZonaCuantica = new List<Tamaño>();
        }
        public List<Vehiculo> getListadoVehiculos()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo> ();

            foreach (Vehiculo v in zonaFinita)
            {
                if(v != null)
                    vehiculos.Add(v);
            }
            foreach (Vehiculo v in zonaCuantica)
            {
                vehiculos.Add(v);
            }

            return vehiculos;
        }
        public override string ToString()
        {
            string strRet = "";
            strRet += "Estacionamiento finito: \nPlaza Nro\t\tInfo Vehiculo\n";
            for (int i = 0; i < zonaFinita.Length; i++)
            {
                if (zonaFinita[i] != null)
                    strRet += $"{i+1}:\t\t" + zonaFinita[i].ToString()+"\n";
            }
            strRet += "Estacionamiento cuantico:\nTamaño Plaza\t\tInfo Vehiculo\n";
            string tabulacion = "";
            for (int i = 0; i < zonaCuantica.Count; i++)
            {
                tabulacion = tamañoZonaCuantica[i] == Tamaño.Standard ? "\t" : "\t\t";
                strRet += $"{tamañoZonaCuantica[i]}"+ tabulacion + zonaCuantica[i].ToString()+"\n";
            }
            return strRet;
        }
        // OPTIMIZAR ESPACIO (quizas) ------------------------------------------------
        public void OptimizarEspacio()
        {
            int cantTipos = Enum.GetValues(typeof(Tamaño)).Length; // cantidad de tipos
            List<Vehiculo>[] vehiculos = new List<Vehiculo>[cantTipos]; // array de listas por tipo
            for (int i=0; i<cantTipos; i++) vehiculos[i] = new List<Vehiculo> (); // inicializamos las listas

            foreach(Vehiculo ve in zonaCuantica)
                vehiculos[(int)ve.GetTamaño()].Add(ve); // separamos por tipo los vehiculos

            int[] dimLogicaLista = new int[cantTipos]; // dimension logica de la lista para tener el indice del ultimo seleccionado
            int tipoPlaza;

            for(int i = 0; i < zonaCuantica.Count; i++)
            {
                tipoPlaza = (int)tamañoZonaCuantica[i];
                zonaCuantica[i] = getSiguiente(vehiculos, tipoPlaza, dimLogicaLista);
            }
        }
        private Vehiculo getSiguiente(List<Vehiculo>[] vehiculos, int tamaño, int[] contadorLista)
        {
            // si dimL es menor a la cantidad de un tipo
            //      buscar el siguiente de ese tipo
            // sino si dimL es mayor igual a la cantidad de un tipo
            //      busca el siguiente del tamaño anterior
            while (contadorLista[tamaño] >= vehiculos[tamaño].Count) tamaño--;
            contadorLista[tamaño]++;
            return vehiculos[tamaño][contadorLista[tamaño]-1];
        }
        // DESAPARCAR DE POR DNI ---------------------------------------------
        public void DesaparcarVehiculoDe(string dniDueño)
        {
            bool pudoDesaparcar = DesaparcarDeZonaFinitaDni(dniDueño); // si no existe no puede desaparcar
            if (!pudoDesaparcar)
            {
                DesaparcarDeZonaCuanticaDni(dniDueño);
            }
        }
        private void DesaparcarDeZonaCuanticaDni(string dniDueño)
        {
            int indice = 0;
            while (indice < zonaCuantica.Count)
            {
                if (zonaCuantica[indice].Dueño.Dni.Equals(dniDueño))
                {
                    tamañoZonaCuantica.RemoveAt(indice); // en zona cuantica no necesito conservar nada
                    zonaCuantica.RemoveAt(indice);
                }
                indice++;
            }
        }
        private bool DesaparcarDeZonaFinitaDni(string dniDueño)
        {
            bool pudo = false;
            int indice = 0;
            while (!pudo && indice < zonaFinita.Length)
            {
                if (zonaFinita[indice]!=null && zonaFinita[indice].Dueño.Dni.Equals(dniDueño))
                {
                    plazasFinitasOcupadas[indice] = false;
                    zonaFinita[indice] = null;
                    pudo = true;
                }
                indice++;
            }
            return pudo;
        }
        // DESAPARCAR POR MATRICULA -----------------------------------------------
        public void DesaparcarVehiculo(string matricula)
        {
            bool pudoDesaparcar = DesaparcarDeZonaFinita(matricula); // si no existe no puede desaparcar
            if (!pudoDesaparcar)
            {
                DesaparcarDeZonaCuantica(matricula);
            }
        }
        private void DesaparcarDeZonaCuantica(string matricula) 
        {
            int indice = 0;
            while (indice < zonaCuantica.Count)
            {
                if (zonaCuantica[indice].Matricula.Equals(matricula))
                {
                    tamañoZonaCuantica.RemoveAt(indice); // en zona cuantica no necesito conservar nada
                    zonaCuantica.RemoveAt(indice);
                }
                indice++;
            }
        }
        private bool DesaparcarDeZonaFinita(string matricula)
        {
            bool pudo = false;
            int indice = 0;
            while (!pudo && indice < zonaFinita.Length)
            {
                if (zonaFinita[indice] != null && zonaFinita[indice].Matricula.Equals(matricula))
                {
                    plazasFinitasOcupadas[indice] = false;
                    zonaFinita[indice] = null;
                    pudo = true;
                }
                indice++;
            }
            return pudo;
        }
        // APARCAR VEHICULOS ------------------------------------------------
        public void Aparcar(Vehiculo vehiculo)
        {
            bool pudoEstacionar = false;
            if (HayPlazasFinitasDisponibles())
                pudoEstacionar = EstacionarEnZonaFinita(vehiculo); // retorna bool si puede o no

            if (!pudoEstacionar)
                EstacionarEnZonaCuantica(vehiculo);
        }
        private void EstacionarEnZonaCuantica(Vehiculo vehiculo)
        {
            // generar tamaño de estacionamiento que acepte el tamaño de vehiculo
            Tamaño tamañoPlaza = getTamañoPlaza(vehiculo.GetTamaño());
            this.zonaCuantica.Add(vehiculo);
            this.tamañoZonaCuantica.Add(tamañoPlaza);
        }
        private Tamaño getTamañoPlaza(Tamaño tamañoVehiculo)
        { 
            //#nullable disable
            Random rand = new Random();
            Array tipos = Enum.GetValues(typeof(Tamaño));
            Tamaño tamañoRet = (Tamaño)tipos.GetValue(rand.Next(tipos.Length));

            while (tamañoRet < tamañoVehiculo) // genera un tamaño que permita el vehiculo aunq no sea el que mejor se adapta
                tamañoRet = (Tamaño)tipos.GetValue(rand.Next(tipos.Length));

            return tamañoRet;
            //#nullable enable
        }
        private bool HayPlazasFinitasDisponibles() => plazasFinitasOcupadas.Contains(false);
        private bool EstacionarEnZonaFinita(Vehiculo vehiculo)
        {
            bool pudoEstacionar = false;
            if (vehiculo.Dueño.esVip()) // si el dueño es vip y hay plaza vip disponible
            {
                int vipDisponible = getVipDisponible();
                if (vipDisponible != -1)
                {
                    this.plazasFinitasOcupadas[vipDisponible-1] = true;
                    this.zonaFinita[vipDisponible-1] = vehiculo;
                    pudoEstacionar = true;
                }
            }
            if (!pudoEstacionar) // si el vip no pudo estacionar o si no era vip entra aca
            {
                int sigDisponible = getSiguienteDisponible();
                if (sigDisponible != -1)
                {
                    this.plazasFinitasOcupadas[sigDisponible] = true;
                    this.zonaFinita[sigDisponible] = vehiculo;
                    pudoEstacionar = true;
                }
            }
            return pudoEstacionar;
        }
        private int getSiguienteDisponible()
        {
            int plazaDisponible = -1;
            int indice = 0;
            while (plazaDisponible == -1 && indice < plazasFinitasOcupadas.Length)
            {
                if (!plazasVip.Contains(indice) && !plazasFinitasOcupadas[indice])// si no es vip y no está ocupada
                    plazaDisponible = indice;
                indice++;
            }
            return plazaDisponible;
        }
        private int getVipDisponible() // cuenta de las plazas vip cuantas hay ocupadas
        {
            int plazaDisponible = -1;
            int indice = 0;
            while (plazaDisponible == -1 && indice < plazasVip.Count) // mientras no haya una plaza disponible y hayan plazas por ver
            {
                if (!plazasFinitasOcupadas[ plazasVip[indice]-1 ]) // si la plaza finita vip no está ocupada
                    plazaDisponible = plazasVip[indice]; // la guarda para ocupar

                indice++;
            }
            return plazaDisponible;
        }
    }
}
