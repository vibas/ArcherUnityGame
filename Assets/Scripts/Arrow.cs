using UnityEngine;
using ToolBox.Pools;
using System.Threading.Tasks;

public class Arrow : MonoBehaviour, IPoolable
{
    [SerializeField] GameObject trail;
    [SerializeField] int waitTimeInMS;
    Rigidbody2D rb;
    bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trail.SetActive(false);
    }

    public async void Move(Vector3 direction, float shootForce)
    {
        isMoving = true;
        trail.SetActive(true);
        transform.SetParent(null);

        rb.simulated = true;
        rb.velocity = direction * shootForce;

        await ResetArrow();
    }
    
    void Update()
    {
        if(isMoving)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);
        } 
    }

    async Task ResetArrow()
    {
        await Task.Delay(waitTimeInMS);
       
        if(this!=null)
            gameObject.Release();
    }

    public void OnReuse()
    {
    }

    public void OnRelease()
    {
        rb.simulated = false;
        isMoving = false;  
        transform.SetParent(null);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        trail.SetActive(false);
        trail.GetComponent<TrailRenderer>().Clear();        
    }
}