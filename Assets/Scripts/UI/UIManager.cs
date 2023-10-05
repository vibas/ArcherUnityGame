using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] HomeScreen homeScreen;
    [SerializeField] GameOverScreen gameOverScreen;

    void OnEnable()
    {
        GameManager.Instance.OnGameStateChangeEvent+= HandleGameStateChangedEvent;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStateChangeEvent+= HandleGameStateChangedEvent;
    }

    void HandleGameStateChangedEvent(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.HOME: 
            homeScreen.gameObject.SetActive(true);               
            break;

            case GameState.GAMEPLAY:
            homeScreen.gameObject.SetActive(false);
            gameOverScreen.gameObject.SetActive(false);
            break;

            case GameState.GAMEOVER:
            gameOverScreen.gameObject.SetActive(true);
            break;
            
        }
    }
}
