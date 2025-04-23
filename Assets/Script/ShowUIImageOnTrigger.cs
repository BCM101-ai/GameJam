using UnityEngine;
using UnityEngine.UI;

public class ShowUIImageOnTrigger : MonoBehaviour
{
    public RawImage uiRawImage;    // Drag your RawImage from the Canvas here
    public float displayTime = 3f; // Time to keep it visible

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowRawImageTemporarily());
        }
    }

    private System.Collections.IEnumerator ShowRawImageTemporarily()
    {
        uiRawImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        uiRawImage.gameObject.SetActive(false);
    }
}
