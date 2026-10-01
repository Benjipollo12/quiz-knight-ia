using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player;
    public BossController boss;
    public QuestionManager questionManager;

    public int bossesDefeated;
    public int correctAnswers;
    public int wrongAnswers;
    public float elapsedTime;
    public bool isGameOver;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!isGameOver)
            elapsedTime += Time.deltaTime;
    }

    public void RegisterAnswer(bool correct)
    {
        if (correct) correctAnswers++;
        else wrongAnswers++;

        if (player != null)
            player.ApplyQuestionFeedback(correct);
    }

    public void RegisterBossDefeat()
    {
        bossesDefeated++;
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("GAME OVER");
        Debug.Log("Jefes derrotados: " + bossesDefeated);
        Debug.Log("Correctas: " + correctAnswers);
        Debug.Log("Incorrectas: " + wrongAnswers);
        Debug.Log("Tiempo: " + elapsedTime);
    }
}
