using System.Collections.Generic;
using UnityEngine;

namespace QuizKnightIA.Gameplay
{
    public class LevelGenerator : MonoBehaviour
    {
        public static List<string> GenerateZoneOrder()
        {
            var zones = new List<string>
            {
                "Programacion",
                "Circuitos",
                "Redes",
                "BasesDatos",
                "Electronica",
                "IA"
            };

            Shuffle(zones);
            return zones;
        }

        public static List<string> GeneratePathWithDFS(Dictionary<string, List<string>> graph, string start, string goal)
        {
            return Pathfinder.DepthFirstSearch(graph, start, goal);
        }

        public static List<string> GeneratePathWithBFS(Dictionary<string, List<string>> graph, string start, string goal)
        {
            return Pathfinder.BreadthFirstSearch(graph, start, goal);
        }

        private static void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
