using UnityEngine;

public class WaterRiser : MonoBehaviour
{
    public float riseSpeed = 0.5f; // Speed of rising in units per second

    void Update()
    {
        // Move the water up every frame
        transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
    }
}
