using System.Collections.Generic;
using UnityEngine;

namespace QuizKnightIA.AI
{
    public static class Pathfinder
    {
        public static List<string> DepthFirstSearch(Dictionary<string, List<string>> graph, string start, string goal)
        {
            var visited = new HashSet<string>();
            var stack = new Stack<string>();
            var path = new Dictionary<string, string>();

            stack.Push(start);
            visited.Add(start);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current == goal)
                {
                    break;
                }

                if (!graph.ContainsKey(current))
                {
                    continue;
                }

                foreach (var neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        path[neighbor] = current;
                        stack.Push(neighbor);
                    }
                }
            }

            return ReconstructPath(path, start, goal);
        }

        public static List<string> BreadthFirstSearch(Dictionary<string, List<string>> graph, string start, string goal)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            var path = new Dictionary<string, string>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == goal)
                {
                    break;
                }

                if (!graph.ContainsKey(current))
                {
                    continue;
                }

                foreach (var neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        path[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return ReconstructPath(path, start, goal);
        }

        public static List<string> AStar(Dictionary<string, List<string>> graph, string start, string goal)
        {
            var open = new List<string> { start };
            var cameFrom = new Dictionary<string, string>();
            var gScore = new Dictionary<string, int> { [start] = 0 };

            while (open.Count > 0)
            {
                var current = open[0];
                open.RemoveAt(0);

                if (current == goal)
                {
                    break;
                }

                if (!graph.ContainsKey(current))
                {
                    continue;
                }

                foreach (var neighbor in graph[current])
                {
                    var tentative = gScore[current] + 1;
                    if (!gScore.ContainsKey(neighbor) || tentative < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentative;
                        if (!open.Contains(neighbor))
                        {
                            open.Add(neighbor);
                        }
                    }
                }
            }

            return ReconstructPath(cameFrom, start, goal);
        }

        private static List<string> ReconstructPath(Dictionary<string, string> path, string start, string goal)
        {
            var result = new List<string>();
            var current = goal;

            while (current != start)
            {
                result.Add(current);
                if (!path.ContainsKey(current))
                {
                    return new List<string> { start, goal };
                }

                current = path[current];
            }

            result.Add(start);
            result.Reverse();
            return result;
        }
    }
}
