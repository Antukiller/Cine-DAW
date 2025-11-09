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
var configuracion = ValidarArgumentos(args);

// 1. Presentación del sistema
Console.WriteLine("=============================================");
Console.WriteLine("     🎬 INICIANDO SISTEMA CINE-DAM 🎟️       ");
Console.WriteLine("=============================================");
Console.WriteLine("Parámetros de la sala:");
Console.WriteLine($"\t- Filas: {configuracion.Filas}");
Console.WriteLine($"\t- Columnas: {configuracion.Columnas}");
Console.WriteLine();

// 2. Creación de la sala
var sala = new Butaca[configuracion.Filas, configuracion.Columnas];

// Inicializar todas las butacas como Libres
for (int i = 0; i < sala.GetLength(0); i++) {
    for (int j = 0; j < sala.GetLength(1); j++) {
        sala[i, j] = Butaca.Libre;
    }
}

// Elegir aleatoriamente entre 1 y 3 butacas para poner FueraDeServicio
int cantidadFueraServicio = random.Next(1, 4); // entre 1 y 3

for (int k = 0; k < cantidadFueraServicio; k++) {
    int filaAleatoria, columnaAleatoria;

    do {
        filaAleatoria = random.Next(0, sala.GetLength(0));
        columnaAleatoria = random.Next(0, sala.GetLength(1));
    } while (sala[filaAleatoria, columnaAleatoria] == Butaca.FueraDeServicio);

    sala[filaAleatoria, columnaAleatoria] = Butaca.FueraDeServicio;
}



// 5. Finalización
Console.WriteLine("\n👋 Gracias por usar Cine-DAM. Presiona una tecla para salir...");
Console.ReadKey();
return;

// ----------------------------------------------------
// FUNCIONES Y PROCEDIMIENTOS AUXILIARES
// ----------------------------------------------------
Configuracion ValidarArgumentos(string[] args) {
    if (args.Length != 2) {
        Console.WriteLine("Error los argumentos a ingresar son: Filas 4-7 Columnas 5-9");
        return PedirConfiguracion();
    }
    var filas = args[0];
    if ((filas.Length < 4 || filas.Length > 7 || !int.TryParse(filas, out var filasParsed) || (filasParsed< 4 || filasParsed > 7))) {
        Console.WriteLine($"Error: El argumento {args[0]} no es valido. Debe ser un numero de fila entre 4 y 7 incluidos");
        return PedirConfiguracion();
    }
    var columnas = args[1];
    if ((columnas.Length < 5 && columnas.Length > 9 || !int.TryParse(columnas, out var columnasParsed) || (columnasParsed< 5 || columnasParsed > 9))) {
        Console.WriteLine($"Error: El argumento {args[1]} no es valido. Debe ser un numero de columnas entre 5 y 9 incluidos");
        return PedirConfiguracion();
    }
    return new Configuracion {
        Filas = filasParsed,
        Columnas = columnasParsed
    };
}
Configuracion PedirConfiguracion() {
    Console.WriteLine("-- Configuracion");
    var regex = new Regex("^[4-7]{1}:[5-9]{1}$");
    var input = (Console.ReadLine() ?? "").Trim();
    while (!regex.IsMatch(input)) {
        Console.WriteLine("Error: Entrada inválida. Inténtalo de nuevo. Formato correcto: Fila 4-7 Columnas 5-9]");
        input = (Console.ReadLine() ?? "").Trim();
    }
    var match = regex.Match(input);
    var filas = int.Parse(args[0]);
    var columnas = int.Parse(args[1]);
    return new Configuracion {
        Filas = filas,
        Columnas = columnas
    };
}




struct Configuracion {
    public int Filas;
    public int Columnas;
}
struct Posicion {
    public int Fila;
    public int Columna;
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

const double PrecioEntrada = 6.50;

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

    ocuparButaca(sala, new Posicion{Fila = fila, Columna = columna});


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


double calcularRecaudacion(Butaca[,] sala) {
    var contador = 0;

    for (int i = 0; i < sala.GetLength(0); i++) {
        for (int j = 0; j < sala.GetLength(1); j++) {
            if (sala[i, j] == Butaca.Ocupada) {
                contador++;
            }
        }
    }
    return contador * PrecioEntrada;
}

void verInforme(Butaca[,] sala) {
    var contadorLibre = 0;
    var contadorOcupada = 0;
    var contadorFueraDeServicio = 0;
    var total = 0;

    for (int i = 0; i < sala.GetLength(0); i++) {
        for (int j = 0; j < sala.GetLength(1); j++) {
            if (sala[i, j] == Butaca.Libre) {
                contadorLibre++;
            }  else if (sala[i, j] == Butaca.Ocupada) {
                contadorOcupada++;
            } else if (sala[i, j] == Butaca.FueraDeServicio) {
                contadorFueraDeServicio++;
            }
            else {
                Console.WriteLine("No hay mas butacas");
            }
        }
    }
    
    total = sala.GetLength(0) * sala.GetLength(1);
    double porcentajeOcupacion = (double)contadorOcupada / total * 100;
    double porcentajeLibres = (double)contadorLibre / total * 100;
    double porcentajeFueraServicio = (double)contadorFueraDeServicio / total * 100;
    double recaudacion = calcularRecaudacion(sala);
    
    Console.WriteLine("\n📊 INFORME DE LA SALA");
    Console.WriteLine("----------------------");
    Console.WriteLine($"Total de butacas: {total}");
    Console.WriteLine($"🟢 Libres: {contadorLibre}");
    Console.WriteLine($"🔴 Ocupadas: {contadorOcupada}");
    Console.WriteLine($"🚫 Fuera de servicio: {contadorFueraDeServicio}");
    Console.WriteLine($"💰 Recaudación total: {recaudacion.ToString("F2")} €");
    Console.WriteLine();
    

    Console.WriteLine("📈 Porcentaje de ocupación: " + porcentajeOcupacion.ToString("F2") + " %");
    Console.WriteLine("📉 Porcentaje de butacas libres: " + porcentajeLibres.ToString("F2") + " %");
    Console.WriteLine("⚠️ Porcentaje fuera de servicio: " + porcentajeFueraServicio.ToString("F2") + " %");
    Console.WriteLine("\n✅ Informe generado correctamente.");

    

}






