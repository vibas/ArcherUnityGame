using UnityEngine;
using ToolBox.Pools;

public class Bow : MonoBehaviour
{
    // ========= CAMERA USED FOR MOUSE POSITION =========
    Camera _mainCamera;

    // ========== BOW =============
    [SerializeField] BowString bowString;
    Vector2 _bowPos;
    Vector2 _bowDirection; 
    
    // =========== ARROW ============
    [SerializeField] GameObject arrowPrefab;    
    [SerializeField] float maxShootForce;    
    [SerializeField] float shootForceMultiplier;
    [SerializeField] float shootForceDivider;
    [SerializeField] float arrowIdlePosOffset;
    [SerializeField] float arrowAngleOffset;
    float shootForce;
    GameObject activeArrow = null;

    // ============ TRAJECTORY PATH ============
    [SerializeField] TrajectoryPath trajectoryPath;


    void Start()
    {
        _mainCamera = Camera.main;
        _bowPos = transform.position;
        trajectoryPath.SetupPath();

        SpawnArrow();
    }

    void Update()
    {
        SetBowDirectionBasedOnMousePosition();
        if(Input.GetMouseButton(0))
        {
            SetForceBasedOnMouseDistance();
            PullArrow();
            trajectoryPath.UpdatePath(_bowDirection,shootForce);
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(activeArrow)
            {
                Shoot();
                trajectoryPath.Hide();
                SpawnArrow();                
            }            
        }
    }

    /// <summary>
    /// Spawn Arrow at Bow and set its initial position and rotation
    /// </summary>
    void SpawnArrow()
    {
        activeArrow = arrowPrefab.Reuse(transform);     // Pool get
        activeArrow.transform.localPosition = new Vector3(arrowIdlePosOffset,0,0);
        activeArrow.transform.localRotation = Quaternion.AngleAxis(arrowAngleOffset,Vector3.forward);
    }

    /// <summary>
    /// Set Bow direction towards the mouse position
    /// </summary>
    void SetBowDirectionBasedOnMousePosition()
    {
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _bowDirection = mousePos - _bowPos;
        transform.right = _bowDirection;
    }

    /// <summary>
    /// Set arrow shot force based on the mouse distance. More distance = more power
    /// Should not cross max power
    /// </summary>
    void SetForceBasedOnMouseDistance()
    {
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float mouseDistanceFromBow = Vector2.Distance(mousePos,_bowPos);
        shootForce = mouseDistanceFromBow * shootForceMultiplier;
        shootForce = Mathf.Clamp(shootForce,shootForce,maxShootForce);
    }

    /// <summary>
    /// Pull Arrow. Update Arrow position as per pull power
    /// </summary>
    void PullArrow()
    {
        bowString.Pull(shootForce/shootForceDivider);
        activeArrow.transform.localPosition = new Vector3(-shootForce/shootForceDivider + arrowIdlePosOffset,0,0);
    }

    /// <summary>
    /// Shoot Arrow event
    /// Arrow is moved with direction and power set by user input
    /// </summary>
    void Shoot()
    {
        activeArrow.GetComponent<Arrow>().Move(transform.right, shootForce);
        bowString.Release();    // Pool release        
    }
}