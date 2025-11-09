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


void imprimirSala(Butaca[,] sala) {
    // Imprimir encabezado de columnas (una sola vez)
    Console.Write("   "); // espacio para la esquina
    for (int j = 0; j < sala.GetLength(1); j++) {
        Console.Write($"{j + 1,3}"); // columnas numeradas
    }
    Console.WriteLine();

    // Imprimir cada fila con letra y símbolos
    for (int i = 0; i < sala.GetLength(0); i++) {
        char letraFila = (char)('A' + i); // convierte 0 → A, 1 → B, etc.
        Console.Write($"{letraFila}  "); // letra de la fila

        for (int j = 0; j < sala.GetLength(1); j++) {
            switch (sala[i, j]) {
                case Butaca.Libre:
                    Console.Write("🟢 ");
                    break;
                case Butaca.Ocupada:
                    Console.Write("🔴 ");
                    break;
                case Butaca.FueraDeServicio:
                    Console.Write("🚫 ");
                    break;
                default:
                    Console.Write("❓ ");
                    break;
            }
        }
        Console.WriteLine(); // salto de línea al final de la fila
    }
}

string obtenerLetra(int fila) {
    const string filasLetras = "ABCDEFG";
    return filasLetras[fila].ToString();
}


int obtenerIndiceFila(string letra) {
    const string filasLetras = "ABCDEFG";
    return filasLetras.IndexOf(letra.ToUpper());
}


void ocuparButaca(Butaca[,] sala, Posicion posicion) {
    sala[posicion.fila, posicion.columna] = Butaca.Ocupada;
    Console.WriteLine("Butaca ocupada con éxito. Coste: " + PrecioEntrada + "€");
}


bool hayButacaLibre(Butaca[,] sala) {
    for (int fila = 0; fila < sala.GetLength(0); fila++) {
        for (int columna = 0; columna < sala.GetLength(1); columna++) {
            if (sala[fila, columna] == Butaca.Libre)
                return true;
        }
    }
    return false;
}

void comprarEntrada(Butaca[,] sala) {
    if (!hayButacaLibre(sala) ) {
        Console.WriteLine("No Hay butacas libres");
        return
    }

    Console.WriteLine("Ingrese una butaca con el formato Letra:Número (por ejemplo: B:3)");


    var regex = new Regex (@"^([A-G]):(\d+)$");
    var input = (Console.ReadLine() ?? "");
    while(!regex.IsMatch(input)) {
        Console.WriteLine("❌Error: formato incorrecto, vuelva a intentarlo");
        input = (Console.ReadLine() ?? "").Trim();
    }

    var match = regex.Match(input);
    var fila = obtenerIndiceFila(match.Groups[1].Value);
    var columna = int.Parse(match.Groups[2].Value);

    columna -= 1;

    if (fila < 0 || fila >= sala.GetLength(0) || columna < 0 || columna >= sala.GetLength(1)) {
        Console.WriteLine("❌ Posición fuera de rango");
        return;
    }

    if (sala[fila, columna] != Butaca.Libre) {
        Console.WriteLine("❌ La butaca no está disponible");
        return;
    }

    ocuparButaca(sala, new Posicion{fila = fila, columna = columna});


}


