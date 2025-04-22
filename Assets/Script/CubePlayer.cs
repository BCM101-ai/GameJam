using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlayer : MonoBehaviour
{
    public float flipDuration = 0.2f;
    public LayerMask obstacleMask;
    public float climbSearchRadius = 1.5f;  // how far we search around direction
    public float maxClimbHeight = 1.5f;     // how high we can climb

    private bool isMoving = false;
    private bool is2DMode = false;

    void Update()
    {
        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.Space))
            is2DMode = !is2DMode;

        Vector3 direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W)) direction = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector3.back;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector3.right;
        if (!is2DMode && Input.GetKeyDown(KeyCode.E)) direction = Vector3.up;

        if (is2DMode && (direction == Vector3.forward || direction == Vector3.back))
            return;

        if (direction != Vector3.zero)
        {
            TryMove(direction);
        }
    }

    void TryMove(Vector3 dir)
    {
        Vector3 origin = transform.position;

        // Look for any climbable block around the intended direction
        Collider[] hits = Physics.OverlapBox(origin + dir, Vector3.one * climbSearchRadius, Quaternion.identity, obstacleMask);

        Collider nearest = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            Vector3 hitPos = hit.transform.position;
            float heightDiff = hitPos.y - origin.y;

            if (heightDiff >= -0.1f && heightDiff <= maxClimbHeight)
            {
                float dist = Vector3.Distance(origin + dir, hitPos);
                if (dist < nearestDistance)
                {
                    nearestDistance = dist;
                    nearest = hit;
                }
            }
        }

        if (nearest != null)
        {
            Vector3 moveDir = nearest.transform.position - origin;
            StartCoroutine(FlipMove(moveDir));
        }
        else
        {
            // Just move normally if no block found
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
        return new Vector3(Mathf.Round(v.x * 10) / 10f, Mathf.Round(v.y * 10) / 10f, Mathf.Round(v.z * 10) / 10f);
    }
}