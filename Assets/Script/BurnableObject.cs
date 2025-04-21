using UnityEngine;

public class BurnableObject : MonoBehaviour
{
    public GameObject burnEffectPrefab;
    public float burnDelay = 1f;
    public float effectLifetime = 3f;

    private bool isBurning = false;
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void OnMouseDown()
    {
        if (isBurning || !playerInRange) return;

        if (CompareTag("Burn"))
        {
            StartCoroutine(Burn());
        }
    }

    System.Collections.IEnumerator Burn()
    {
        isBurning = true;

        GameObject effect = null;
        if (burnEffectPrefab != null)
        {
            effect = Instantiate(burnEffectPrefab, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(burnDelay);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, 0.1f);

        if (effect != null)
        {
            Destroy(effect, effectLifetime);
        }
    }
}
