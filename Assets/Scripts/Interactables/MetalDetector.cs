using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class MetalDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    public float range = 2f;
    public LayerMask metalDetectionMask;

    [Header("Battery Settings")]
    public float maxBattery = 100f;
    public float dischargeRate = 10f;
    private float battery;

    [Header("Audio Settings")]
    public AudioClip metalDetectorBeep;
    public float minPitch = 0.8f;
    public float maxPitch = 2.0f;
    public float beepInterval = 0.1f;

    private float beepTimer = 0f;
    private Mouse mouse;
    private AudioSource audioSource;

    void Start()
    {
        mouse = Mouse.current;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = metalDetectorBeep;
        battery = maxBattery;
    }

    void Update()
    {
        if (mouse.rightButton.isPressed)
        {
            Detect();
        }
        else
        {
            beepTimer = 0f;
        }
    }

    public void Detect()
    {
        battery -= dischargeRate * Time.deltaTime;
        if (battery <= 0f) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, metalDetectionMask);
        if (hitColliders.Length == 0)
        {
            beepTimer = 0f;
            return;
        }

        float closestDistance = hitColliders
            .Select(hit => Vector3.Distance(transform.position, hit.transform.position))
            .Min();

        float proximity = Mathf.Clamp01(1f - (closestDistance / range));

        beepTimer += Time.deltaTime;

        if (beepTimer >= beepInterval)
        {
            PlayBeep(proximity);
            beepTimer = 0f;
        }
    }

    void PlayBeep(float proximity)
    {
        if (audioSource == null || metalDetectorBeep == null)
            return;

        audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, proximity);
        audioSource.PlayOneShot(metalDetectorBeep);
        AudioManager.Instance.StartDuckAudio(AudioManager.Instance.musicSource, 0.1f, 0.5f, 0.5f);

    }
}