using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeController : MonoBehaviour
{
    public float flipDuration = 0.2f;
    private bool isMoving = false;
    private bool is2DMode = false;

    void Update()
    {
        if (isMoving) return;

        // Toggle mode
        if (Input.GetKeyDown(KeyCode.Space))
            is2DMode = !is2DMode;

        // Read input
        Vector3 direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W)) direction = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector3.back;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector3.right;
        if (!is2DMode && Input.GetKeyDown(KeyCode.E)) direction = Vector3.up; // 3D only

        // Ignore Z movement in 2D mode
        if (is2DMode && (direction == Vector3.forward || direction == Vector3.back))
            return;

        if (direction != Vector3.zero)
            StartCoroutine(FlipMove(direction));
    }

    IEnumerator FlipMove(Vector3 dir)
    {
        isMoving = true;

        // Determine rotation anchor point
        Vector3 anchor = transform.position + (Vector3.down + dir) * 0.5f;
        Vector3 axis = Vector3.Cross(Vector3.up, dir);

        float rotated = 0;
        while (rotated < 90)
        {
            float rotateStep = Mathf.Min(90 - rotated, Time.deltaTime * (90 / flipDuration));
            transform.RotateAround(anchor, axis, rotateStep);
            rotated += rotateStep;
            yield return null;
        }

        // Snap position and rotation
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
