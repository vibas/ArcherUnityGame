using UnityEngine;
using ToolBox.Pools;

public class Monster : MonoBehaviour
{
    [SerializeField] GameObject deathParticlePrefab;
    [SerializeField] Vector2 speedRange;
    float _speed;   
    private Transform target; 

    void Start()
    {        
        _speed = Random.Range(speedRange.x,speedRange.y);
        target = FindObjectOfType<Player>().transform;        
    }

   void Update()
    {
        if(target!=null)
        {
            var step =  _speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step); 
        }               
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag(target.tag))
        {
            target.GetComponent<Player>().Damage();
            Die();
        }
    }

    public void Die()
    {
        deathParticlePrefab.Reuse(transform.position,Quaternion.identity);
        gameObject.Release();  
    }
}