using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlayer : MonoBehaviour
{
    public float flipDuration = 0.2f;
    public LayerMask obstacleMask;
    public float climbSearchRadius = 1.5f;
    public float maxClimbHeight = 1.5f;

    public AudioSource walkAudio;
    public AudioSource fallAudio;
    public float groundCheckDistance = 0.6f;

    public CameraFollow cameraFollow;

    private bool isMoving = false;
    private bool is2DMode = false;
    private bool wasGrounded = true;
    private bool isFalling = false;

    void Start()
    {
        if (cameraFollow == null)
            cameraFollow = FindObjectOfType<CameraFollow>();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();

        if (!isMoving)
        {
            if (!isGrounded && !isFalling)
            {
                isFalling = true;
                if (fallAudio != null && !fallAudio.isPlaying)
                    fallAudio.Play();
            }
            else if (isGrounded && isFalling)
            {
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

        Vector3 direction = Vector3.zero;

        if (!is2DMode)
        {
            float yRot = Camera.main.transform.eulerAngles.y;
            float closest = 0f;
            float[] angles = { 45f, 135f, 225f, 315f };
            float minDiff = 999f;

            foreach (float angle in angles)
            {
                float diff = Mathf.Abs(Mathf.DeltaAngle(yRot, angle));
                if (diff < minDiff)
                {
                    minDiff = diff;
                    closest = angle;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                direction = closest switch
                {
                    45f => Vector3.right,
                    135f => Vector3.back,
                    225f => Vector3.left,
                    315f => Vector3.forward,
                    _ => Vector3.zero
                };
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                direction = closest switch
                {
                    45f => Vector3.left,
                    135f => Vector3.forward,
                    225f => Vector3.right,
                    315f => Vector3.back,
                    _ => Vector3.zero
                };
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = closest switch
                {
                    45f => Vector3.forward,
                    135f => Vector3.right,
                    225f => Vector3.back,
                    315f => Vector3.left,
                    _ => Vector3.zero
                };
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                direction = closest switch
                {
                    45f => Vector3.back,
                    135f => Vector3.left,
                    225f => Vector3.forward,
                    315f => Vector3.right,
                    _ => Vector3.zero
                };
            }
            if (Input.GetKeyDown(KeyCode.E)) direction = Vector3.up;
        }
        if (is2DMode && cameraFollow != null)
        {
            int viewIndex = cameraFollow.currentViewIndex;

            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = viewIndex switch
                {
                    0 => Vector3.right,
                    1 => Vector3.forward,
                    2 => Vector3.left,
                    3 => Vector3.back,
                    _ => Vector3.zero
                };
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                direction = viewIndex switch
                {
                    0 => Vector3.left,
                    1 => Vector3.back,
                    2 => Vector3.right,
                    3 => Vector3.forward,
                    _ => Vector3.zero
                };
            }

        }


        if (direction != Vector3.zero)
        {
            TryMove(direction.normalized);
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

        if (fallAudio != null && fallAudio.isPlaying)
        {
            fallAudio.Stop();
        }
        isFalling = false;

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

        yield return null; // Wait one frame before snapping
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
            Mathf.Round(v.x),
            Mathf.Round(v.y),
            Mathf.Round(v.z)
        );
    }


    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f);
    }
}