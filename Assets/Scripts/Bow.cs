using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Camera mainCamera;
    Vector2 bowPos;
    Vector2 bowDirection;

    [SerializeField] BowString bowString;

    [SerializeField] GameObject arrowPrefab;
    GameObject activeArrow = null;
    [SerializeField] Transform shotPoint;
    [SerializeField] float shootForce;
    [SerializeField] float shootForceMultiplier;

    [SerializeField] GameObject pointPrefab;
    private GameObject[] points;
    [SerializeField] int numberOfPoints;
    [SerializeField] float spaceBetweenPoints;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        bowPos = transform.position;

        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab,transform.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float mouseDistanceFromBow = Vector2.Distance(mousePos,bowPos);
        bowDirection = mousePos - bowPos;
        transform.right = bowDirection;

        if(Input.GetMouseButtonDown(0))
        {
            activeArrow = PrepareArrow();
        }

        if(Input.GetMouseButton(0))
        {
            shootForce = mouseDistanceFromBow * shootForceMultiplier;

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].SetActive(true);
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(activeArrow)
                Shoot();

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].SetActive(false);
            }
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

    Vector2 PointPosition(float t)
    {
        Vector2 pointPos = (Vector2)transform.position + 
                           (bowDirection.normalized * shootForce * t)
                            + 0.5f * Physics2D.gravity * (t * t); 
        return pointPos;
    }
}