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

        activeArrow = PrepareArrow();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float mouseDistanceFromBow = Vector2.Distance(mousePos,bowPos);
        bowDirection = mousePos - bowPos;
        transform.right = bowDirection;

        if(Input.GetMouseButton(0))
        {
            shootForce = mouseDistanceFromBow * shootForceMultiplier;
            shootForce = Mathf.Clamp(shootForce,shootForce,21);
            bowString.PullString(shootForce);
            activeArrow.transform.localPosition = new Vector3(-shootForce/15f + 0.9f,0,0);
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,(1f-i/(float)numberOfPoints));
                points[i].SetActive(true);
                points[i].transform.position = TrajectoryPointPosition(i * spaceBetweenPoints);
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
        GameObject newArrow = Instantiate(arrowPrefab, transform);
        newArrow.transform.localPosition = new Vector3(0.8f,0,0);
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

        activeArrow = PrepareArrow();
    }

    Vector2 TrajectoryPointPosition(float t)
    {
        Vector2 pointPos = (Vector2)transform.position + 
                           (bowDirection.normalized * shootForce * t)
                            + 0.5f * Physics2D.gravity * (t * t); 
        return pointPos;
    }
}