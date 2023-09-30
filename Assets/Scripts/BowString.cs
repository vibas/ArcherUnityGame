using UnityEngine;

public class BowString : MonoBehaviour
{
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void PullString()
    {
        lineRenderer.SetPosition(1, new Vector3(-0.78f,0,0));
    }

    public void Release()
    {
        lineRenderer.SetPosition(1,Vector3.zero);
    }
}
