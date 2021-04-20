using System.Collections.Generic;
using UnityEngine;

public class ButtonPool : MonoBehaviour
{
    //private static ButtonPool _instance;
    //public static ButtonPool Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = new ButtonPool();
    //        }

    //        return _instance;
    //    }
    //}

    //private void Awake()
    //{
    //    _instance = this;
    //}

    [SerializeField] private AnswerButton _buttonPrefab;
    [SerializeField] private int _buttonsInPool;
    [SerializeField] private GameObject _buttonsContainer;

    private List<AnswerButton> _buttons = new List<AnswerButton>();

    private void Start()
    {
        GeneratePool();
    }

    private void GeneratePool()
    {
        for (int i = 0; i < _buttonsInPool; i++)
        {
            AddButtonToPool();
        }
    }

    public AnswerButton ButtonRequest()
    {
        foreach (var button in _buttons)
        {
            if (button != null)
            {
                if (button.gameObject.activeInHierarchy == false)
                {
                    button.gameObject.SetActive(true);
                    return button;
                }
            }

        }
        return AddButtonToPool();
    }
    private AnswerButton AddButtonToPool()
    {
        AnswerButton button = Instantiate(_buttonPrefab);
        button.transform.SetParent(_buttonsContainer.transform);
        button.gameObject.SetActive(false);
        _buttons.Add(button);
        return button;
    }
}
