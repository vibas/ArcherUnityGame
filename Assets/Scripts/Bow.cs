using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Camera mainCamera;
    Vector2 bowPos;

    [SerializeField] BowString bowString;

    [SerializeField] GameObject arrowPrefab;
    GameObject activeArrow = null;
    [SerializeField] Transform shotPoint;
    private float shootForce = 20;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        bowPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - bowPos;
        transform.right = direction;

        if(Input.GetMouseButtonDown(0))
        {
            activeArrow = PrepareArrow();
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(activeArrow)
                Shoot();
        }

    }

    GameObject PrepareArrow()
    {
        bowString.PullString();
        GameObject newArrow = Instantiate(arrowPrefab, transform);
        newArrow.transform.Rotate(Vector3.forward,-90); 
        return newArrow;
    }

    void Shoot()
    {
        activeArrow.transform.SetParent(null);
        activeArrow.GetComponent<Arrow>().Move();
        Rigidbody2D rb = activeArrow.GetComponent<Rigidbody2D>();

        rb.simulated = true;
        rb.velocity = transform.right * shootForce;
        bowString.Release();
    }
}