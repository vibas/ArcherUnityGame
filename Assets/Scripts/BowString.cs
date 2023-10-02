using UnityEngine;
public class BowString : MonoBehaviour
{
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Pull(float shootForce)
    {
        lineRenderer.SetPosition(1, new Vector3(-shootForce,0,0));
    }

    public void Release()
    {
        lineRenderer.SetPosition(1,Vector3.zero);
    }
}