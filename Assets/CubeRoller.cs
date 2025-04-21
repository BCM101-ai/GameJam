using UnityEngine;
using System.Collections;


public class CubeRoller : MonoBehaviour
{
    public float flipDuration = 0.2f; // Short animation
    private bool isFlipping = false;

    void Update()
    {
        if (isFlipping) return;

        if (Input.GetKeyDown(KeyCode.W)) StartCoroutine(Flip(Vector3.forward));
        if (Input.GetKeyDown(KeyCode.S)) StartCoroutine(Flip(Vector3.back));
        if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Flip(Vector3.left));
        if (Input.GetKeyDown(KeyCode.D)) StartCoroutine(Flip(Vector3.right));
    }

    IEnumerator Flip(Vector3 direction)
    {
        isFlipping = true;

        Vector3 pivot = transform.position + (Vector3.down + direction) * 0.5f;
        Vector3 axis = Vector3.Cross(Vector3.up, direction);

        float rotated = 0f;
        float totalAngle = 90f;

        while (rotated < totalAngle)
        {
            float step = (Time.deltaTime / flipDuration) * totalAngle;
            transform.RotateAround(pivot, axis, step);
            rotated += step;
            yield return null;
        }

        // Snap position and rotation
        transform.position = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            Mathf.Round(transform.position.z)
        );

        transform.rotation = Quaternion.Euler(
            Mathf.Round(transform.eulerAngles.x / 90f) * 90f,
            Mathf.Round(transform.eulerAngles.y / 90f) * 90f,
            Mathf.Round(transform.eulerAngles.z / 90f) * 90f
        );

        isFlipping = false;
    }
}
