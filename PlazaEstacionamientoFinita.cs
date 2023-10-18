using EstacionamientoCuantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
    class PlazaEstacionamientoFinita : PlazaEstacionamiento
    {
        private bool _vip { get; set; }
        public PlazaEstacionamientoFinita(Vehiculo vehiculoEstacionado, bool vip = false) : base(vehiculoEstacionado)
        {
            setVip(vip);
        }
        public void setVip(bool vip)
        {
            this._vip = vip;
        }
        public bool esParaVip()
        {
            return _vip;
        }
    }
}
