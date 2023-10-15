﻿using EstacionamientoCuantico;

Estacionamiento ApertureParking = new Estacionamiento();
int eleccion;

while (!fin)
{
    MostrarMenu();
    eleccion = Console.ReadLine();
    ProcesarEleccion(eleccion);
}
// LISTAR VEHICULOS ---------------------------------------------------------------
void ListarVehiculos()
{
    foreach (Vehiculo vehiculo in ApertureParking.getListadoVehiculos())    
        Console.WriteLine(vehiculo.ToString());
}
// AGREGAR VEHICULO ---------------------------------------------------------------
void AgregarVehiculo()
{
    Vehiculo v = LeerVehiculo();
    ApertureParking.Aparcar(v);
}
Vehiculo LeerVehiculo()
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
Dueño LeerDueño()
{
    Console.WriteLine("Ingrese el DNI");
    int dni = int.Parse(Console.ReadLine());
    Console.WriteLine("Ingrese el Nombre");
    string nombre = Console.ReadLine();
    Console.WriteLine("Es vip? s/n");
    bool vip = Console.ReadLine().ToLower().Equals("s");
    return new Dueño(dni, nombre, vip);
}
// QUITAR VEHICULO POR MATRICULA --------------------------------------------------
void QuitarVehiculoMatricula()
{
    Console.WriteLine("Ingrese la matricula del auto a desaparcar");
    string matricula = Console.ReadLine();
    ApertureParking.DesaparcarVehiculo(matricula);
}
// QUITAR VEHICULO POR DNI --------------------------------------------------------
void QuitarVehiculoDni()
{
    Console.WriteLine("Ingrese DNI del titular cuyo vehiculo desea desaparcar (primer ocurrencia)");
    int dni = int.Parse(Console.ReadLine());
    ApertureParking.DesaparcarVehiculoDe(dni);
}
// QUITAR VEHICULOS RANDOM --------------------------------------------------------
void QuitarVehiculosRandom()
{
    Random rnd = new Random();
    int iteraciones = rnd.Next(ApertureParking.getListadoVehiculos().Count);
}
void ProcesarEleccion(int eleccion)
{
    switch (eleccion)
    {
        case 1:
            ListarVehiculos();
            break;
        case 2:
            AgregarVehiculo();
            break;
        case 3:
            QuitarVehiculoMatricula();
            break;
        case 4:
            QuitarVehiculoDni();
            break;
        case 5:
            QuitarVehiculosRandom();
            break;
        case 6:
            OptimizarEspacioCuantico();
            break;
        default:
            break;
    }
}
void MostrarMenu()
{
    Console.WriteLine("Aperture Parking");
    Console.WriteLine("1. Listar Vehiculos");
    Console.WriteLine("2. Agregar Vehiculo");
    Console.WriteLine("3. Quitar Vehiculo por Matricula");
    Console.WriteLine("4. Quitar Vehiculo por Dni");
    Console.WriteLine("5. Quitar Cantidad Aleatoria de Vehiculos");
    Console.WriteLine("6. Optimizar Espacio");
}