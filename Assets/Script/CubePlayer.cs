using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlayer : MonoBehaviour
{
    public float flipDuration = 0.2f;
    public LayerMask obstacleMask;

    private bool isMoving = false;
    private bool is2DMode = false;

    void Update()
    {
        if (isMoving) return;

        // Toggle mode
        if (Input.GetKeyDown(KeyCode.Space))
            is2DMode = !is2DMode;

        // Input handling
        Vector3 direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W)) direction = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector3.back;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector3.right;
        if (!is2DMode && Input.GetKeyDown(KeyCode.E)) direction = Vector3.up; // optional

        if (is2DMode && (direction == Vector3.forward || direction == Vector3.back))
            return; // No depth movement in 2D

        if (direction != Vector3.zero)
        {
            TryMove(direction);
        }
    }

    void TryMove(Vector3 dir)
    {
        Vector3 origin = transform.position;
        Vector3 checkPos = origin + dir;

        // Check if there's a cube in the direction (1 unit high)
        if (Physics.Raycast(checkPos + Vector3.up * 2, Vector3.down, out RaycastHit hit, 2f, obstacleMask))
        {
            Vector3 targetPos = hit.collider.transform.position;

            // If the cube is 1 unit high (same level), flip onto it
            if (Mathf.Abs(targetPos.y - origin.y) <= 1.1f)
            {
                StartCoroutine(FlipMove(targetPos - origin));
            }
        }
        else
        {
            // No block — just move normally
            StartCoroutine(FlipMove(dir));
        }
    }

    IEnumerator FlipMove(Vector3 dir)
    {
        isMoving = true;

        Vector3 anchor = transform.position + (Vector3.down + dir.normalized) * 0.5f;
        Vector3 axis = Vector3.Cross(Vector3.up, dir.normalized);

        float rotated = 0;
        while (rotated < 90)
        {
            float rotateStep = Mathf.Min(90 - rotated, Time.deltaTime * (90 / flipDuration));
            transform.RotateAround(anchor, axis, rotateStep);
            rotated += rotateStep;
            yield return null;
        }

        // Snap
        transform.position = RoundVector(transform.position);
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
