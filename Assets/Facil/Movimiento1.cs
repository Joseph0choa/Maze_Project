using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public Transform goal; // El objetivo
    public MazeOptimization mazeGenerator; // Referencia a la generación del laberinto

    private Queue<Vector3> bfsQueue; // Cola para BFS
    private HashSet<Vector3> visitedNodes; // Conjunto de nodos visitados
    private Dictionary<Vector3, Vector3> parentNodes; // Diccionario para almacenar el nodo padre de cada nodo visitado

    private Vector3[] directions = new Vector3[]
    {
        new Vector3(1, 0, 0),  // Derecha
        new Vector3(-1, 0, 0), // Izquierda
        new Vector3(0, 0, 1),  // Arriba
        new Vector3(0, 0, -1)  // Abajo
    };

    private float cellSize = 1.0f; // Tamaño de las celdas del laberinto
    private float safeDistance = 0.3f; // Distancia mínima a las paredes para evitar colisiones

    private void Start()
    {
        bfsQueue = new Queue<Vector3>();
        visitedNodes = new HashSet<Vector3>();
        parentNodes = new Dictionary<Vector3, Vector3>();
        StartCoroutine(PerformBFS());
    }

    private IEnumerator PerformBFS()
    {
        // Iniciar BFS desde la posición del agente
        bfsQueue.Clear();
        visitedNodes.Clear();
        parentNodes.Clear();
        bfsQueue.Enqueue(GetCellCenter(transform.position)); // Nodo inicial es la posición central de la celda actual
        visitedNodes.Add(GetCellCenter(transform.position));
        parentNodes[GetCellCenter(transform.position)] = transform.position; // El nodo inicial no tiene un padre

        Debug.Log("Iniciando BFS desde: " + transform.position);

        // BFS para encontrar el camino hacia el objetivo
        while (bfsQueue.Count > 0)
        {
            Vector3 currentNode = bfsQueue.Dequeue();
            Debug.Log("Visitando nodo: " + currentNode);

            // Verifica si hemos llegado al nodo objetivo
            if (Vector3.Distance(currentNode, GetCellCenter(goal.position)) < cellSize)
            {
                Debug.Log("¡Objetivo alcanzado!");
                DrawPath(currentNode); // Dibuja el camino
                StartCoroutine(FollowPath(currentNode)); // Mueve al agente
                yield break;
            }

            // Explorar los nodos vecinos en las 4 direcciones
            foreach (Vector3 dir in directions)
            {
                Vector3 neighbor = currentNode + dir * cellSize;

                if (IsValidNode(neighbor) && !visitedNodes.Contains(neighbor))
                {
                    bfsQueue.Enqueue(neighbor);
                    visitedNodes.Add(neighbor);
                    parentNodes[neighbor] = currentNode;
                }
            }
        }

        Debug.Log("No se encontró un camino.");
    }

    private void DrawPath(Vector3 currentNode)
    {
        // Dibuja el camino desde el nodo final hacia el inicio
        List<Vector3> path = new List<Vector3>();
        Vector3 pathNode = currentNode;

        while (pathNode != transform.position)
        {
            path.Add(pathNode);
            pathNode = parentNodes[pathNode];
        }
        path.Add(transform.position); // Añade el nodo de inicio al final

        // Dibuja el camino
        path.Reverse(); // Reversa el camino para dibujarlo de inicio a fin
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i], path[i + 1], Color.green, 10f); // Dibuja una línea en verde durante 10 segundos
        }
    }

    private bool IsValidNode(Vector3 node)
    {
        // Verificar si el nodo está dentro del laberinto y es transitable
        int x = Mathf.RoundToInt(node.x);
        int z = Mathf.RoundToInt(node.z);

        if (x >= 0 && x < mazeGenerator.maze.GetLength(0) && z >= 0 && z < mazeGenerator.maze.GetLength(1))
        {
            // Nodo válido si está lejos de las paredes
            return mazeGenerator.maze[x, z] == 0 && IsFarFromWalls(node);
        }

        return false;
    }

    private bool IsFarFromWalls(Vector3 node)
    {
        // Verifica que el nodo esté a una distancia segura de las paredes
        foreach (Vector3 dir in directions)
        {
            Vector3 neighbor = node + dir * safeDistance;

            int x = Mathf.RoundToInt(neighbor.x);
            int z = Mathf.RoundToInt(neighbor.z);

            if (x < 0 || x >= mazeGenerator.maze.GetLength(0) || z < 0 || z >= mazeGenerator.maze.GetLength(1))
                continue;

            if (mazeGenerator.maze[x, z] == 1) // Si hay una pared cerca
                return false;
        }
        return true;
    }

    private Vector3 GetCellCenter(Vector3 position)
    {
        // Ajusta la posición al centro de la celda más cercana
        int x = Mathf.RoundToInt(position.x / cellSize) * Mathf.RoundToInt(cellSize);
        int z = Mathf.RoundToInt(position.z / cellSize) * Mathf.RoundToInt(cellSize);

        return new Vector3(x, position.y, z);
    }

    private IEnumerator FollowPath(Vector3 goalNode)
    {
        // Recorre el camino calculado y mueve al agente
        List<Vector3> path = new List<Vector3>();
        Vector3 pathNode = goalNode;

        // Reconstruye el camino desde el objetivo hacia el inicio
        while (pathNode != transform.position)
        {
            path.Add(pathNode);
            pathNode = parentNodes[pathNode];
        }
        path.Add(transform.position); // Añade el nodo de inicio al final
        path.Reverse(); // Invierte el camino para recorrerlo de inicio a fin

        // Mueve al agente a lo largo del camino, centrado en las celdas
        foreach (Vector3 position in path)
        {
            Vector3 targetPosition = GetCellCenter(position);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 3f); // 3f es la velocidad de movimiento
                yield return null; // Espera un frame
            }
        }

        Debug.Log("Agente ha llegado al objetivo.");
    }
}