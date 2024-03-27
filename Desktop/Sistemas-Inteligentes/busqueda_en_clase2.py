# Buenas prácticas y pequeños incrementos hacia la generalización

# 1. Renombrar clases y objetos de acuerdo al contexto de espacio de Estados
# 2. Eliminar código duplicado

# 3. Diseñar una clase para representar un EspacioDeEstados
#        Debe poder cargarse el grafo unidireccional de la practica #1
#        y el grafo bidireccional de la práctica #2 y #3

# 4. Ajusta las funciones de búsqueda para que
#       reciba un EspacioDeEstados, valor_inicial, valor_final
#       retornen el camino encontrado

class Node:
    def __init__(self, state, children=[]):
        self.state = state
        self.children = children

def depth_first_search(initial_node, goal_state):
    stack = [initial_node]
    while stack:
        current_node = stack.pop()
        print(current_node.state)
        if current_node.state == goal_state:
            print("Eureka!")
            return True
        for child in current_node.children:
            stack.append(child)
    return False

def breadth_first_search(initial_node, goal_state):
    queue = [initial_node]
    while queue:
        current_node = queue.pop(0)
        print(current_node.state)
        if current_node.state == goal_state:
            print("Eureka!")
            return True
        for child in current_node.children:
            queue.append(child)
    return False


if __name__ == "__main__":
    initial_node = Node("A",
        [
            Node("B", [
                Node("D"),
                Node("E")
            ]),
            Node("C", [
                Node("F")
            ])
        ]
    )

    print("Buscar en profundidad")
    depth_first_search(initial_node, 'E')

    print()

    print("Buscar en amplitud")
    breadth_first_search(initial_node, 'E')
