using UnityEngine;
using ToolBox.Pools;

public class Monster : MonoBehaviour
{
    [SerializeField] GameObject deathParticlePrefab;
    public float speed = 1.0f;
   
    private Transform target; 

    void Start()
    {        
        target = GameObject.FindWithTag("Player").transform;        
    }

   void Update()
    {
        var step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);        
    }

    public void Die()
    {
        deathParticlePrefab.Reuse(transform.position,Quaternion.identity);
        gameObject.Release();  
    }
}