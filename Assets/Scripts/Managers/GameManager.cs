using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        State = GameState.Main;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        OnStateUpdate?.Invoke(State);
    }

}
