using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WaterRiser : MonoBehaviour
{
    public float riseSpeed = 0.5f;
    public Transform player;
    public float maxVolumeDistance = 10f;
    public float waterHeight = 5f; // thickness/height of water cube (adjust if needed)
    public AudioClip ambientWaterSound;
    public AudioClip drownSound;

    private AudioSource waterAudioSource;
    private AudioSource drownAudioSource;

    void Start()
    {
        // Setup ambient water sound
        waterAudioSource = gameObject.AddComponent<AudioSource>();
        waterAudioSource.clip = ambientWaterSound;
        waterAudioSource.loop = true;
        waterAudioSource.playOnAwake = false;
        waterAudioSource.spatialBlend = 0f; // 2D sound
        waterAudioSource.Play();

        // Setup drowning sound
        drownAudioSource = gameObject.AddComponent<AudioSource>();
        drownAudioSource.clip = drownSound;
        drownAudioSource.loop = true;
        drownAudioSource.playOnAwake = false;
        drownAudioSource.spatialBlend = 0f;
    }

    void Update()
    {
        // Move the water up
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        // Adjust ambient volume based on distance
        float distance = Mathf.Abs(player.position.y - transform.position.y);
        float volume = Mathf.Clamp01(1f - (distance / maxVolumeDistance));
        waterAudioSource.volume = volume;

        // Check if player is under water (simplified vertical check)
        float waterTopY = transform.position.y + waterHeight / 2f;
        float waterBottomY = transform.position.y - waterHeight / 2f;

        if (player.position.y < waterTopY && player.position.y > waterBottomY)
        {
            if (!drownAudioSource.isPlaying)
                drownAudioSource.Play();
        }
        else
        {
            if (drownAudioSource.isPlaying)
                drownAudioSource.Stop();
        }
    }
}
