using System;
using UnityEngine;

public enum GameState
{
    HOME,
    GAMEPLAY,
    GAMEOVER
}

public class GameManager : SingletonBehaviour<GameManager>
{    
    public Action OnGameStartEvent;
    public Action OnPlayerDeathEvent;
    public Action<GameState> OnGameStateChangeEvent;

    [SerializeField] GameState GameState;

    void OnEnable()
    {
        OnGameStartEvent += GameStart;
        OnPlayerDeathEvent += GameOver;
    }

    void OnDisable()
    {
        OnGameStartEvent -= GameStart;
        OnPlayerDeathEvent -= GameOver;
    }

    void Start()
    {
        ChangeGameState(GameState.HOME);
    }

    public void ChangeGameState(GameState gameState)
    {
        GameState = gameState;
        OnGameStateChangeEvent?.Invoke(GameState);
    }

    public void GameStart()
    {
        ChangeGameState(GameState.GAMEPLAY);
    }

    void GameOver()
    {
        ChangeGameState(GameState.GAMEOVER);
    }
}