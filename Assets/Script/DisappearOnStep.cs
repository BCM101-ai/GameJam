using UnityEngine;

public class DisappearOnStep : MonoBehaviour
{
    public float delay = 0.5f; // Delay before disappearing

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(Disappear), delay);
        }
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }
}
