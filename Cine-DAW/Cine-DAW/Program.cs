// See https://aka.ms/new-console-template for more information

Configuracion ValidarArgumentosEntrada(string[] args) {
    // Validamos que haya dos argumentos
    if (args.Length != 2) {
        Console.WriteLine("❌ Error: Debe ingresar dos argumentos: fila:X columna:Y");
        return PedirConfiguracion();
    }

    // Validamos fila:X
    var fila = args[0].Split(':');
    if (fila.Length != 2 || !int.TryParse(fila[1], out var filaParsed) || filaParsed < 4 || filaParsed > 7) {
        Console.WriteLine($"❌ Error: El argumento '{args[0]}' no es válido. Debe ser fila:X, donde X es un entero entre 4 y 7.");
        return PedirConfiguracion();
    }

    // Validamos columna:Y
    var columna = args[1].Split(':');
    if (columna.Length != 2 || !int.TryParse(columna[1], out var columnaParsed) || columnaParsed < 5 || columnaParsed > 9) {
        Console.WriteLine($"❌ Error: El argumento '{args[1]}' no es válido. Debe ser columna:Y, donde Y es un entero entre 5 y 9.");
        return PedirConfiguracion();
    }

    // Si todo es correcto, retornamos la configuración
    return new Configuracion {
        Fila = filaParsed,
        Columna = columnaParsed
    }