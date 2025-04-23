using UnityEngine;

public class BurnableObject : MonoBehaviour
{
    public GameObject burnEffectPrefab;
    public float burnDelay = 1f;
    public float effectLifetime = 3f;
    public AudioClip burnSound;

    private bool isBurning = false;
    private bool playerInRange = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

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

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the clicked object is this burnable object
                if (hit.collider.gameObject == gameObject && !isBurning && playerInRange && CompareTag("Burn"))
                {
                    StartCoroutine(Burn());
                }
            }
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

        if (burnSound != null)
        {
            audioSource.PlayOneShot(burnSound);
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
