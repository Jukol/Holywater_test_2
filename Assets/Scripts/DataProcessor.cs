using UnityEngine;

public class DataProcessor : MonoBehaviour, IDataHandler
{
    public int numberOfQuestions { get; private set; }
    public string[] inputQuestionTexts { get; private set; }
    public string[][] inputAnswerTexts { get; private set; }

    public Answers answers;

    [SerializeField] private JsonReader _jsonReader;

    private string _buttonText;
    private int _counter = 0;

    private void Awake()
    {
        HandleInput();
        AnswerArrayInit();
    }

    private void OnEnable()
    {
        AnswerButton.TextFromButton += GetAnswerFromButton;
    }

    private void OnDisable()
    {
        AnswerButton.TextFromButton -= GetAnswerFromButton;
    }

    public void HandleInput()
    {
        numberOfQuestions = _jsonReader.ReadFromJson().listOfQuestions.Length;

        inputQuestionTexts = new string[numberOfQuestions];
        inputAnswerTexts = new string[numberOfQuestions][];

        for (int i = 0; i < numberOfQuestions; i++)
        {
            inputQuestionTexts[i] = _jsonReader.ReadFromJson().listOfQuestions[i].question;
            inputAnswerTexts[i] = _jsonReader.ReadFromJson().listOfQuestions[i].answers;
        }
    }

    public void HandleOutput()
    {
        answers.listOfAnswers[_counter].question = inputQuestionTexts[_counter];
        answers.listOfAnswers[_counter].answer = _buttonText;
        _counter++;
    }

    private void AnswerArrayInit()
    {
        answers = new Answers { };
        answers.listOfAnswers = new Answer[numberOfQuestions];

        for (int i = 0; i < numberOfQuestions; i++)
        {
            answers.listOfAnswers[i] = new Answer();
        }
    }

    public void GetAnswerFromButton(string answer)
    {
        _buttonText = answer;
    }
}