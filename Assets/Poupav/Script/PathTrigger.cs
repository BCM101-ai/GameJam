using UnityEngine;

public class PathTrigger : MonoBehaviour
{
    public BlockGenerator targetPathExpander;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (targetPathExpander != null)
            {
                targetPathExpander.ExpandPath();
            }
        }
    }
}