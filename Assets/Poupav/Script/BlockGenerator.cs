using UnityEngine;
using System.Collections;
public class BlockGenerator : MonoBehaviour
{
    public GameObject blockPrefab;
    public int numberOfBlocks = 5;
    public Vector3 direction = Vector3.right;
    public float spacing = 1f;
    public float spawnDelay = 0.2f;  // 🔄 Delay between block spawns

    private bool hasGenerated = false;

    public void ExpandPath()
    {
        if (!hasGenerated)
        {
            StartCoroutine(GenerateBlocksOneByOne());
            hasGenerated = true;
        }
    }

    IEnumerator GenerateBlocksOneByOne()
    {
        for (int i = 1; i <= numberOfBlocks; i++)
        {
            Vector3 spawnPos = transform.position + direction.normalized * spacing * i;
            Instantiate(blockPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}