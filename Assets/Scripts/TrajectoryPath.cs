using UnityEngine;

public class TrajectoryPath : MonoBehaviour
{
    [SerializeField] GameObject pointPrefab;
    private GameObject[] points;
    [SerializeField] int numberOfPoints;
    [SerializeField] float spaceBetweenPoints;


    public void SetupPath()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab,transform.position, Quaternion.identity);
            points[i].transform.SetParent(transform);
            points[i].SetActive(false);
        }
    }

    public void UpdatePath(Vector2 bowDirection, float shootForce)
    {
        for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,(1f-i/(float)numberOfPoints));
                points[i].SetActive(true);
                points[i].transform.position = TrajectoryPointPosition(i * spaceBetweenPoints, bowDirection, shootForce);
            }
    }

    Vector2 TrajectoryPointPosition(float t, Vector2 bowDirection, float shootForce)
    {
        Vector2 pointPos = (Vector2)transform.position + 
                           (bowDirection.normalized * shootForce * t)
                            + 0.5f * Physics2D.gravity * (t * t); 
        return pointPos;
    }

    public void Hide()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(false);
        }
    }
}