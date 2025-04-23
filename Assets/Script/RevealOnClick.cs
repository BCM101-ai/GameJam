using UnityEngine;

public class RevealOnClick : MonoBehaviour
{
    public GameObject hiddenObject; // Assign the object you want to reveal in the Inspector

    void Start()
    {
        if (hiddenObject != null)
        {
            hiddenObject.SetActive(false); // Start hidden
        }
    }

    void OnMouseDown()
    {
        if (hiddenObject != null)
        {
            hiddenObject.SetActive(true); // Reveal when clicked
        }
    }
}
