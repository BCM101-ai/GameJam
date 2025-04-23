using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform cameraTransform;
    public Camera mainCamera;

    public Quaternion rotation3D = Quaternion.Euler(30f, 45f, 0f);
    public Quaternion rotation2D = Quaternion.Euler(90f, 0f, 0f);
    public float rotationDuration = 1f;


    public bool is2D = false;
    public Vector3 offset2D;
    public bool isRotating = false;


    private int cameraPOVIndex = 0;
    
    public static System.Action<bool> OnDimensionChange; // True = 2D, False = 3D
    public static bool Is2DModeStatic { get; private set; }



    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        mainCamera.orthographic = true;
        cameraTransform.rotation = rotation3D;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            is2D = !is2D;
            Is2DModeStatic = is2D;
            cameraFollow.is2D = is2D;
            Camera.main.orthographic = is2D;

            StartCoroutine(SmoothRotate(is2D ? rotation2D : rotation3D));
            ToggleDimensionObjects(is2D);

            OnDimensionChange?.Invoke(is2D); // 🔥 trigger event
        }


        // Change camera POV
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int max = cameraFollow.is2D ? cameraFollow.cameraOffsets2D.Length : cameraFollow.cameraOffsets3D.Length;
            cameraPOVIndex = (cameraPOVIndex + 1) % max;
            cameraFollow.SetViewIndex(cameraPOVIndex);
        }

    }

    System.Collections.IEnumerator SmoothRotate(Quaternion targetRotation)
    {
        isRotating = true;
        Quaternion startRotation = cameraTransform.rotation;
        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            cameraTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.rotation = targetRotation;
        isRotating = false;
    }

    void ToggleDimensionObjects(bool is2DNow)
    {
        DimensionObjectToggler[] allObjects = GameObject.FindObjectsOfType<DimensionObjectToggler>();



        foreach (var obj in allObjects)
        {
            obj.SetActiveForDimension(is2DNow);
        }
    }

}




