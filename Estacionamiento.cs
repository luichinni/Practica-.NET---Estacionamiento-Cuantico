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
                vehiculos.Add (v);
            foreach (Vehiculo v in zonaCuantica)
                vehiculos.Add (v);

            return vehiculos;
        }
        // DESAPARCAR DE POR DNI ---------------------------------------------
        public void DesaparcarVehiculoDe(int dniDueño)
        {
            bool pudoDesaparcar = DesaparcarDeZonaFinitaDni(dniDueño); // si no existe no puede desaparcar
            if (!pudoDesaparcar)
            {
                DesaparcarDeZonaCuanticaDni(dniDueño);
            }
        }
        private void DesaparcarDeZonaCuanticaDni(int dniDueño)
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
        private bool DesaparcarDeZonaFinitaDni(int dniDueño)
        {
            bool pudo = false;
            int indice = 0;
            while (!pudo && indice < zonaFinita.Length)
            {
                if (zonaFinita[indice].Dueño.Dni.Equals(dniDueño))
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
                if (zonaFinita[indice].Matricula.Equals(matricula))
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
                    this.plazasFinitasOcupadas[vipDisponible] = true;
                    this.zonaFinita[vipDisponible] = vehiculo;
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
            }
            return plazaDisponible;
        }
        private int getVipDisponible() // cuenta de las plazas vip cuantas hay ocupadas
        {
            int plazaDisponible = -1;
            int indice = 0;
            while (plazaDisponible == -1 && indice < plazasVip.Count) // mientras no haya una plaza disponible y hayan plazas por ver
            {
                if (!plazasFinitasOcupadas[ plazasVip[indice] ]) // si la plaza finita vip no está ocupada
                    plazaDisponible = plazasVip[indice]; // la guarda para ocupar

                indice++;
            }
            return plazaDisponible;
        }
    }
}
