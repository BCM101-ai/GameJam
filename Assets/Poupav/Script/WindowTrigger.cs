using UnityEngine;
using System.Collections;

public class WindowTrigger : MonoBehaviour
{
    public Transform player;                  
    public Transform teleportTarget;          
    public float smoothDuration = 0.1f;       

    public DimensionManager dimensionManager; // 👈 Drag in from Inspector
    [Range(0, 3)] public int requiredViewIndex = 0; // 👈 Set this per trigger

    void OnTriggerEnter(Collider other)
    {
        // ✅ Only trigger if:
        // 1. It’s the player
        // 2. A teleport target exists
        // 3. We're in 2D mode
        // 4. The camera is currently on the correct view
        if (other.transform == player 
            && teleportTarget != null 
            && dimensionManager != null 
            && dimensionManager.is2D 
            && dimensionManager.cameraFollow.currentViewIndex == requiredViewIndex)
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