using UnityEngine;

namespace QuizKnightIA.Controllers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public enum GameState
        {
            Idle,
            Playing,
            GameOver
        }

        [Header("Player Stats")]
        public int maxPlayerHealth = 50;
        public int currentPlayerHealth = 50;
        public int baseDamage = 8;
        public int totalDamageBonus = 0;
        public int maxQuestionHearts = 3;
        public int currentQuestionHearts = 3;

        [Header("Debug Stats")]
        public int bossesDefeated;
        public int zonesReached;
        public int questionsCorrect;
        public int questionsIncorrect;
        public float totalPlayTime;
        public int totalScore;

        public GameState state = GameState.Idle;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (state == GameState.Playing)
            {
                totalPlayTime += Time.deltaTime;
            }
        }

        public void StartGame()
        {
            currentPlayerHealth = maxPlayerHealth;
            bossesDefeated = 0;
            questionsCorrect = 0;
            questionsIncorrect = 0;
            totalScore = 0;
            totalPlayTime = 0f;
            currentQuestionHearts = maxQuestionHearts;
            state = GameState.Playing;
            zonesReached = 1;
            Debug.Log("Partida iniciada.");
        }

        public void RegisterBossDefeat()
        {
            bossesDefeated++;
            totalScore += 150;
            Debug.Log("Jefe derrotado.");
        }

        public void RegisterAnswer(bool correct)
        {
            if (correct)
            {
                questionsCorrect++;
                totalScore += 50;
            }
            else
            {
                questionsIncorrect++;
                currentQuestionHearts = Mathf.Max(0, currentQuestionHearts - 1);
                totalScore = Mathf.Max(0, totalScore - 25);
            }
        }

        public void ApplyDamage(int damage)
        {
            currentPlayerHealth = Mathf.Max(0, currentPlayerHealth - damage);
            if (currentPlayerHealth <= 0)
            {
                TriggerGameOver();
            }
        }

        public void TriggerGameOver()
        {
            state = GameState.GameOver;
            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowGameOverPanel();
            }
            Time.timeScale = 0f;
            Debug.Log("Game Over.");
        }

        public void Heal(int amount)
        {
            currentPlayerHealth = Mathf.Min(maxPlayerHealth, currentPlayerHealth + amount);
        }

        public float GetAccuracy()
        {
            int totalAnswers = questionsCorrect + questionsIncorrect;
            if (totalAnswers == 0)
            {
                return 0f;
            }

            return (float)questionsCorrect / totalAnswers * 100f;
        }
    }
}
