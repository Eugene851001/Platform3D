using Assets.Scripts;
using Assets.Scripts.Diaog;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesDialog : Dialog
{
    [SerializeField] GameObject _abilitiesManagerContainer;
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private GameObject _buttonsPanel;
    [SerializeField] private Button _buttonReturn;
    private AbilitiesManager _abilitiesManager;

    private AbilityNode _currentNode;
    private Dictionary<Button, AbilitiesButtonHandler> _buttonHandlers = 
        new Dictionary<Button, AbilitiesButtonHandler>();

    private void Start()
    {
        base.InitDialog();

        GameManager.Instance.OnStateUpdate += HandleChangeState;
        _text.text = "Please, choose ablity";

        _buttonReturn.onClick.AddListener(HandeButtonReturn);

        _abilitiesManager = _abilitiesManagerContainer.GetComponent<AbilitiesManager>();
        _currentNode = _abilitiesManager.AbilitiesTree;
        
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateUpdate -= HandleChangeState;  
    }

    private void HandleChangeState(GameState state)
    {
        if (state == GameState.ChooseAblility)
        {
            ResetDialog();
        }
    }

    private void ResetDialog()
    {
        _currentNode = _abilitiesManager.AbilitiesTree;
        ResetButtons();
    }

    private void RemoveButtons()
    {
        foreach (var button in _buttonHandlers.Keys)
        {
            button.onClick.RemoveAllListeners();
            Destroy(button.gameObject);
        }

        _buttonHandlers.Clear();
    }

    private void CreateButtons()
    {
        foreach (var node in _currentNode.Childs)
        {
            var button = Instantiate(_buttonPrefab);
            button.transform.SetParent(_buttonsPanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = node.Name;

            if (node.NodeType == NodeType.Ability && _abilitiesManager.Contains(node.Value))
            {
                if (node.Childs?.Length > 0)
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text += "+";
                }
                button.enabled = false;
                
            }

            var handler = new AbilitiesButtonHandler(node, HandleButton);
            _buttonHandlers.Add(button, handler);
            button.onClick.AddListener(handler.Handle);
        }

        _buttonReturn.gameObject.SetActive(_currentNode != _abilitiesManager.AbilitiesTree);
    }

    private void ResetButtons()
    {
        RemoveButtons();
        CreateButtons();
    }

    private void HandleButton(AbilityNode node)
    {
        if (node.NodeType == NodeType.Group && node.Childs.Length > 0)
        {
            _currentNode = node;
        }

        if (node.NodeType == NodeType.Ability)
        {
            if (_abilitiesManager.Contains(node.Value) && node.Childs.Length > 0)
            {
                _currentNode = node;
            }

            if (!_abilitiesManager.Contains(node.Value))
            {
                _abilitiesManager.Add(node.Value);
                GameManager.Instance.UpdateGameState(GameState.Main);
            }
        }

        ResetButtons();
    }

    private void HandeButtonReturn()
    {
        if (_currentNode.Parent != null)
        {
            _currentNode = _currentNode.Parent;
            ResetButtons();
        }
    }

}
