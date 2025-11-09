// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;


using System;
using System.Text.RegularExpressions;

var random = new Random();
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.Clear();

// ----------------------------------------------------
// BLOQUE PRINCIPAL (Top-Level Statements)
// ----------------------------------------------------

// **INICIO DEL PROGRAMA PRINCIPAL**

Console.Clear();

// 0. Validación de la entrada del tamaño de la sala por argumentos
var configuracion = ValidarArgumentosEntrada(args);

// 1. Presentación del sistema
Console.WriteLine("=============================================");
Console.WriteLine("     🎬 INICIANDO SISTEMA CINE-DAM 🎟️       ");
Console.WriteLine("=============================================");
Console.WriteLine("Parámetros de la sala:");
Console.WriteLine($"\t- Filas: {configuracion.Fila}");
Console.WriteLine($"\t- Columnas: {configuracion.Columna}");
Console.WriteLine();

// 2. Creación de la sala
var sala = new Butaca[configuracion.Fila, configuracion.Columna];


// 5. Finalización
Console.WriteLine("\n👋 Gracias por usar Cine-DAM. Presiona una tecla para salir...");
Console.ReadKey();
return;

// ----------------------------------------------------
// FUNCIONES Y PROCEDIMIENTOS AUXILIARES
// ----------------------------------------------------

Configuracion ValidarArgumentosEntrada(string[] args) {
    if (args.Length != 2 || !args[0].StartsWith("fila:") || !args[1].StartsWith("columna:"))
    {
        Console.WriteLine("❌ Error: Debe ingresar dos argumentos con formato fila:X columna:Y");
        return PedirConfiguracion();
    }

    var filaSplit = args[0].Split(':');
    var columnaSplit = args[1].Split(':');

    if (!int.TryParse(filaSplit[1], out var filaParsed) || filaParsed < 4 || filaParsed > 7)
    {
        Console.WriteLine("❌ Error: Fila fuera de rango [4-7]");
        return PedirConfiguracion();
    }

    if (!int.TryParse(columnaSplit[1], out var columnaParsed) || columnaParsed < 5 || columnaParsed > 9)
    {
        Console.WriteLine("❌ Error: Columna fuera de rango [5-9]");
        return PedirConfiguracion();
    }

    return new Configuracion {
        Fila = filaParsed,
        Columna = columnaParsed
    };
}

Configuracion PedirConfiguracion()
{
    Console.WriteLine("--- Configuración de la Sala ---");
    Console.WriteLine("Ingrese los parámetros con el formato: fila:X columna:Y");

    var regex = new Regex(@"fila:(\d+)\s*columna:(\d+)", RegexOptions.IgnoreCase);

    while (true)
    {
        var input = (Console.ReadLine() ?? "").Trim();

        if (!regex.IsMatch(input))
        {
            Console.WriteLine("❌ Formato inválido. Ejemplo: fila:5 columna:8");
            continue;
        }

        var match = regex.Match(input);
        var fila = int.Parse(match.Groups[1].Value);
        var columna = int.Parse(match.Groups[2].Value);

        if (fila < 4 || fila > 7 || columna < 5 || columna > 9)
        {
            Console.WriteLine("❌ Valores fuera de rango. Filas [4-7], Columnas [5-9]");
            continue;
        }

        return new Configuracion() {
            Fila = fila,
            Columna = columna
        };
    }
}






const double PrecioEntrada = 6.50;

public struct Posicion {
    int Fila;
    int Columna;
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

void mostrarMenuPrincipal(Butaca[,] sala) {
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
    sala[posicion.Fila, posicion.Columna] = Butaca.Ocupada;
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
    if (!hayButacaLibre(sala)) {
        Console.WriteLine("No Hay butacas libres");
        return;
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

void devolverEntrada(Butaca[,] sala) {
    if (hayButacaLibre(sala)) {
        Console.WriteLine("Hay butacas libres");
        return;
    }
    
    Console.WriteLine("Ingrese la butaca que quiere deolver con el formato Letra:Número (por ejemplo: B:3)");
    var regex = new Regex(@"^([A-G]):(\d+)$");
    var input = (Console.ReadLine() ?? "").Trim();
    while (!regex.IsMatch(input)) {
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

    if (sala[fila, columna] == Butaca.Libre) {
        Console.WriteLine("❌ No hay entrada que devolver. La butaca ya está libre.");
    } else if (sala[fila, columna] == Butaca.Ocupada) {
        sala[fila, columna] = Butaca.Libre;
        Console.WriteLine("✅ Entrada devuelta con éxito.");
    } else if (sala[fila, columna] == Butaca.FueraDeServicio) {
        Console.WriteLine("Esta butaca está fuera de servivio, por favor eliga otra");
    }
    else {
        Console.WriteLine("Entrada no valida");
    }
    
    
}






