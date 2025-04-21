using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // The player
    public Vector3 offset;         // Offset from the player
    public float smoothSpeed = 5f; // Higher = faster camera snap

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}

