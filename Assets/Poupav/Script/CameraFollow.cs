using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // Your player
    public Vector3 offset3D;         // Offset for 3D view
    public Vector3 offset2D;         // Offset for 2D view
    public float smoothSpeed = 5f;

    public bool is2D = false;        // Are we in 2D mode?

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetOffset = is2D ? offset2D : offset3D;
        Vector3 desiredPosition = target.position + targetOffset;

        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;
    }
}