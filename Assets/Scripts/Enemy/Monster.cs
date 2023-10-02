using UnityEngine;
using ToolBox.Pools;

public class Monster : MonoBehaviour, IPoolable
{
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

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 8f)
        {
            gameObject.Release();
        }
    }

    public void OnReuse()
    {
    }

    public void OnRelease()
    {
    }
}
