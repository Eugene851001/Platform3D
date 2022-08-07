using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Run()
    {
        Debug.Log("Starting game...");
        GameManager.Instance.UpdateGameState(GameState.Main);
    }
}
