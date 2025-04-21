using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform cameraTransform;

    public Quaternion rotation3D = Quaternion.Euler(30f, 45f, 0f);
    public Quaternion rotation2D = Quaternion.Euler(90f, 0f, 0f);

    public float rotationDuration = 1f;

    private bool is2D = false;
    private bool isRotating = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            is2D = !is2D;

            // Tell camera follow script to switch offset
            cameraFollow.is2D = is2D;

            // Start smooth rotation
            StartCoroutine(SmoothRotate(is2D ? rotation2D : rotation3D));
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
}
