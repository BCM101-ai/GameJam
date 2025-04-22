using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform cameraTransform;
    public Camera mainCamera;
    public Transform playerTransform;
    public float raycastDistance = 100f;
    public LayerMask twoDPlatformLayer;
    public Vector3 rayOriginOffset = new Vector3(0f, -0.1f, 0f);
    public Transform last2DPlatform; // Already exists, just make sure it's public

    


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
            mainCamera.orthographic = is2D;
            cameraFollow.is2D = is2D;

            if (!is2D && last2DPlatform != null)
            {
                playerTransform.position = new Vector3(
                    last2DPlatform.position.x,
                    last2DPlatform.position.y,
                    playerTransform.position.z
                );
            }

<<<<<<< Updated upstream
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
=======
            //StartCoroutine(SmoothRotate(is2D ? rotation2D : rotation3D));
>>>>>>> Stashed changes
        }

        // Always raycast while in 2D mode
        
    }
<<<<<<< Updated upstream
}
=======

    

}
>>>>>>> Stashed changes
