using UnityEngine;
using UnityEngine.UI;

namespace QuizKnightIA.Controllers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public GameObject gameOverPanel;
        public Text gameOverText;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowGameOverPanel()
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }

            if (gameOverText != null && GameManager.Instance != null)
            {
                gameOverText.text = "GAME OVER\nNivel: " + GameManager.Instance.zonesReached +
                    "\nJefes: " + GameManager.Instance.bossesDefeated +
                    "\nRondas correctas: " + GameManager.Instance.questionsCorrect +
                    "\nRondas incorrectas: " + GameManager.Instance.questionsIncorrect +
                    "\nPrecisión: " + GameManager.Instance.GetAccuracy().ToString("0.0") + "%" +
                    "\nTiempo: " + GameManager.Instance.totalPlayTime.ToString("0.0") + "s" +
                    "\nPuntuación: " + GameManager.Instance.totalScore;
            }
        }
    }
}
