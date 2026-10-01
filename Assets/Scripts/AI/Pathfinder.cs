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
                string current = stack.Pop();
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
                string current = queue.Dequeue();
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
            var openSet = new PriorityQueue<string, int>();
            var cameFrom = new Dictionary<string, string>();
            var gScore = new Dictionary<string, int> { [start] = 0 };
            var fScore = new Dictionary<string, int> { [start] = Heuristic(start, goal) };

            openSet.Enqueue(start, fScore[start]);

            while (openSet.Count > 0)
            {
                string current = openSet.Dequeue();
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
                    int tentativeScore = gScore[current] + 1;
                    if (!gScore.ContainsKey(neighbor) || tentativeScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeScore;
                        fScore[neighbor] = tentativeScore + Heuristic(neighbor, goal);
                        if (!openSet.UnorderedItems.Any(item => item.Element == neighbor))
                        {
                            openSet.Enqueue(neighbor, fScore[neighbor]);
                        }
                    }
                }
            }

            return ReconstructPath(cameFrom, start, goal);
        }

        private static int Heuristic(string node, string goal)
        {
            return Mathf.Abs(node.Length - goal.Length);
        }

        private static List<string> ReconstructPath(Dictionary<string, string> cameFrom, string start, string goal)
        {
            var result = new List<string>();
            string current = goal;

            while (current != start)
            {
                result.Add(current);
                if (!cameFrom.ContainsKey(current))
                {
                    return new List<string> { start, goal };
                }

                current = cameFrom[current];
            }

            result.Add(start);
            result.Reverse();
            return result;
        }
    }
}
