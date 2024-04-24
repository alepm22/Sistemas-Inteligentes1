# ProyectoCuboRubik
1. **Autor:** Alejandra Palza Morales
2. **Descripcion del proyecto**

    El proyecto de Sistemas Inteligentes se enfoca en crear un sistema avanzado para resolver el Cubo de Rubik de manera óptima. Se emplearán algoritmos de búsqueda en espacio de estados junto con técnicas de optimización y heurísticas para encontrar la secuencia de movimientos más corta que lleve al cubo a su estado resuelto. En el proyecto también se vera diferentes estrategias de búsqueda para evaluar su efectividad y eficiencia.

3. **Requerimientos del entorno de programación**
    Python 3.x
    Bibliotecas:numpy

4. **Manual de uso**
    El programa permite cargar un estado inicial del cubo Rubik desde un archivo de texto y luego resolverlo utilizando un algoritmo de búsqueda.
    1. **Formato de carga del cubo para un archivo de texto** \
        - Cada línea del archivo representa una fila del cubo.
        - Cada fila contiene los colores de las casillas separados por - espacios.

        **EJEMPLO**
        rojo azul rojo
        rojo blanco verde
        blanco verde blanco
        azul rojo azul
    
    2. **.Instrucciones para ejecutar el programa**
        - Descarga el archivo "Cubo_Desordenado.txt" con el estado inicial del cubo.
        - Ejecuta el programa "main.py".
        - El programa mostrará el estado inicial del cubo y luego buscará la solución para resolverlo.

5. **Diseño e implementación**
    5.1. **descripción de modelo del problema** \
    El problema consiste en resolver un cubo Rubik, donde cada cara tiene nueve casillas de diferentes colores y el objetivo es llevar el cubo a un estado donde todas las caras estén completamente formadas por un solo color.

    5.2. **Explicación y justificación de algoritmo(s), técnicas, heurísticas seleccionadas:** \
    - Se utilizó el algoritmo de búsqueda A* para encontrar la solución al cubo Rubik.
    - La heurística utilizada es la suma de las diferencias entre el estado actual y el estado objetivo del cubo, es decir, la cantidad de casillas que no están en su lugar correcto.
    - Se implementó una clase Nodo para representar los estados del cubo en el árbol de búsqueda.
    5.3. **Promps utilizados para la codificacion**\
    Para una codificacion se utilizo 3 promps consultados a ChatGPT:\
    - Solicitud de formato de sugererncias para el formato de - codificacion
    - Consulta sobre tecnicas de resolucion
    - Consulta sobre algoritmos especificos
  
    
6. **Trabajo futuro**

    Implementar otras técnicas de resolución de cubos Rubik, como el algoritmo CFOP.
    Mejorar la interfaz gráfica para una mejor experiencia de usuario.
    Expandir el programa para resolver cubos de dimensiones mayores.
    Refactorizar el cdoigo en partes mas peqeuñas.

        