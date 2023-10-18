using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
    abstract class PlazaEstacionamiento // metia herencia el tipo.. aguantia
    {
        public Vehiculo VehiculoEstacionado { get; set; }
        public PlazaEstacionamiento(Vehiculo vehiculoEstacionado) 
        {
            this.VehiculoEstacionado = vehiculoEstacionado;
        }

        public bool estaOcupada()
        {
            return VehiculoEstacionado != null;
        }
    }
}
