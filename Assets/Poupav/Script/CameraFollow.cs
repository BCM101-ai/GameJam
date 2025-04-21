using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset3D;
    public Vector3 offset2D;
    public float smoothSpeed = 5f;

    public bool is2D = false;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 offset = is2D ? offset2D : offset3D;
        Vector3 desired = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;
    }
}