using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int _health;
    [SerializeField] SpriteRenderer shieldSprite;
    [SerializeField] Bow bow;

    void OnEnable()
    {
        GameManager.Instance.OnGameStateChangeEvent+= HandleGameStateChangedEvent;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStateChangeEvent+= HandleGameStateChangedEvent;
    }  
    
    void Start()
    {
        ResetHealth();
    }

    void ResetHealth()
    {
        _health = maxHealth;
        UpdateShieldHealth();
    }

    void UpdateShieldHealth()
    {
        var alpha = (float) _health/ (float) maxHealth;
        shieldSprite.color = new Color(1,1,1,alpha);
    }

    public void Damage()
    {
        if(_health>0)
        {
            _health--;
            UpdateShieldHealth();
        }
        if(_health == 0)
        {
            Die();
        } 
    }

    public void Die()
    {
        GameManager.Instance.OnPlayerDeathEvent?.Invoke();
    }

    void HandleGameStateChangedEvent(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.HOME:
            bow.ShouldUseBow = false;
            break;

            case GameState.GAMEPLAY:
            StartCoroutine(StartUsingBow());
            ResetHealth();
            break;

            case GameState.GAMEOVER:
            bow.ShouldUseBow = false;
            break;            
        }
    }

    IEnumerator StartUsingBow()
    {
        yield return new WaitForSeconds(1);
        bow.ShouldUseBow = true;
    }
}