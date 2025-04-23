using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3[] cameraOffsets3D;
    public Vector3[] cameraOffsets2D;
    public int currentViewIndex = 0;
    public float smoothSpeed = 5f;
    public int directionIndex = 0; // 0 = behind, 1 = left, 2 = front, 3 = right
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f; // adjust this in Inspector
    public bool is2D = false;
    public Vector3 offset2D;




    void LateUpdate()
    {
        if (target == null) return;

        Vector3 offset = is2D ? cameraOffsets2D[currentViewIndex] : cameraOffsets3D[currentViewIndex];
        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(target);
    }




    public void SetViewIndex(int index)
    {
        int max = is2D ? cameraOffsets2D.Length : cameraOffsets3D.Length;
        if (index >= 0 && index < max)
        {
            currentViewIndex = index;
        }
    }

}