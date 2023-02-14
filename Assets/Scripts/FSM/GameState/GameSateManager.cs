using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSateManager : MonoBehaviour
{
    #region Singleton
    public static GameSateManager Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion


    GameBaseState currentGameState;

    LobbyState lobbyState = new LobbyState();
    PauseState pauseState = new PauseState();
    MainGameState mainGameState = new MainGameState();

    private void Start()
    {
        ChangeGameState(GameState.lobby);
    }

    private void Update()
    {
        currentGameState.UpdateState(this);
    }

    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.lobby:
                currentGameState = lobbyState;
                break;
            case GameState.mainGame:
                currentGameState = mainGameState;
                break;
            case GameState.pause:
                currentGameState = pauseState;
                break;
        }
        currentGameState.EnterState(this);
    }
}

public enum GameState
{
    lobby,
    pause,
    mainGame
}
