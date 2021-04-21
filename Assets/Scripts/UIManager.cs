using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator _bunnyAnimator, _questionBubbleAnimator;

    [SerializeField] private GameObject _questionBubble, _answersParent;
    [SerializeField] private GameObject _startPanel, _mainPanel, _finalPanel, _thankYouPanel;

    [SerializeField] private ButtonPool _buttonPool;
    
    [SerializeField] private DataProcessor _dataProcessor;

    [SerializeField] private TextMeshProUGUI _finalText;

    private WaitForSeconds _waitBetweenAnimations;
    private float _secondsBetweenAnimations = 0.3f;

    private int _counter = 0;

    private TextMeshProUGUI _questionBubbleText;

    private AnswerButton _answerButton;

    private List<AnswerButton> _answerButtonList = new List<AnswerButton>();

    private void Awake()
    {
        _questionBubble.gameObject.SetActive(false);
        _waitBetweenAnimations = new WaitForSeconds(_secondsBetweenAnimations);
    }

    private void Start()
    {
        _startPanel.SetActive(true);
        _mainPanel.SetActive(false);
        _finalPanel.SetActive(false);
        _thankYouPanel.SetActive(false);

        _questionBubbleText = _questionBubble.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StartApp()
    {
        StartCoroutine(AnimationSequence());
    }

    private IEnumerator AnimationSequence()
    {
        _bunnyAnimator.SetTrigger("StartPlaying");

        yield return _waitBetweenAnimations;

        _questionBubble.gameObject.SetActive(true);

        QuestionButtonInit(_counter);

        yield return _waitBetweenAnimations;

        AnswerSelectionInit(_counter);
    }

    private void QuestionButtonInit(int id)
    {
        _questionBubbleText.text = _dataProcessor.dataBase[id].question;
        _questionBubbleAnimator.SetTrigger("StartPlaying");
    }

    private void AnswerSelectionInit(int id)
    {
        for (int i = 0; i < _dataProcessor.dataBase[id].answers.Length; i++)
        {
            _answerButton = _buttonPool.ButtonRequest();
            _answerButton.transform.SetParent(_answersParent.transform, false);
            _answerButton.Id = id;
            
            _answerButton.TMProText.text = _dataProcessor.dataBase[id].answers[i];

            _answerButtonList.Add(_answerButton);
            
            AnimationPlayer(_answerButton.gameObject);
        }
    }

    private void AnimationPlayer(GameObject gameObject)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Animator>().SetTrigger("StartPlaying");
    }

    public void DataSwitcher()
    {
        if(_counter < _dataProcessor.QuestionsCount - 1 && !_dataProcessor.fileEmpty)
        {
            _counter++;

            DeactivateAnswerButtons();

            QuestionButtonInit(_counter);
            AnswerSelectionInit(_counter);
        }

        else if (_counter >= _dataProcessor.QuestionsCount - 1 && !_dataProcessor.fileEmpty)
        {
            if (!_dataProcessor.СheckForEmptyAnswers())
            {
                LoadFinalPanel("Ready to upload your answers?");
            }
            else
            {
                LoadFinalPanel("One or more questions left unanswered. \nStill want to upload?");
            }
        }
    }

    private void LoadFinalPanel(string finalText)
    {
        _mainPanel.SetActive(false);
        _finalPanel.SetActive(true);
        _finalText.text = finalText;
    }

    private void DeactivateAnswerButtons()
    {
        foreach (AnswerButton button in _answerButtonList)
        {
            button.gameObject.SetActive(false);
            button.gameObject.transform.SetParent(_buttonPool.transform, false);
        }
        _answerButtonList.Clear();
    }

    public void ReloadApp()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
