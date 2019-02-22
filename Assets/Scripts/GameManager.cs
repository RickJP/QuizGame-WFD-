using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Question[] _questions = null;
    public Question[] Questions { get { return _questions; } }

    [SerializeField] GameEvents events = null;

    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();
    private int currentQuestion = 0;

    private void Start()
    {
        LoadQuestions();
        foreach (var question in Questions)
        {
            Debug.Log(question.Info);
        }
        //Display();  
    }


    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerData>();
    }

    void Display()
    {
        EraseAnswers();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        }
        else
        {
            Debug.LogWarning("Error when trying to display a new question UI data");
        }
    }

    Question GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;

        return Questions[currentQuestion];
    }

    int GetRandomQuestionIndex() 
    {
        var random = 0;
        if (FinishedQuestions.Count < Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestion);
        }
        return random;
    }


    void LoadQuestions()
    {
        Object[] objects = Resources.LoadAll("Questions", typeof(Question));
        _questions = new Question[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            _questions[i] = (Question)objects[i];
        }
    }
}
