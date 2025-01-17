using UnityEngine;
using UnityEngine.SocialPlatforms;
[RequireComponent(typeof(AudioSource))]

public class SoundSystem : MonoBehaviour
{
    [Header("References")]
    public AudioSource audioSource;
    PlayerMovementAdvanced pm;
    PlayerSpeed ps;

    [Header("Audio")]
    public AudioClip footstep;
    public AudioClip jump;
    public AudioClip grapplingHookLand;

    [Header("Variables")]
    private float footstepTimer = 0f;
    public float footstepSpeed;
    public float minFootstepSpeed;
    private float speed;

    private void Start()
    {
        pm = GetComponent<PlayerMovementAdvanced>();
        ps = GetComponent<PlayerSpeed>();
    }

    private void Update()
    {
        speed = ps.speed;
        footstepSpeed = minFootstepSpeed - (speed / 130f);

        if (footstepSpeed < 0.17f)
        {
            footstepSpeed = 0.17f;
        }

        footstepTimer -= Time.deltaTime;
    }

    public void PlayFootstep()
    {
        if (footstepTimer <= 0f && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            audioSource.pitch = Random.Range(0.92f, 1.12f);
            audioSource.PlayOneShot(footstep);
            footstepTimer = footstepSpeed;
        }
    }

    public void PlayJump()
    {
        audioSource.pitch = Random.Range(0.9f, 1.15f);
        audioSource.PlayOneShot(jump);
    }

    public void PlayGrapplingHookLand(Transform hitTransform)
    {
        GameObject tempAudioSource = new GameObject("TempAudio");
        tempAudioSource.transform.position = hitTransform.position;
        AudioSource tempSource = tempAudioSource.AddComponent<AudioSource>();

        tempSource.clip = grapplingHookLand;
        tempSource.pitch = Random.Range(0.9f, 1f);
        tempSource.volume = audioSource.volume;
        tempSource.spatialBlend = 1.0f;
        tempSource.minDistance = audioSource.minDistance;
        tempSource.maxDistance = audioSource.maxDistance;

        tempSource.Play();
        Destroy(tempAudioSource, grapplingHookLand.length);
    }
}