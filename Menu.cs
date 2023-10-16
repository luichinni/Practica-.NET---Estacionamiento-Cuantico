using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamientoCuantico
{
     class Menu
    {
        public Estacionamiento ApertureParking { get; set; }
        public Menu(Estacionamiento estacionamiento) 
        {
            this.ApertureParking = estacionamiento;
        }
        public bool ProcesarEleccion(string eleccion)
        {
            bool fin = false;
            switch (eleccion)
            {
                case "1":
                    ListarVehiculos();
                    break;
                case "2":
                    AgregarVehiculo();
                    break;
                case "3":
                    QuitarVehiculoMatricula();
                    break;
                case "4":
                    QuitarVehiculoDni();
                    break;
                case "5":
                    QuitarVehiculosRandom();
                    break;
                case "6":
                    OptimizarEspacioCuantico();
                    break;
                case "7":
                    fin = true;
                    break;
                default:
                    Console.WriteLine("Opcion invalida");
                    break;
            }
            return fin;
        }
        public void Mostrar()
        {
            Console.WriteLine("Aperture Parking");
            Console.WriteLine("1. Listar Vehiculos");
            Console.WriteLine("2. Agregar Vehiculo");
            Console.WriteLine("3. Quitar Vehiculo por Matricula");
            Console.WriteLine("4. Quitar Vehiculo por Dni");
            Console.WriteLine("5. Quitar Cantidad Aleatoria de Vehiculos");
            Console.WriteLine("6. Optimizar Espacio");
            Console.WriteLine("7. Salir");
        }
        // LISTAR VEHICULOS ---------------------------------------------------------------
        private void ListarVehiculos()
        {
            Console.WriteLine(ApertureParking.ToString());
        }
        // AGREGAR VEHICULO ---------------------------------------------------------------
        private void AgregarVehiculo()
        {
            Vehiculo v = LeerVehiculo();
            ApertureParking.Aparcar(v);
        }
        private Vehiculo LeerVehiculo()
        {
            Dueño dueño = LeerDueño();
            Console.WriteLine("Ingrese Modelo");
            string modelo = Console.ReadLine();
            Console.WriteLine("Ingrese Matricula");
            string matricula = Console.ReadLine();
            Console.WriteLine("Ingrese el largo");
            double largo = double.Parse(Console.ReadLine().Replace(',', '.'));
            Console.WriteLine("Ingrese el ancho");
            double ancho = double.Parse(Console.ReadLine().Replace(',', '.'));
            return new Vehiculo(dueño, modelo, matricula, largo, ancho);
        }
        private Dueño LeerDueño()
        {
            Console.WriteLine("Ingrese el DNI");
            string dni = Console.ReadLine();
            Console.WriteLine("Ingrese el Nombre");
            string nombre = Console.ReadLine();
            Console.WriteLine("Es vip? s/n");
            bool vip = Console.ReadLine().ToLower().Equals("s");
            return new Dueño(dni, nombre, vip);
        }
        // QUITAR VEHICULO POR MATRICULA --------------------------------------------------
        private void QuitarVehiculoMatricula()
        {
            Console.WriteLine("Ingrese la matricula del auto a desaparcar");
            string matricula = Console.ReadLine();
            ApertureParking.DesaparcarVehiculo(matricula);
        }
        // QUITAR VEHICULO POR DNI --------------------------------------------------------
        private void QuitarVehiculoDni()
        {
            Console.WriteLine("Ingrese DNI del titular cuyo vehiculo desea desaparcar (primer ocurrencia)");
            string dni = Console.ReadLine();
            ApertureParking.DesaparcarVehiculoDe(dni);
        }
        // QUITAR VEHICULOS RANDOM --------------------------------------------------------
        private void QuitarVehiculosRandom()
        {
            Random rnd = new Random();
            List<Vehiculo> listaVehiculos = ApertureParking.getListadoVehiculos();
            Vehiculo vAux;
            int iteraciones = rnd.Next(listaVehiculos.Count);
            for (int i = 0; i < iteraciones; i++)
            {
                vAux = listaVehiculos[rnd.Next(listaVehiculos.Count)];
                ApertureParking.DesaparcarVehiculo(vAux.Matricula);
                listaVehiculos.Remove(vAux);
            }
        }
        // OPTIMIZAR ESPACIO CUANTICO -----------------------------------------------------
        private void OptimizarEspacioCuantico() => ApertureParking.OptimizarEspacio();
    }
}
