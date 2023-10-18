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
        private Vehiculo LeerVehiculo() // comprobacion de datos
        {
            Dueño dueño = LeerDueño();

            Console.WriteLine("Ingrese Modelo");
            string modelo = Console.ReadLine();

            Console.WriteLine("Ingrese Matricula");
            string matricula = Console.ReadLine();

            Console.WriteLine("Ingrese el largo");
            string largo = "";
            //double largo = double.Parse(Console.ReadLine().Replace(',', '.'));
            while (!EsNumerico(largo.Remove(largo.IndexOf('.'))))
            {
                largo = Console.ReadLine().Replace(',', '.');
            }
            double largoNum = double.Parse(largo);

            Console.WriteLine("Ingrese el ancho");
            string ancho = "";
            //double largo = double.Parse(Console.ReadLine().Replace(',', '.'));
            while (!EsNumerico(ancho.Remove(ancho.IndexOf('.'))))
            {
                ancho = Console.ReadLine().Replace(',', '.');
            }
            double anchoNum = double.Parse(ancho);

            return new Vehiculo(dueño, modelo, matricula, largoNum, anchoNum);
        }
        private Dueño LeerDueño()
        {
            string dni = "";
            Console.WriteLine("Ingrese el DNI");
            while (!EsNumerico(dni))
            {
                dni = Console.ReadLine();
            }

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
            bool pudoDesaparcar = ApertureParking.IntentarDesaparcarVehiculo(matricula);
            if (pudoDesaparcar)
            {
                Console.WriteLine($"Vehiculo de matricula {matricula} desaparcado con exito");
            }
            else
            {
                Console.WriteLine($"No existe un vehiculo de matricula {matricula}");
            }
        }
        // QUITAR VEHICULO POR DNI --------------------------------------------------------
        private void QuitarVehiculoDni()
        {
            Console.WriteLine("Ingrese DNI del titular cuyo vehiculo/s desea desaparcar");
            string dni = Console.ReadLine();
            // listar vehiculo de cierto dni
            List<Vehiculo> listaV = ApertureParking.getListadoVehiculosDe(dni);
            if (listaV.Count > 0)
            {
                Console.WriteLine("Vehiculos encontrados:");
                int indice = 0;
                foreach (Vehiculo vo in listaV)
                {
                    Console.WriteLine((indice) + ": " + vo.ToString());
                    indice++;
                }
                string seleccion = "";
                while (!EsNumerico(seleccion))
                {
                    Console.WriteLine($"\nNro de vehiculo a desaparcar (o ingrese otro valor numerico para desaparcar todos)");
                    seleccion = Console.ReadLine(); // mientras la rta no sea numerica sigue pidiendo valores
                }
                int selec = int.Parse(seleccion); // parsea
                if (selec >= 0 && selec < indice) ApertureParking.IntentarDesaparcarVehiculo(listaV[selec].Matricula); // si la seleccion esta en el rango de vehiculos, lo elimina
                else// sino, elimina todos
                {
                    foreach(Vehiculo vo in listaV)
                    {
                        ApertureParking.IntentarDesaparcarVehiculo(vo.Matricula);
                    }
                }
            }
            else
            {
                Console.WriteLine($"La persona de dni {dni} no posee autos estacionados");
            }
            
        }
        private bool EsNumerico(string str)
        {
            int cantNumeros=0;
            foreach(char c in str)
            {
                if (c >= '0' && c <= '9') cantNumeros++;
            }
            return cantNumeros == (str.Length-1);
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
                ApertureParking.IntentarDesaparcarVehiculo(vAux.Matricula);
                listaVehiculos.Remove(vAux);
            }
        }
        // OPTIMIZAR ESPACIO CUANTICO -----------------------------------------------------
        private void OptimizarEspacioCuantico() => ApertureParking.OptimizarEspacio();
    }
}
