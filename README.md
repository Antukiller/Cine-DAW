🎬 Cine-DAM
Cine-DAM es una aplicación de consola desarrollada en C# que simula la gestión de una sala de cine. El objetivo principal es permitir al usuario interactuar con una matriz de butacas, donde puede visualizar su estado, comprar entradas, devolverlas y consultar estadísticas de ocupación y recaudación.

Al iniciar el programa, se configura la sala con un número de filas y columnas determinado. Si no se proporcionan argumentos válidos, el sistema solicita los datos por consola y valida que estén dentro del rango permitido.

Cada butaca puede estar libre, ocupada o fuera de servicio. Al comenzar, todas las butacas están libres, excepto entre una y tres que se marcan aleatoriamente como fuera de servicio. El usuario puede ver el estado de la sala representado con símbolos visuales, donde las filas se identifican con letras y las columnas con números.

La interacción se realiza mediante coordenadas en formato letra:número (por ejemplo, B:3). El sistema valida que el formato sea correcto y que la coordenada esté dentro de los límites. Si la butaca está libre, se puede comprar; si está ocupada, se puede devolver; si está fuera de servicio, no se puede modificar.

El menú principal ofrece opciones para ver la sala, comprar o devolver entradas, consultar la recaudación y generar un informe estadístico. Este informe muestra el número de entradas vendidas, asientos libres, asientos fuera de servicio, porcentaje de ocupación y total recaudado.

El proyecto está desarrollado en C# sobre la plataforma .NET y se recomienda usar JetBrains Rider como entorno de desarrollo. Para ejecutarlo, solo necesitas clonar el repositorio, abrir el proyecto y ejecutar el archivo Program.cs.

Este proyecto está bajo licencia MIT, lo que permite su uso, modificación y distribución libremente.
