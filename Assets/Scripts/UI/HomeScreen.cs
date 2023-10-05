using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] Button startButton;

    void OnEnable()
    {
        startButton.onClick.AddListener(()=>StartButtonAction());
    }

    void OnDisable()
    {
        startButton.onClick.RemoveAllListeners();
    }

    void StartButtonAction()
    {
        GameManager.Instance.OnGameStartEvent?.Invoke();
        gameObject.SetActive(false);
    }
}
