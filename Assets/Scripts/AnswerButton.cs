﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AnswerButton : MonoBehaviour
{
    public string buttonText;

    private Button _thisButton;

    public static Action<string> TextFromButton;

    private void OnEnable()
    {
        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(GetText);
    }

    private void GetText()
    {
        buttonText = _thisButton.GetComponentInChildren<TextMeshProUGUI>().text;
        TextFromButton?.Invoke(buttonText);
    }
}
