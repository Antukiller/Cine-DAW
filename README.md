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

#### ✅ Ejemplos de Interacción




---

### 2. 🎟️ Representación de Butacas

Cada butaca tiene un estado y un precio fijo:

| Estado             | Valor Interno | Símbolo | Precio |
|--------------------|---------------|---------|--------|
| Libre              | 0             | [🟢]    | 6,50€  |
| Ocupada            | 1             | [🔴]    | 6,50€  |
| Fuera de servicio  | 2             | [🚫]    | N/A    |

- Al iniciar, todas las butacas están libres excepto entre 1 y 3 que se marcan aleatoriamente como fuera de servicio.

---

### 3. 🗺️ Visualización de la Sala

- Coordenadas mixtas: **Filas con letras (A, B, C...)** y **Columnas con números (1, 2, 3...)**

#### Ejemplo de salida:



---

### 4. 🎯 Entrada de Coordenadas

- Formato requerido: `Letra:Numero` (ej. `B:3`)
- Validaciones:
  - Formato correcto
  - Coordenada dentro de los límites

#### Ejemplos de interacción:



---

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

#### Ejemplos de acciones:




---

### 6. 📊 Informe Estadístico

La opción 5 genera un informe con:

- Entradas Vendidas
- Asientos Libres
- Asientos No Disponibles
- Porcentaje de Ocupación
- Recaudación Total

#### Ejemplo de salida:



## 🛠️ Tecnologías Utilizadas

- Lenguaje: **C#**
- Plataforma: **.NET Console Application**
- IDE: **JetBrains Rider**

## 🚀 Cómo Ejecutar

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Antukiller/Cine-DAM.git
