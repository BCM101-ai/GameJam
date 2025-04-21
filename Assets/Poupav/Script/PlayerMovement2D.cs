using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        if (!enabled) return; // Only move when script is enabled

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}