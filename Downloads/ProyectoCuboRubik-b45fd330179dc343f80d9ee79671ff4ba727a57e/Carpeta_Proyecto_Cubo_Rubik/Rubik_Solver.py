import numpy as np
from collections import Counter

class CuboRubik:  
    def __init__(self, arriba=np.zeros((3, 3), dtype=object), 
                 frente=np.zeros((3, 3), dtype=object), 
                 izquierda=np.zeros((3, 3), dtype=object), 
                 derecha=np.zeros((3, 3), dtype=object),
                 atras=np.zeros((3, 3), dtype=object),
                 abajo=np.zeros((3, 3), dtype=object)):
        self.__cubo = np.array([arriba, frente, izquierda, derecha, atras, abajo])

    @property
    def cubo(self):
        return self.__cubo

    @property
    def arriba(self):
        return self.__cubo[0]

    @property
    def frente(self):
        return self.__cubo[1]

    @property
    def izquierda(self):
        return self.__cubo[2]

    @property
    def derecha(self):
        return self.__cubo[3]

    @property
    def atras(self):
        return self.__cubo[4]

    @property
    def abajo(self):
        return self.__cubo[5]

    def mostrar_sector(self, lado):
        for fila in lado:
            print(" ".join(map(str, fila)))
        print("-----------------------")

    def mostrar_cubo(self):
        print('arriba')
        self.mostrar_sector(self.arriba)
        print('frente')
        self.mostrar_sector(self.frente)
        print('izquierda')
        self.mostrar_sector(self.izquierda)
        print('derecha')
        self.mostrar_sector(self.derecha)
        print('atras')
        self.mostrar_sector(self.atras)
        print('abajo')
        self.mostrar_sector(self.abajo)

    def validacion_centros(self):
        centros = ["blanco", "verde", "naranja", "rojo", "azul", "amarillo"]
        return all(self.cubo[cara][1, 1].lower() == centros[cara] for cara in range(6))

    def validacion_colores(self):
        colores = Counter(self.cubo[cara, fila, columna] for cara in range(6) for fila in range(3) for columna in range(3))
        return len(colores) == 6 and all(count == 9 for count in colores.values())

    def validacion_cubo(self):
        return self.validacion_centros() and self.validacion_colores()

    def __eq__(self, otro_objeto):
        if isinstance(otro_objeto, CuboRubik):
            return np.array_equal(self.__cubo, otro_objeto.cubo)
        return False

    def insertar_datos_cubo(self, txt):
        with open(txt, 'r') as f:
            lineas = f.readlines()

        if len(lineas) != 18:
            print(f"Error en insertar valores: el archivo debe contener exactamente 18 líneas.")
            return

        for indice, linea in enumerate(lineas):
            casillas = linea.split()
            if len(casillas) < 3:  # Verifica si hay menos de 3 elementos en la fila
                print(f"Error en la línea {indice + 1}: debe tener al menos 3 elementos.")
                return

            self.cubo[indice // 3][indice % 3] = casillas

        self.validacion_cubo()


    def giro_horario(self, matrix):
        return np.rot90(matrix, 3)

    def giro_antihorario(self, matrix):
        return np.rot90(matrix)

    def girar_pieza(self, giro):
        giros = {
            "Up": self.Up, "Up'": self.Up, "Up2": self.UpP,
            "Front": self.Front, "Front'": self.Front, "Front2": self.FrontP,
            "Right": self.Right, "Right'": self.Right, "Right2": self.RightP,
            "Left": self.Left, "Left'": self.Left, "Left2": self.LeftP,
            "Down": self.Down, "Down'": self.Down, "Down2": self.DownP,
            "Back": self.Back, "Back'": self.Back, "Back2": self.BackP
        }
        giros[giro]()

    def Up(self):
        self.__cubo[0] = self.giro_horario(self.arriba)

    def UpP(self):
        self.Up(); self.Up()

    def Front(self):
        self.arriba[2], self.abajo[0] = self.izquierda[0], self.derecha[2]
        for i in range(3):
            self.izquierda[i][0], self.derecha[i][2] = self.abajo[i][2], self.arriba[i][0]
        self.frente = self.giro_horario(self.frente)

    def FrontP(self):
        self.Front(); self.Front()

    def Right(self):
        self.arriba, self.derecha[0], self.abajo, self.izquierda[2] = \
            self.giro_horario(self.izquierda[2]), self.arriba, self.giro_antihorario(self.izquierda[0]), self.derecha[0]
        self.derecha = self.giro_horario(self.derecha)

    def RightP(self):
        self.Right(); self.Right()

    def Left(self):
        self.Right(); self.Right(); self.Right()

    def LeftP(self):
        self.Left(); self.Left()

    def Down(self):
        self.Up(); self.Up(); self.Up()

    def DownP(self):
        self.Down(); self.Down()

    def Back(self):
        columnas_ady_U = [fila[0] for fila in self.arriba[::-1]]
        columnas_ady_D = [fila[2] for fila in self.abajo]
        self.arriba[0] = self.derecha[2]
        self.abajo[2] = self.izquierda[0]
        for i in range(3):
            self.derecha[i][2] = columnas_ady_U[i]
            self.izquierda[i][0] = columnas_ady_D[i]
        self.atras = self.giro_horario(self.atras)

    def BackP(self):
        self.Back(); self.Back(); self.Back()