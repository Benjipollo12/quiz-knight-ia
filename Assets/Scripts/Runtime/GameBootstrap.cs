using UnityEngine;
using UnityEngine.UI;

namespace QuizKnightIA.Runtime
{
    public class GameBootstrap : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Bootstrap()
        {
            if (GameObject.Find("QuizKnightGameRoot") != null)
            {
                return;
            }

            GameObject root = new GameObject("QuizKnightGameRoot");
            DontDestroyOnLoad(root);

            GameManager gm = root.AddComponent<GameManager>();
            PlayerController player = root.AddComponent<PlayerController>();
            BossController boss = root.AddComponent<BossController>();
            QuizKnightIA.Gameplay.QuestionManager questionManager = root.AddComponent<QuizKnightIA.Gameplay.QuestionManager>();
            UIManager ui = root.AddComponent<UIManager>();

            GameObject canvasObject = new GameObject("Canvas");
            canvasObject.transform.SetParent(root.transform);
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();

            GameObject panelObject = new GameObject("GameOverPanel");
            panelObject.transform.SetParent(canvasObject.transform);
            Image panelImage = panelObject.AddComponent<Image>();
            panelImage.color = new Color(0f, 0f, 0f, 0.8f);
            RectTransform panelRect = panelObject.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.25f, 0.25f);
            panelRect.anchorMax = new Vector2(0.75f, 0.75f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;
            panelObject.SetActive(false);

            GameObject textObject = new GameObject("GameOverText");
            textObject.transform.SetParent(panelObject.transform);
            Text text = textObject.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 22;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.text = "GAME OVER";
            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            ui.gameOverPanel = panelObject;
            ui.gameOverText = text;

            gm.StartGame();
            Debug.Log("Quiz Knight IA: prototipo inicializado.");
        }
    }
}
