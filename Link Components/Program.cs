using System;
using System.Collections.Generic;

class Graph
{
    public int V;
    public List<int>[] adj; // Используется для представления графа
    public List<int>[] revAdj; // Используется для хранения транспонированного графа

    public Graph(int v)
    {
        V = v;
        adj = new List<int>[v];
        revAdj = new List<int>[v];
        for (int i = 0; i < v; i++)
        {
            adj[i] = new List<int>();
            revAdj[i] = new List<int>();
        }
    }

    public void AddEdge(int a, int b)
    {
        adj[a].Add(b);
        revAdj[b].Add(a); // Добавляем ребро в транспонированный граф
    }

    private void FillOrder(int v, bool[] visited, Stack<int> stack)
    {
        visited[v] = true;
        foreach (int n in adj[v])
        {
            if (!visited[n])
                FillOrder(n, visited, stack);
        }
        stack.Push(v);
    }

    private void DFSUtil(int v, bool[] visited, List<int> component)
    {
        visited[v] = true;
        component.Add(v);
        foreach (int n in revAdj[v])
        {
            if (!visited[n])
                DFSUtil(n, visited, component);
        }
    }

    public void FindSCCs()
    {
        Stack<int> stack = new Stack<int>();
        bool[] visited = new bool[V];

        // Заполнение стека порядком завершения вершин
        for (int i = 0; i < V; i++)
        {
            if (!visited[i])
                FillOrder(i, visited, stack);
        }

        // Обнуление массива посещенных вершин для второго прохода
        for (int i = 0; i < V; i++)
            visited[i] = false;

        // Обработка всех вершин в порядке убывания значения времени окончания
        while (stack.Count != 0)
        {
            int v = stack.Pop();

            if (!visited[v])
            {
                List<int> component = new List<int>();
                DFSUtil(v, visited, component);
                Console.WriteLine(string.Join(" ", component));
            }
        }
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Graph gr = new Graph(6);
        gr.AddEdge(0, 2);
        gr.AddEdge(2, 3);
        gr.AddEdge(3, 0);
        gr.AddEdge(2, 4);
        gr.AddEdge(4, 5);
        gr.AddEdge(5, 4);

        gr.FindSCCs();
    }
}
