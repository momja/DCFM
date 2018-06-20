using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDataStructure
{
    public class Graph
    {
        private int[][] adjecencyMatrix;
        private int[] vertices;

        public Graph()
        {
        }

        public int[] Vertices { get { return vertices; } }

        public int[][] AdjecencyMatrix { get { return adjecencyMatrix;  } }

        public void initGraph(int[][] edges)
        {
            vertices = edges.SelectMany(x => x).Distinct().OrderBy(x => x).ToArray();

            adjecencyMatrix = new int[vertices.Length][];
            for (int j = 0; j < vertices.Length; j++)
            {
                var adjecents = new List<int>();
                foreach(var row in edges)
                {
                    if (row.Contains(vertices[j]))
                        adjecents.Add(row.First(x => x != vertices[j]));
                }
                adjecencyMatrix[j] = adjecents.OrderBy(x => x).ToArray();
            }
        }

        public int[] getAdjecents(int c)
        {
            if (Array.IndexOf(vertices, c) < 0)
                throw new InvalidOperationException("Invalid vertex");

            return adjecencyMatrix[Array.IndexOf(vertices, c)];
        }

        //DFS algorithm
        public int[] Traverse()
        {
            var stack = new Stack<int>();
            var result = new List<int>();
            var c = Vertices[0];
            result.Add(c);
            stack.Push(c);
            while(stack.Count > 0)
            {
                var adjecents = getAdjecents(stack.Peek());
                if (adjecents.Any(x => !result.Contains(x)))
                {
                    c = adjecents.Where(x => !result.Contains(x)).ElementAt(0);
                    result.Add(c);
                    stack.Push(c);
                }
                else
                    stack.Pop();
            }
            return result.ToArray();
        }
    }
}
