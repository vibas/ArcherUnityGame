using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Button quitButton;
    [SerializeField] Button restartButton;


    void OnEnable()
    {
        quitButton.onClick.AddListener(()=>QuitButtonAction());
        restartButton.onClick.AddListener(()=>RestartButtonAction());
    }

    void OnDisable()
    {
        quitButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }

    void QuitButtonAction()
    {
        Application.Quit();
    }

    void RestartButtonAction()
    {
        GameManager.Instance.ChangeGameState(GameState.GAMEPLAY);
    }
}