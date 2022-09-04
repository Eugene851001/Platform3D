using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Dialog : MonoBehaviour
{
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected TextMeshProUGUI _text;
    [SerializeField] protected Button _closeButton;

    protected void InitDialog()
    {
        _text.text = "Dialog";
        _closeButton.onClick.AddListener(HandleCloseButton);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(HandleCloseButton);
    }

    protected void HandleCloseButton()
    {
        GameManager.Instance.UpdateGameState(GameState.Main);
    }
}
