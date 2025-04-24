using UnityEngine;
using UnityEngine.UI;

public class DrowningManager : MonoBehaviour
{
    [Header("UI Bubbles")]
    public Image[] bubbles;             // Assign your 3 UI bubbles here in order (left to right)
    public Sprite blueBubbleSprite;     // Drag your blue bubble sprite here

    [Header("Drowning Settings")]
    public float bubbleFillDelay = 1.5f; // Time before each bubble turns blue

    private bool isUnderwater = false;
    private float drownTimer = 0f;
    private int bubblesFilled = 0;

    void Update()
    {
        if (isUnderwater)
        {
            drownTimer += Time.deltaTime;

            if (drownTimer >= bubbleFillDelay && bubblesFilled < bubbles.Length)
            {
                bubbles[bubblesFilled].sprite = blueBubbleSprite;
                bubblesFilled++;
                drownTimer = 0f;

                if (bubblesFilled >= bubbles.Length)
                {
                    Die();
                }
            }
        }
        else
        {
            // Optional: Reset when leaving water
            drownTimer = 0f;
            if (bubblesFilled > 0)
            {
                for (int i = 0; i < bubblesFilled; i++)
                    bubbles[i].color = Color.white; // Reset sprite or color
                bubblesFilled = 0;
            }
        }
    }

    void Die()
    {
        Debug.Log("Player drowned!");
        Destroy(gameObject); // or trigger Game Over logic
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
            isUnderwater = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
            isUnderwater = false;
    }
}