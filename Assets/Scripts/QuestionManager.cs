using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text questionText;
    public TMP_Text[] optionTexts;
    public Button[] optionButtons;

    private QuestionEntry activeQuestion;
    private readonly List<string> askedQuestions = new List<string>();

    public bool IsOpen()
    {
        return panel.activeSelf;
    }

    public void ShowRandomQuestion()
    {
        TextAsset[] files = Resources.LoadAll<TextAsset>("Questions");
        if (files.Length == 0)
        {
            Debug.LogWarning("No hay archivos JSON en Resources/Questions");
            return;
        }

        TextAsset chosenFile = files[Random.Range(0, files.Length)];
        QuestionSet set = JsonUtility.FromJson<QuestionSet>(chosenFile.text);

        if (set == null || set.questions == null || set.questions.Length == 0)
            return;

        List<QuestionEntry> valid = new List<QuestionEntry>();
        foreach (var q in set.questions)
        {
            if (!askedQuestions.Contains(q.question))
                valid.Add(q);
        }

        if (valid.Count == 0)
            valid = new List<QuestionEntry>(set.questions);

        activeQuestion = valid[Random.Range(0, valid.Count)];
        askedQuestions.Add(activeQuestion.question);

        questionText.text = activeQuestion.question;

        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < activeQuestion.options.Length)
            {
                optionTexts[i].text = activeQuestion.options[i];
                optionButtons[i].gameObject.SetActive(true);
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }

        panel.SetActive(true);
    }

    public void Answer(int index)
    {
        if (activeQuestion == null)
            return;

        bool correct = index == activeQuestion.correctIndex;
        GameManager.instance.RegisterAnswer(correct);
        panel.SetActive(false);
        activeQuestion = null;
    }
}
