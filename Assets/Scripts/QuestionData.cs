using System;

[Serializable]
public class QuestionEntry
{
    public string question;
    public string[] options;
    public int correctIndex;
    public string explanation;
    public string subject;
}

[Serializable]
public class QuestionSet
{
    public QuestionEntry[] questions;
}
