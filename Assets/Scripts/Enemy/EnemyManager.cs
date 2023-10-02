using UnityEngine;
using ToolBox.Pools;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;  

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 3);
    }

    void SpawnEnemy()
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.5f,1f), Random.value));
        var enemy = enemyPrefab.Reuse(transform);     // Pool get
        enemy.transform.position = randomPositionOnScreen;
    }
}
