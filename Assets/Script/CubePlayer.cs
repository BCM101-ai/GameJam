using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlayer : MonoBehaviour
{
    public float flipDuration = 0.2f;
    public LayerMask obstacleMask;
<<<<<<< HEAD
    public float climbSearchRadius = 1.5f;
    public float maxClimbHeight = 1.5f;

    public AudioSource walkAudio;
    public AudioSource fallAudio;
    public float groundCheckDistance = 0.6f;
=======
    public float climbSearchRadius = 1.5f;  // how far we search around direction
    public float maxClimbHeight = 1.5f;     // how high we can climb
    public Transform cameraTransform;  // Drag the camera here in the inspector
    public CameraFollow cameraFollow;

>>>>>>> 7579dcd272abe0a5e8b0dba18e1cdd793e85acbc

    private bool isMoving = false;
    private bool is2DMode = false;
    private bool wasGrounded = true;
    private bool isFalling = false;

    void Update()
    {
        bool isGrounded = IsGrounded();

        if (!isMoving)
        {
            if (!isGrounded && !isFalling)
            {
                // Started falling
                isFalling = true;
                if (fallAudio != null && !fallAudio.isPlaying)
                    fallAudio.Play();
            }
            else if (isGrounded && isFalling)
            {
                // Landed
                isFalling = false;
                if (fallAudio != null && fallAudio.isPlaying)
                    fallAudio.Stop();

                if (walkAudio != null)
                    walkAudio.Play();
            }
        }

        wasGrounded = isGrounded;

        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.Space))
            is2DMode = !is2DMode;
        
        Vector3 input = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W)) input = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S)) input = Vector3.back;
        if (Input.GetKeyDown(KeyCode.A)) input = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) input = Vector3.right;

        Vector3 direction = RotateInput(input, cameraFollow.directionIndex);

        if (!is2DMode && Input.GetKeyDown(KeyCode.E)) direction = Vector3.up;

        if (is2DMode && (direction == Vector3.forward || direction == Vector3.back))
            return;

        if (direction != Vector3.zero)
        {
            TryMove(direction);
        }
        Vector3 RotateInput(Vector3 input, int index)
        {
            switch (index % 4)
            {
                case 0: return input;                              // Behind (default)
                case 1: return new Vector3(-input.z, 0, input.x);  // Left of player
                case 2: return new Vector3(-input.x, 0, -input.z); // In front of player
                case 3: return new Vector3(input.z, 0, -input.x);  // Right of player
                default: return input;
            }
        }

    }

    void TryMove(Vector3 dir)
    {
        Vector3 origin = transform.position;
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
            StartCoroutine(FlipMove(dir));
        }
    }

    IEnumerator FlipMove(Vector3 dir)
    {
        isMoving = true;

        // Stop falling audio if playing
        if (fallAudio != null && fallAudio.isPlaying)
        {
            fallAudio.Stop();
        }
        isFalling = false;

        // Play walk sound only if grounded
        if (walkAudio != null && IsGrounded())
        {
            walkAudio.Play();
        }

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
        return new Vector3(
            Mathf.Round(v.x * 10) / 10f,
            Mathf.Round(v.y * 10) / 10f,
            Mathf.Round(v.z * 10) / 10f
        );
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f);
    }
}
