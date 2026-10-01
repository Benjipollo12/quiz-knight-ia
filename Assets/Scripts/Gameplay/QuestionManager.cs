using System.Collections.Generic;
using System.IO;
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
            string[] files = Directory.GetFiles(Path.Combine(Application.dataPath, "Resources/Questions"), "*.json");

            foreach (var file in files)
            {
                string raw = File.ReadAllText(file);
                var questions = JsonHelper.FromJson<QuestionData>(raw);
                string key = Path.GetFileNameWithoutExtension(file);
                questionPool[key] = new List<QuestionData>(questions);
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
            string key = category + selected.question;

            if (usedQuestions.Contains(key) && pool.Count > 1)
            {
                for (int i = 0; i < pool.Count; i++)
                {
                    var candidate = pool[(index + i + 1) % pool.Count];
                    string candidateKey = category + candidate.question;
                    if (!usedQuestions.Contains(candidateKey))
                    {
                        usedQuestions.Add(candidateKey);
                        return candidate;
                    }
                }
            }

            usedQuestions.Add(key);
            return selected;
        }

        public void RegisterAnswer(bool correct)
        {
            GameManager.Instance.RegisterAnswer(correct);
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string wrapped = json.Trim();
            if (wrapped.StartsWith("[") && wrapped.EndsWith("]"))
            {
                return JsonUtility.FromJson<Wrapper<T>>("{\"Items\":" + wrapped + "}").Items;
            }

            return new T[] { JsonUtility.FromJson<T>(wrapped) };
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
