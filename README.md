# 3D Maze Navigation: AI vs Human Performance

## Descripción del Proyecto

Proyecto de investigación que compara el rendimiento de navegación en laberintos entre un agente autónomo utilizando el algoritmo de Búsqueda por Anchura (BFS) y participantes humanos en entornos de realidad virtual inmersivos.

## Características Principales

- Generación de laberintos 3D utilizando Unity
- Algoritmo de navegación autónoma Búsqueda por Anchura (BFS)
- Interacción de Realidad Virtual (VR) con Oculus Quest 2
- Análisis comparativo de rendimiento entre agente de IA y jugadores humanos

## Metodología

### Generación de Laberintos
- Implementado mediante matrices 2D que representan caminos y paredes
- Generación dinámica de entornos de laberintos 3D
- Tres niveles de complejidad: Laberintos Pequeño, Mediano y Grande

### Estrategias de Navegación
- Algoritmo BFS: Exploración sistemática de todas las rutas posibles
- Navegación Humana: Búsqueda de rutas intuitiva en entorno de realidad virtual

## Resultados Experimentales

### Comparación de Rendimiento

| Tamaño de Laberinto | Tiempo Agente BFS | Tiempo Jugador Humano |
|---------------------|-------------------|----------------------|
| Pequeño             | 00:36.25          | 00:45.23             |
| Mediano             | 05:50.51          | 07:29.52             |
| Grande              | 11:33.46          | 16:44.07             |

### Hallazgos Principales
- El agente BFS supera consistentemente a los jugadores humanos
- La diferencia de rendimiento aumenta con la complejidad del laberinto
- La exploración algorítmica sistemática resulta más eficiente que la navegación intuitiva

## Instalación de Paquetes de Unity

### Paquetes Esenciales
1. **Oculus Integration**
   - Versión: 49.0 o superior
   - Descargar desde: Unity Asset Store
   - Componentes clave: 
     * XR Managment (Oculus - Android)
     * Meta All In One SDK

2. **TextMeshPro**
   - Incluido en paquetes estándar de Unity
   - Necesario para interfaz de usuario

3. **AI Navigation**
   - Para sistemas de navegación autónoma
   - Permite movimiento del agente en el laberinto
     
4. **TMP Essentials, Examples & Extras**
   - Creación e interacción con Canvas
   
   
### Configuración Adicional
- Unity versión 2022 LTS o superior
- .NET Framework 4.x 
- Soporte para realidad virtual habilitado

### Dependencias Externas
- Oculus Quest 2 SDK

## Tecnologías Utilizadas
- Unity 3D
- Oculus Quest 2
- Algoritmo de Búsqueda por Anchura (BFS)

## Requisitos del Sistema
- Sistema Operativo: Windows 10/11 (64-bit)
- Procesador: Intel Core i5 o superior
- Memoria: 16 GB RAM
- Gráficos: NVIDIA GeForce GTX 1060 o superior
- Espacio en disco: 10 GB
- Dispositivo VR: Oculus Quest 2

## Instrucciones de Instalación

1. Clonar el repositorio
   
3. Abrir proyecto en Unity
- Asegurarse de tener instalados todos los paquetes mencionados

3. Configurar integración de Oculus Quest 2
- Conectar dispositivo
- Configurar permisos de desarrollador

4. Compilar y ejecutar el proyecto

## Contribuidores
- Jeremy Lewis Herrera Acuña
- Anderson Joseph Ochoa Trujillo
- Juan Pablo Hoyos Sanchez


