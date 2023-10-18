using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
    class Estacionamiento
    {
        private PlazaEstacionamientoFinita[] zonaFinita;
        private int[] plazasVip;
        private List<PlazaEstacionamientoCuantico> zonaCuantica;

        public Estacionamiento(int tamañoZonaFinita, int[] plazasVip) 
        {
            this.plazasVip = plazasVip;
            this.zonaFinita = IniciarZonaFinita(tamañoZonaFinita);
            this.zonaCuantica = new List<PlazaEstacionamientoCuantico>();
        }
        private PlazaEstacionamientoFinita[] IniciarZonaFinita(int tamaño)
        {
            PlazaEstacionamientoFinita[] nuevaZona = new PlazaEstacionamientoFinita[tamaño];
            for(int i=0; i < nuevaZona.Length; i++)
            {
                nuevaZona[i] = new PlazaEstacionamientoFinita(null, plazasVip.Contains(i));
            }
            return nuevaZona;
        }
        public List<Vehiculo> getListadoVehiculos()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo> ();

            foreach (PlazaEstacionamientoFinita plazaEstacionamiento in zonaFinita)
            {
                if(plazaEstacionamiento.estaOcupada())
                    vehiculos.Add(plazaEstacionamiento.VehiculoEstacionado);
            }
            foreach (PlazaEstacionamientoCuantico plazaEstacionamiento in zonaCuantica)
            {
                vehiculos.Add(plazaEstacionamiento.VehiculoEstacionado);
            }

            return vehiculos;
        }
        public List<Vehiculo> getListadoVehiculosDe(string dni)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();

            foreach (PlazaEstacionamientoFinita plazaEstacionamiento in zonaFinita)
            {
                if (plazaEstacionamiento.estaOcupada() && plazaEstacionamiento.VehiculoEstacionado.Dueño.Dni.Equals(dni))
                    vehiculos.Add(plazaEstacionamiento.VehiculoEstacionado);
            }
            foreach (PlazaEstacionamientoCuantico plazaEstacionamiento in zonaCuantica)
            {
                if (plazaEstacionamiento.VehiculoEstacionado.Dueño.Dni.Equals(dni))
                    vehiculos.Add(plazaEstacionamiento.VehiculoEstacionado);
            }

            return vehiculos;
        }
        public override string ToString()
        {
            string strRet = "";
            strRet += "Estacionamiento finito: \nPlaza Nro\t\tInfo Vehiculo\n";
            for (int i = 0; i < zonaFinita.Length; i++)
            {
                if (zonaFinita[i].estaOcupada())
                    strRet += $"{i+1}:\t\t" + zonaFinita[i].VehiculoEstacionado.ToString()+"\n";
            }
            strRet += "Estacionamiento cuantico:\nTamaño Plaza\t\tInfo Vehiculo\n";
            string tabulacion = "";
            for (int i = 0; i < zonaCuantica.Count; i++)
            {
                tabulacion = zonaCuantica[i].TamañoPlaza == Tamaño.Standard ? "\t" : "\t\t";
                strRet += $"{zonaCuantica[i].TamañoPlaza}"+ tabulacion + zonaCuantica[i].VehiculoEstacionado.ToString()+"\n";
            }
            return strRet;
        }
        // OPTIMIZAR ESPACIO (quizas) ------------------------------------------------
        public void OptimizarEspacio()
        {
            int cantTipos = Enum.GetValues(typeof(Tamaño)).Length; // cantidad de tipos
            List<Vehiculo>[] vehiculos = new List<Vehiculo>[cantTipos]; // array de listas por tipo
            for (int i=0; i<cantTipos; i++) vehiculos[i] = new List<Vehiculo> (); // inicializamos las listas

            foreach(PlazaEstacionamientoCuantico plaza in zonaCuantica)
                vehiculos[(int)plaza.VehiculoEstacionado.GetTamaño()].Add(plaza.VehiculoEstacionado); // separamos por tipo los vehiculos

            int[] dimLogicaLista = new int[cantTipos]; // arreglo contador de cantidad de autos usados por lista
            int tipoPlaza;

            for(int i = 0; i < zonaCuantica.Count; i++)
            {
                tipoPlaza = (int)zonaCuantica[i].TamañoPlaza;
                zonaCuantica[i].VehiculoEstacionado = getSiguiente(vehiculos, tipoPlaza, dimLogicaLista);
            }
        }
        private Vehiculo getSiguiente(List<Vehiculo>[] vehiculos, int tamaño, int[] contadorLista)
        {
            // si dimL es menor a la cantidad de un tipo
            //      buscar el siguiente de ese tipo
            // sino si dimL es mayor igual a la cantidad de un tipo
            //      busca el siguiente del tamaño anterior
            while (contadorLista[tamaño] >= vehiculos[tamaño].Count) tamaño--;
            contadorLista[tamaño]++; // aumento del contador de autos usados del arreglo contador
            return vehiculos[tamaño][contadorLista[tamaño]-1];
        }
        // DESAPARCAR DE POR DNI ---------------------------------------------
        /*public void DesaparcarVehiculoDe(string dniDueño)
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
                if (zonaCuantica[indice].VehiculoEstacionado.Dueño.Dni.Equals(dniDueño))
                {
                    zonaCuantica.RemoveAt(indice); // en zona cuantica no necesito conservar nada
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
                if (zonaFinita[indice].estaOcupada() && zonaFinita[indice].VehiculoEstacionado.Dueño.Dni.Equals(dniDueño))
                {
                    zonaFinita[indice].VehiculoEstacionado = null;
                    pudo = true;
                }
                indice++;
            }
            return pudo;
        }*/
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
                if (zonaCuantica[indice].VehiculoEstacionado.Matricula.Equals(matricula))
                {
                    zonaCuantica.RemoveAt(indice);// en zona cuantica no necesito conservar nada
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
                if (zonaFinita[indice].estaOcupada() && zonaFinita[indice].VehiculoEstacionado.Matricula.Equals(matricula))
                {
                    zonaFinita[indice].VehiculoEstacionado = null;
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
            PlazaEstacionamientoCuantico plazaEstCuantic = new PlazaEstacionamientoCuantico(vehiculo, getTamañoPlaza(vehiculo.GetTamaño()));
            this.zonaCuantica.Add(plazaEstCuantic);
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
        private bool HayPlazasFinitasDisponibles() 
        {
            bool hayDisponible = false;
            int indice = 0;
            while (!hayDisponible && indice < zonaFinita.Length)
            {
                if (!zonaFinita[indice].estaOcupada())
                {
                    hayDisponible = true;
                }
                indice++;
            }
            return hayDisponible;
        }
        private bool EstacionarEnZonaFinita(Vehiculo vehiculo)
        {
            bool pudoEstacionar = false;
            if (vehiculo.Dueño.esVip()) // si el dueño es vip y hay plaza vip disponible
            {
                int vipDisponible = getVipDisponible();
                if (vipDisponible != -1)
                {
                    this.zonaFinita[vipDisponible-1].VehiculoEstacionado = vehiculo;
                    pudoEstacionar = true;
                }
            }
            if (!pudoEstacionar) // si el vip no pudo estacionar o si no era vip entra aca
            {
                int sigDisponible = getSiguienteDisponible();
                if (sigDisponible != -1)
                {
                    this.zonaFinita[sigDisponible].VehiculoEstacionado = vehiculo;
                    pudoEstacionar = true;
                }
            }
            return pudoEstacionar;
        }
        private int getSiguienteDisponible()
        {
            int plazaDisponible = -1;
            int indice = 0;
            while (plazaDisponible == -1 && indice < zonaFinita.Length)
            {
                if (!zonaFinita[indice].estaOcupada() && !zonaFinita[indice].esParaVip())// si no es vip y no está ocupada
                    plazaDisponible = indice;
                indice++;
            }
            return plazaDisponible;
        }
        private int getVipDisponible() // cuenta de las plazas vip cuantas hay ocupadas
        {
            int plazaDisponible = -1;
            int indice = 0;
            while (plazaDisponible == -1 && indice < plazasVip.Length) // mientras no haya una plaza disponible y hayan plazas por ver
            {
                if (!zonaFinita[plazasVip[indice]-1].estaOcupada()) // si la plaza finita vip no está ocupada
                    plazaDisponible = plazasVip[indice]; // la guarda para ocupar

                indice++;
            }
            return plazaDisponible;
        }
    }
}
