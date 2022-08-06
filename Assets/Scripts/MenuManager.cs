using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _panelPause;
    [SerializeField] private GameObject _panelChoice;

    private void Awake()
    {
        GameManager.Instance.OnStateUpdate += HandleChangeState;
        
        _panelPause.SetActive(false);
        _panelChoice.SetActive(false);
    }
 
    private void Start()
    {
        _panelPause.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateUpdate -= HandleChangeState;
    }

    public void HandleChangeState(GameState state)
    {
        _panelPause.SetActive(state == GameState.Pause);
        _panelChoice.SetActive(state == GameState.ChooseAblility);

        Cursor.lockState = IsMenuShowing(state) ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = IsMenuShowing(state);// state == GameState.Pause;
        Time.timeScale = IsMenuShowing(state) ? 0 : 1;

        if (state == GameState.MainMenu)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnResume()
    {
        GameManager.Instance.UpdateGameState(GameState.Main);
    }

    public void OnButtonQuit()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
        //Application.Quit();
    }

    private void Update()
    {
        if (GameManager.Instance.State == GameState.Pause && Input.GetKey(KeyCode.Escape))
        {
            //GameManager.Instance.UpdateGameState(GameState.Main);
        }
    }

    private bool IsMenuShowing(GameState state)
    {
        return state == GameState.Pause || state == GameState.ChooseAblility;
    }
}
