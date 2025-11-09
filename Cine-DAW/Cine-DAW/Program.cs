// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;


Configuracion ValidarArgumentosEntrada(string[] args) {
    // Validamos que haya dos argumentos
    if (args.Length != 2) {
        Console.WriteLine("❌ Error: Debe ingresar dos argumentos: fila:X columna:Y");
        return PedirConfiguracion();
    }

    // Validamos fila:X
    var fila = args[0].Split(':');
    if (fila.Length != 2 || !int.TryParse(fila[1], out var filaParsed) || filaParsed < 4 || filaParsed > 7) {
        Console.WriteLine(
            $"❌ Error: El argumento '{args[0]}' no es válido. Debe ser fila:X, donde X es un entero entre 4 y 7.");
        return PedirConfiguracion();
    }

    // Validamos columna:Y
    var columna = args[1].Split(':');
    if (columna.Length != 2 || !int.TryParse(columna[1], out var columnaParsed) || columnaParsed < 5 ||
        columnaParsed > 9) {
        Console.WriteLine(
            $"❌ Error: El argumento '{args[1]}' no es válido. Debe ser columna:Y, donde Y es un entero entre 5 y 9.");
        return PedirConfiguracion();
    }

    // Si todo es correcto, retornamos la configuración
    return new Configuracion {
        Fila = filaParsed,
        Columna = columnaParsed
    };
}

Configuracion PedirConfiguracion() {
    Console.WriteLine("---Configuración del cine---");
    Console.WriteLine("Por favor ingrese los parámetros fila y columna, de la siguiente forma: fila:X columna:Y");

    var regex = new Regex(@"^fila:(\d+)\s+columna:(\d+)$");

    var input = (Console.ReadLine() ?? "").Trim();
    while (!regex.IsMatch(input)) {
        Console.WriteLine("❌ Error: Entrada inválida. Inténtelo de nuevo con el formato correcto.");
        input = (Console.ReadLine() ?? "").Trim();
    }

    var match = regex.Match(input);
    var fila = int.Parse(match.Groups[1].Value);
    var columna = int.Parse(match.Groups[2].Value);

    if (fila < 4 || fila > 7 || columna < 5 || columna > 9) {
        Console.WriteLine("❌ Error: Los valores están fuera del rango permitido. Inténtelo de nuevo.");
        return PedirConfiguracion();
    }

    return new Configuracion {
        Fila = fila,
        Columna = columna
    };
}




const int TamañoFila = 5;
const int TamañoColumna = 8;


const double PrecioEntrada = 6.50;

struct Posicion {
    int fila;
    int columna;
}

enum Butaca {
    Libre,
    Ocupada,
    FueraDeServicio
}



enum Menú {
    VerSala,
    ComprarEntrada,
    DevolverEntrada,
    Recaudación, 
    Informe, 
    Salir
}

void MostrarMenuPrincipal(Butaca[,] sala) {
    int opcion = 0;
    Menú opcionMenu = Menú.VerSala; // Inicialización para que esté disponible en el while

    Console.WriteLine("Bienvenido al cine DAW");
    Console.WriteLine("=====================");
    Console.WriteLine("");

    do {
        Console.WriteLine("Opción 1: Visualizar sala");
        Console.WriteLine("Opción 2: Comprar entrada");
        Console.WriteLine("Opción 3: Devolver entrada");
        Console.WriteLine("Opción 4: Recaudación total");
        Console.WriteLine("Opción 5: Informe");
        Console.WriteLine("Opción 6: Salir");

        var inputOpcion = Console.ReadLine().Trim();

        if (!int.TryParse(inputOpcion, out opcion) || opcion < 1 || opcion > 6) {
            Console.WriteLine("❌ Entrada inválida. Intente nuevamente.");
            continue;
        }

        opcionMenu = (Menú)(opcion - 1);

        switch (opcionMenu) {
            case Menú.VerSala:
                imprimirSala(sala);
                break;

            case Menú.ComprarEntrada:
                comprarEntrada(sala);
                break;

            case Menú.DevolverEntrada:
                devolverEntrada(sala);
                break;

            case Menú.Recaudación:
                calcularRecaudacion(sala);
                break;

            case Menú.Informe:
                verInforme(sala);
                break;

            case Menú.Salir:
                Console.WriteLine("Nos sentimos tristes que te vayas, vuelva otro díaaa");
                break;

            default:
                Console.WriteLine("Opción no válida");
                break;
        }

    } while (opcionMenu != Menú.Salir);
}

