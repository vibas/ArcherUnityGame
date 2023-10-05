using UnityEngine;
using ToolBox.Pools;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;  

    string _spawnEnemyMethod = "SpawnEnemy";

    void OnEnable()
    {
        GameManager.Instance.OnGameStateChangeEvent+= HandleGameStateChangedEvent;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStateChangeEvent+= HandleGameStateChangedEvent;
    }  

    void SpawnEnemy()
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.5f,1f), Random.value));
        var enemy = enemyPrefab.Reuse(transform);     // Pool get
        enemy.transform.position = randomPositionOnScreen;
    }

    void HandleGameStateChangedEvent(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAMEPLAY:
            InvokeRepeating(_spawnEnemyMethod, 5, Random.Range(3,5));
            break;

            case GameState.GAMEOVER:
            CancelInvoke(_spawnEnemyMethod);
            break;            
        }
    }
}