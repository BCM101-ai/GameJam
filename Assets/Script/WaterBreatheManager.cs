using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaterBreatheManager : MonoBehaviour
{
    public Transform player;
    public Collider waterArea;

    public Image[] bubbles; // Use Image (not RawImage)
    public float timePerBubble = 2f;

    private float bubbleTimer = 0f;
    private int bubbleIndex = 0;
    private bool isDrowning = false;

    void Update()
    {
        // Check if player is inside water
        bool isUnderwater = waterArea.bounds.Contains(player.position);

        if (isUnderwater)
        {
            bubbleTimer += Time.deltaTime;

            if (bubbleIndex < bubbles.Length && bubbleTimer >= timePerBubble)
            {
                bubbles[bubbleIndex].enabled = false;
                bubbleIndex++;
                bubbleTimer = 0f;
            }

            if (bubbleIndex >= bubbles.Length && !isDrowning)
            {
                isDrowning = true;
                SceneManager.LoadScene("GameOver"); // Replace with your death scene name
            }
        }
        else
        {
            // Reset if player exits water
            bubbleTimer = 0f;
            bubbleIndex = 0;
            isDrowning = false;

            foreach (Image bubble in bubbles)
            {
                bubble.enabled = true;
            }
        }
    }
}
