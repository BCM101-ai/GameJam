using UnityEngine;

public class DimensionSwapCamera : MonoBehaviour
{
    public Camera camera2D;
    public Camera camera3D;

    private bool isIn2D = true;

    void Start()
    {
        SetActiveCamera();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isIn2D = !isIn2D;
            SetActiveCamera();
        }
    }

    void SetActiveCamera()
    {
        camera2D.enabled = isIn2D;
        camera3D.enabled = !isIn2D;
    }
}

