using EstacionamientoCuantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
    class PlazaEstacionamientoCuantico : PlazaEstacionamiento
    {
        public Tamaño TamañoPlaza { get; set; }
        public PlazaEstacionamientoCuantico(Vehiculo vehiculoEstacionado, Tamaño tamañoPlaza) : base(vehiculoEstacionado)
        {
            this.TamañoPlaza = tamañoPlaza;
        }
    }
}
