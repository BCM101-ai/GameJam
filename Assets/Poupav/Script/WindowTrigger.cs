using UnityEngine;
using System.Collections;

public class WindowTrigger : MonoBehaviour
{
    public Transform player;                  
    public Transform teleportTarget;          
    public float smoothDuration = 0.1f;       

    public DimensionManager dimensionManager;  // 👈 Drag this in Inspector

    void OnTriggerEnter(Collider other)
    {
        // ✅ Only teleport if in 2D mode and has teleport target
        if (other.transform == player && teleportTarget != null && dimensionManager.is2D)
        {
            StartCoroutine(SmoothTeleport(teleportTarget.position));
        }
    }

    IEnumerator SmoothTeleport(Vector3 targetPos)
    {
        float elapsed = 0f;
        Vector3 start = player.position;

        while (elapsed < smoothDuration)
        {
            player.position = Vector3.Lerp(start, targetPos, elapsed / smoothDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.position = targetPos;
    }
}