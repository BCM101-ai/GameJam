using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    public float delayBeforeDestroy = 1f; // You can change this in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("DestroyBlock", delayBeforeDestroy);
        }
    }

    void DestroyBlock()
    {
        Destroy(gameObject);
    }
}
