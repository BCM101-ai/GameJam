using UnityEngine;

public class PerspectiveGroundDetector : MonoBehaviour
{
    public Camera cam;                     // Reference to main camera
    public float rayLength = 100f;         // Max ray distance
    public LayerMask groundLayer;          // Layer to detect platforms

    public bool isOnVisualGround = false;
    public Vector3 hitPoint;

    void Update()
    {
        // Get screen position of player (center point)
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);

        // Cast ray from camera through that point
        Ray ray = cam.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, groundLayer))
        {
            isOnVisualGround = true;
            hitPoint = hit.point;
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.green);
        }
        else
        {
            isOnVisualGround = false;
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
        }
    }
}