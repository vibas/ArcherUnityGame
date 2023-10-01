using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject trail;
    Rigidbody2D rb2d;
    bool isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        trail.SetActive(false);
    }

    public void Move()
    {
        isMoving = true;
        trail.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);
        }
    }
}