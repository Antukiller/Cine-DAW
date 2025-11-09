# 🎬 CINE-DAM: Gestión Modular de Sala de Cine en C#

**CINE-DAM** es una aplicación de consola desarrollada en C# que simula la gestión de una sala de cine. El sistema permite visualizar el estado de las butacas, realizar compras y devoluciones, calcular la recaudación y generar informes estadísticos, todo con una interfaz robusta y validaciones estrictas.

## 📌 Objetivo

Crear una solución modular que gestione una sala de cine mediante matrices, garantizando la integridad de los datos y ofreciendo una experiencia clara y funcional por consola.

## 🧱 Características Principales

### 1. ⚙️ Configuración Inicial y Validación

- La dimensión de la sala (Filas:Columnas) se intenta leer desde los argumentos de línea de comandos.
- Si los argumentos faltan o son inválidos, se solicita la entrada por consola en formato `F:C`.
- Validaciones:
  - Filas: entre **4 y 7**
  - Columnas: entre **5 y 9**
  - Formato correcto: `Número:Numero` (ej. `5:8`)
- Ejemplo de interacción:
Bienvenido a CINEMAD. ERROR: Faltan argumentos. Formato de ejecución: -filas:X -columnas:Y
--- Modo Consola de Respaldo ---
Introduzca las dimensiones de la sala (F:C). Rango permitido: [4-7]:[5-9]


### 2. 🎟️ Representación de Butacas

Cada butaca tiene un estado y un precio fijo:

| Estado           | Valor Interno | Símbolo | Precio     |
|------------------|---------------|---------|------------|
| Libre            | 0             | [🟢]    | N/A        |
| Ocupada          | 1             | [🔴]    | 6,50€      |
| Fuera de servicio| 2             | [🚫]    | N/A        |

- Al iniciar, todas las butacas están libres excepto entre 1 y 3 que se marcan aleatoriamente como fuera de servicio.

### 3. 🗺️ Visualización de la Sala

- Coordenadas mixtas: **Filas con letras (A, B, C...)** y **Columnas con números (1, 2, 3...)**
- Ejemplo de salida:
1 2 3 4 5 A [🟢] [🟢] [🔴] [🟢] [🟢] B [🚫] [🟢] [🟢] [🟢] [🔴] C [🟢] [🟢] [🔴] [🚫] [🟢]


### 4. 🎯 Entrada de Coordenadas

- Formato requerido: `Letra:Numero` (ej. `B:3`)
- Validaciones:
- Formato correcto
- Coordenada dentro de los límites
- Ejemplo de errores:
- `A-5` → ❌ ERROR: Formato incorrecto
- `Z:9` → ❌ ERROR: Coordenada fuera de los límites

### 5. 📋 Menú Principal

El programa opera en un bucle con las siguientes opciones:

| Opción | Acción              | Resultado Esperado |
|--------|---------------------|---------------------|
| 1      | Ver Sala            | Muestra la matriz de butacas |
| 2      | Comprar Entrada     | Solicita coordenada y marca como ocupada |
| 3      | Devolver Entrada    | Solicita coordenada y marca como libre |
| 4      | Recaudación         | Muestra el total recaudado |
| 5      | Informe             | Muestra estadísticas completas |
| 6      | Salir               | Finaliza el programa |

### 6. 📊 Informe Estadístico

La opción 5 genera un informe con:

- Entradas Vendidas
- Asientos Libres
- Asientos No Disponibles
- Porcentaje de Ocupación
- Recaudación Total

Ejemplo de salida:
--- INFORME CINEMAD --- Entradas Vendidas: 5 Asientos Libres: 18 Asientos No Disponibles (F/S): 2 Ocupación: 21.74% (sobre 23 asientos disponibles) Recaudación Total: 32.50€



## 🛠️ Tecnologías Utilizadas

- Lenguaje: **C#**
- Plataforma: **.NET Console Application**
- IDE: **JetBrains Rider**

## 🚀 Cómo Ejecutar

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Antukiller/Cine-DAM.git
