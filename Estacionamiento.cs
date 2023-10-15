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
        private List<int> plazasVip = new List<int>() { 3, 7, 12 };
        private List<Vehiculo> zonaCuantica;
        private List<Tamaño> tamañoZonaCuantica;

        public Estacionamiento() 
        {
            this.zonaFinita = new Vehiculo[tamañoZonaFinita];
            this.plazasFinitasOcupadas = new bool[tamañoZonaFinita];
            this.zonaCuantica = new List<Vehiculo>();
            this.tamañoZonaCuantica = new List<Tamaño>();
        }

        public void Estacionar(Vehiculo vehiculo)
        {
            bool pudoEstacionar = false;
            if (HayPlazasFinitasDisponibles())
                pudoEstacionar = EstacionarEnZonaFinita(vehiculo); // retorna bool si puede o no
            
            if (!pudoEstacionar)
                EstacionarEnZonaCuantica(vehiculo);
        }
        private void EstacionarEnZonaCuantica(Vehiculo vehiculo)
        {

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
