using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float flipDuration = 0.2f;
    public LayerMask groundLayer; // Assign this in Inspector
    private bool isMoving = false;
    private bool is2DMode = false;

    void Update()
    {
        if (isMoving) return;

        // Toggle between 2D and 3D mode
        if (Input.GetKeyDown(KeyCode.Space))
            is2DMode = !is2DMode;

        // Read input
        Vector3 direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W)) direction = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector3.back;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector3.right;

        // Ignore Z movement in 2D mode
        if (is2DMode && (direction == Vector3.forward || direction == Vector3.back))
            return;

        if (direction != Vector3.zero)
        {
            Vector3 frontCheck = transform.position + direction;

            // Raycast to check if a cube exists in front of us
            if (Physics.Raycast(frontCheck + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 1f, groundLayer))
            {
                // Teleport on top of it
                transform.position = hit.collider.transform.position + Vector3.up;
                return; // stop here, don't flip
            }

            // Normal flip movement
            StartCoroutine(FlipMove(transform.position + direction));
        }
    }

    IEnumerator FlipMove(Vector3 targetPos)
    {
        isMoving = true;

        Vector3 direction = targetPos - transform.position;
        Vector3 anchor = transform.position + (Vector3.down + direction.normalized) * 0.5f;
        Vector3 axis = Vector3.Cross(Vector3.up, direction.normalized);

        float rotated = 0;
        while (rotated < 90)
        {
            float rotateStep = Mathf.Min(90 - rotated, Time.deltaTime * (90 / flipDuration));
            transform.RotateAround(anchor, axis, rotateStep);
            rotated += rotateStep;
            yield return null;
        }

        // Snap to clean grid
        transform.position = RoundVector(targetPos);
        transform.rotation = Quaternion.Euler(
            Mathf.Round(transform.eulerAngles.x / 90) * 90,
            Mathf.Round(transform.eulerAngles.y / 90) * 90,
            Mathf.Round(transform.eulerAngles.z / 90) * 90
        );

        isMoving = false;
    }

    Vector3 RoundVector(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }
}
