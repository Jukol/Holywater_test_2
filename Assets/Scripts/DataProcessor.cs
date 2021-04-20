using System.Collections.Generic;
using UnityEngine;

public class DataProcessor : MonoBehaviour, IDataHandler
{
    public int QuestionsCount { get; private set; }
    
    public Answers answers;

    public Dictionary<int, Question> dataBase = new Dictionary<int, Question>();

    public bool fileEmpty;

    [SerializeField] private JsonReader _jsonReader;

    private Questions _questions;

    private Questions _emptyQuestions;

    private void Awake()
    {
        _emptyQuestions = new Questions
        {
            listOfQuestions = new Question[]
            {
                new Question
                {
                    id = 0,
                    question = "No question",
                    answers = new string[]
                    {
                        "No answer",
                    }
                }
            }
        };

        HandleInput();
        AnswerArrayInit();
    }

    public void HandleInput()
    {
        _questions = _jsonReader.ReadFromJson();
        
        if (_questions != null)
        {
            QuestionsCount = _questions.listOfQuestions.Length;

            for (int i = 0; i < QuestionsCount; i++)
            {
                dataBase.Add(_questions.listOfQuestions[i].id, _questions.listOfQuestions[i]);
            }
        }
        else
        {
            fileEmpty = true;
            
            QuestionsCount = 1;

            dataBase.Add(_emptyQuestions.listOfQuestions[0].id, _emptyQuestions.listOfQuestions[0]);
        }
    }

    public void HandleOutput(int id, string buttonText)
    {
        if (!fileEmpty)
        {
            answers.listOfAnswers[id].question = _questions.listOfQuestions[id].question;
            answers.listOfAnswers[id].answer = buttonText;
        }
        else
        {
            return;
        }
        
    }

    private void AnswerArrayInit()
    {
        answers = new Answers { };
        answers.listOfAnswers = new Answer[QuestionsCount];

        for (int i = 0; i < QuestionsCount; i++)
        {
            answers.listOfAnswers[i] = new Answer();
        }
    }

    public bool checkForEmptyAnswers()
    {
        foreach (var answer in answers.listOfAnswers)
        {
            if (answer.answer == null)
            {
                return true;
            }
        }

        return false;
    }
}