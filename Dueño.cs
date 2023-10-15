using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
    class Dueño
    {
        public int Dni { get; private set; }
        public string Nombre { get; private set; } = "Rattmann";
        private bool _vip = false;

        public Dueño(int dni, string nombre, bool vip = false) 
        {
            this.Dni = dni;
            this.Nombre = nombre;
            this.setVip(vip);
        }
        public bool esVip() // para mayor legibilidad
        {
            return _vip;
        }
        public void setVip(bool vip)
        {
            _vip = vip;
        }
    }
}
