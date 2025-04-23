using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WaterRiser : MonoBehaviour
{
    public float riseSpeed = 0.5f;
    public Transform player;
    public float maxVolumeDistance = 10f;
    public AudioClip ambientWaterSound;

    private AudioSource waterAudioSource;

    void Start()
    {
        // Set up ambient water sound
        waterAudioSource = gameObject.AddComponent<AudioSource>();
        waterAudioSource.clip = ambientWaterSound;
        waterAudioSource.loop = true;
        waterAudioSource.playOnAwake = false;
        waterAudioSource.spatialBlend = 0f; // 2D sound
        waterAudioSource.Play();
    }

    void Update()
    {
        // Move the water up
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        // Adjust water sound volume based on vertical distance
        float distance = Mathf.Abs(player.position.y - transform.position.y);
        float volume = Mathf.Clamp01(1f - (distance / maxVolumeDistance));
        waterAudioSource.volume = volume;
    }
}
