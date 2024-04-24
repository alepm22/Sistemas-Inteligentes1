import numpy as np
from collections import Counter
import heapq


class CuboRubik:
    def __init__(self, config=None):
        if config is None:
            config = [
                np.full((3, 3), 'A'),  
                np.full((3, 3), 'F'),  
                np.full((3, 3), 'L'),  
                np.full((3, 3), 'R'),  
                np.full((3, 3), 'B'),  # Atrás
                np.full((3, 3), 'D')   # Abajo
            ]
        self.cubo = np.array(config)
        self.validar_cubo()

   

    def validar_cubo(self):
        centros = [self.cubo[i][1][1] for i in range(6)]
        if len(set(centros)) != 6:
            raise ValueError("Error: Los centros del cubo no son únicos.")
        colores = Counter(self.cubo.flatten())
        if any(color != ' ' and cantidad != 9 for color, cantidad in colores.items()):
            raise ValueError("Error: Falta o sobra un color en alguna cara del cubo.")

    def cargar_configuracion(self, archivo):
        try:
            with open(archivo, 'r') as file:
                lines = [line.strip().split() for line in file if line.strip()]
                if len(lines) != 18:
                    raise ValueError("Error: El archivo no tiene 18 líneas.")
                self.cubo = np.array(lines).reshape((6, 3, 3))
                self.validar_cubo()
        except Exception as e:
            print(f"Error al cargar la configuración: {e}")

    def resolver(self):
        objetivo = CuboRubik()
        solucion = self.buscar_solucion(objetivo)
        if solucion:
            print("Secuencia de Movimientos para Resolver el Cubo:")
            for i, mov in enumerate(solucion, 1):
                print(f"Paso {i}: {mov}")
            print("El cubo ha sido resuelto!")
        else:
            print("No se encontró una solución.")

    def buscar_solucion(self, objetivo):
        frontera = []
        inicio = Nodo(self.cubo, None, None, 0, self.heuristica(self.cubo, objetivo.cubo))
        heapq.heappush(frontera, inicio)
        explorados = set()

        while frontera:
            current = heapq.heappop(frontera)
            if np.array_equal(current.estado, objetivo.cubo):
                return self.reconstruir_camino(current)

            explorados.add(hash(str(current.estado)))

            for movimiento in self.movimientos_posibles():
                nuevo_estado = self.aplicar_movimiento(current.estado, movimiento)
                if hash(str(nuevo_estado)) in explorados:
                    continue
                nuevo_nodo = Nodo(nuevo_estado, current, movimiento, current.g + 1, self.heuristica(nuevo_estado, objetivo.cubo))
                heapq.heappush(frontera, nuevo_nodo)
        return None


    def reconstruir_camino(self, nodo):
        camino = []
        while nodo.padre:
            camino.append(nodo.movimiento)
            nodo = nodo.padre
        return camino[::-1]

    def heuristica(self, estado, objetivo):
        return np.sum(estado != objetivo)

    def movimientos_posibles(self):
        return ["U", "U'", "F", "F'", "R", "R'", "L", "L'", "B", "B'", "D", "D'"]

    def aplicar_movimiento(self, estado, movimiento):
        nuevo_estado = np.copy(estado)

        if movimiento == "U":
            nuevo_estado[0] = np.rot90(estado[0], -1)
        elif movimiento == "U'":
            nuevo_estado[0] = np.rot90(estado[0], 1)
        return nuevo_estado


class Nodo:
    def __init__(self, estado, padre, movimiento, g, h):
        self.estado = estado
        self.padre = padre
        self.movimiento = movimiento
        self.g = g
        self.h = h
        self.f = g + h

    def __lt__(self, other):
        return self.f < other.f

cubo = CuboRubik()
#cubo.cargar_configuracion("C:\Users\ALEJANDRA\Downloads\ProyectoCuboRubik-b45fd330179dc343f80d9ee79671ff4ba727a57e\Carpeta_Proyecto_Cubo_Rubik\Cubo_Desordenado.txt")
cubo.cargar_configuracion("C:\\Users\\ALEJANDRA\\Downloads\\ProyectoCuboRubik-b45fd330179dc343f80d9ee79671ff4ba727a57e\\Carpeta_Proyecto_Cubo_Rubik\\Validar_Cubo_Centro.txt")
#cubo.mostrar_cubo()
cubo = CuboRubik()
cubo.resolver()