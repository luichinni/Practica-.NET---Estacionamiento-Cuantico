using EstacionamientoCuantico;

Estacionamiento ApertureParking = Randomizador.RandEstacionamiento();
Menu menusito = new Menu(ApertureParking);
bool fin = false;

while (!fin)
{
    menusito.Mostrar();
    fin = menusito.ProcesarEleccion(Console.ReadLine());
}