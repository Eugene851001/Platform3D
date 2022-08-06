using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesDialog : Dialog
{
    [SerializeField] GameObject _abilitiesManagerContainer;
    private AbilitiesManager _abilitiesManager;

    private void Start()
    {
        base.InitDialog();
        _text.text = "Please, choose ablity";

        _firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "Double jump";
        _secondButton.GetComponentInChildren<TextMeshProUGUI>().text = "Move object";

        _abilitiesManager = _abilitiesManagerContainer.GetComponent<AbilitiesManager>();
    }

    protected override void OnFirstButton()
    {
        _abilitiesManager.Add(Ablilities.DoubleJump);
        GameManager.Instance.UpdateGameState(GameState.Main);
    }

    protected override void OnSecondButton()
    {
        _abilitiesManager.Add(Ablilities.MoveObjects);
        GameManager.Instance.UpdateGameState(GameState.Main);
    }
}
