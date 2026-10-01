using System.Collections.Generic;
using UnityEngine;

namespace QuizKnightIA.Gameplay
{
    [System.Serializable]
    public class QuestionData
    {
        public string question;
        public string[] options;
        public int correctIndex;
        public string explanation;
    }

    public class QuestionManager : MonoBehaviour
    {
        private readonly Dictionary<string, List<QuestionData>> questionPool = new Dictionary<string, List<QuestionData>>();
        private readonly HashSet<string> usedQuestions = new HashSet<string>();

        private void Start()
        {
            LoadQuestions();
        }

        public void LoadQuestions()
        {
            string[] keys = { "Programacion", "Circuitos", "Redes", "BasesDatos", "Electronica", "IA" };
            foreach (var key in keys)
            {
                TextAsset asset = Resources.Load<TextAsset>("Questions/" + key);
                if (asset == null)
                {
                    continue;
                }

                var items = JsonUtility.FromJson<QuestionList>("{\"items\":" + asset.text + "}").items;
                questionPool[key] = new List<QuestionData>(items);
            }
        }

        public QuestionData GetRandomQuestion(string category)
        {
            if (!questionPool.ContainsKey(category) || questionPool[category].Count == 0)
            {
                return null;
            }

            List<QuestionData> pool = questionPool[category];
            int index = Random.Range(0, pool.Count);
            var selected = pool[index];
            string marker = category + selected.question;

            if (usedQuestions.Contains(marker) && pool.Count > 1)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    var candidate = pool[(index + i + 1) % pool.Count];
                    string candidateMarker = category + candidate.question;
                    if (!usedQuestions.Contains(candidateMarker))
                    {
                        usedQuestions.Add(candidateMarker);
                        return candidate;
                    }
                }
            }

            usedQuestions.Add(marker);
            return selected;
        }

        public void RegisterAnswer(bool correct)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegisterAnswer(correct);
            }
        }

        [System.Serializable]
        private class QuestionList
        {
            public QuestionData[] items;
        }
    }
}
