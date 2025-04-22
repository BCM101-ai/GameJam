using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform cameraTransform;
    public Camera mainCamera;

    // Both modes use orthographic projection
    public Quaternion rotation3D = Quaternion.Euler(30f, 45f, 0f);   // Isometric view
    public Quaternion rotation2D = Quaternion.Euler(90f, 0f, 0f);    // Side view
    public float rotationDuration = 1f;

    public bool is2D = false;
    public bool isRotating = false;

    void Start()
    {
        // Always orthographic
        if (mainCamera == null) mainCamera = Camera.main;
        mainCamera.orthographic = true;
        cameraTransform.rotation = rotation3D; // Start in 3D
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            is2D = !is2D;
            cameraFollow.is2D = is2D;

            StartCoroutine(SmoothRotate(is2D ? rotation2D : rotation3D));
            ToggleDimensionObjects(is2D);
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