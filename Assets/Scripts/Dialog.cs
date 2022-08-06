using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Dialog : MonoBehaviour
{
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected TextMeshProUGUI _text;
    [SerializeField] protected Button _firstButton;
    [SerializeField] protected Button _secondButton;

    protected void InitDialog()
    {
        _text.text = "Dialog";
        _firstButton.onClick.AddListener(OnFirstButton);
        _secondButton.onClick.AddListener(OnSecondButton);
    }

    protected abstract void OnFirstButton();

    protected abstract void OnSecondButton();
}
