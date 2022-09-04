using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Main, 
    Pause,
    GameOver,
    ChooseAblility,
}

public class GameManager : MonoBehaviour
{
    public GameState State { get; private set; }
    public event Action<GameState> OnStateUpdate;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        State = GameState.MainMenu;
    }

    public void UpdateGameState(GameState newState)
    {
        if (State == GameState.MainMenu && newState == GameState.Main)
        {
            SceneManager.LoadScene(2);
        }

        State = newState;

        if (State == GameState.GameOver)
        {
            SceneManager.LoadScene(3);
        }

        OnStateUpdate?.Invoke(State);
    }

}
