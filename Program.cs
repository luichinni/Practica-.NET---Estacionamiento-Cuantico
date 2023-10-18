using EstacionamientoCuantico;

int[] plazasVip = new int[] { 3, 7, 12 };
int plazasFinitas = 12;

Estacionamiento ApertureParking = Randomizador.RandEstacionamiento(plazasFinitas,plazasVip);
Menu menusito = new Menu(ApertureParking);
bool fin = false;

while (!fin)
{
    menusito.Mostrar();
    fin = menusito.ProcesarEleccion(Console.ReadLine());
}

// menu reutilizable (al final no)

// cambiar las listas y arreglos con una clase plazas(tamaño,vehiculo)
// en eliminar dar la opcion de eliminar uno especifico o todos