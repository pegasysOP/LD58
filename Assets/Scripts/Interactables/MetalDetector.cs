using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class MetalDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    public float range = 2f;
    public float ignoreAngleRange = 0.5f;
    public float maxAngle = 90f;
    public LayerMask metalDetectionMask;

    [Header("Battery Settings")]
    public float maxBattery = 100f;
    public float dischargeRate = 10f;
    private float battery;

    [Header("Audio Settings")]
    public AudioClip metalDetectorBeep;
    public AudioClip foundMetalDetectorBeep;
    public float minPitch = 0.8f;
    public float maxPitch = 2.0f;
    private float minBeepInterval = 0.2f;
    private float maxBeepInterval = 0.5f;
    private float beepInterval = 0.5f;

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
        if (GameManager.Instance.LOCKED)
            return;


        if (mouse.rightButton.isPressed)
        {
            Detect();
        }
        else
        {
            beepTimer = 0f;
            audioSource.Stop();
        }
    }

    public void Detect()
    {
        DischargeBattery();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, metalDetectionMask);
        if (hitColliders.Length == 0)
        {
            beepTimer = 0f;
            return;
        }

        Vector3 forward = Camera.main.transform.forward;
        Vector2 forward2D = new Vector2(forward.x, forward.z).normalized;

        List<Collider> validTargets = hitColliders
            .Where(hit =>
            {
                Vector2 target2D = new Vector2(hit.transform.position.x - transform.position.x,
                                               hit.transform.position.z - transform.position.z);
                float distance2D = target2D.magnitude;

                float horizontalAngle = Vector2.Angle(forward2D, target2D.normalized);

                return horizontalAngle <= maxAngle || distance2D < ignoreAngleRange;
            })
            .ToList();

        if (validTargets.Count == 0)
        {
            beepTimer = 0f;
            return;
        }

        Collider closestTarget = validTargets
            .OrderBy(hit =>
            {
                Vector2 target2D = new Vector2(hit.transform.position.x - transform.position.x,
                                               hit.transform.position.z - transform.position.z);
                return target2D.magnitude;
            })
            .First();

        Vector2 closestTarget2D = new Vector2(closestTarget.transform.position.x - transform.position.x,
                                              closestTarget.transform.position.z - transform.position.z);
        float closestDistance2D = closestTarget2D.magnitude;

        float proximity = Mathf.Clamp01(1f - (closestDistance2D / range));
        beepInterval = Mathf.Lerp(maxBeepInterval, minBeepInterval, proximity);

 
        float horizontalAngleToTarget = Vector2.Angle(forward2D, closestTarget2D.normalized);
        if (closestDistance2D > ignoreAngleRange)
        {
            float normalizedAngle = 1f - Mathf.Clamp01(horizontalAngleToTarget / maxAngle);
            float exponent = 2f;
            float curvedAngle = Mathf.Pow(normalizedAngle, exponent);

            audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, curvedAngle);
        }
        else
        {
            audioSource.pitch = maxPitch;
        }

        Debug.Log($"Pitch: {audioSource.pitch:F2} | Horizontal Angle: {horizontalAngleToTarget:F1}° | Horizontal Distance: {closestDistance2D:F2}");

        beepTimer += Time.deltaTime;
        if (beepTimer >= beepInterval)
        {
            PlayBeep(proximity);
            beepTimer = 0f;
        }
    }

    void PlayBeep(float proximity)
    {
        Debug.Log("Proximity: " + proximity);
        if (audioSource == null || metalDetectorBeep == null)
            return;

        
        if(proximity >= 0.75f)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(foundMetalDetectorBeep);
        }
        else
        {
            audioSource.Stop();
            audioSource.PlayOneShot(metalDetectorBeep);
        }
            
        AudioManager.Instance.StartDuckAudio(AudioManager.Instance.musicSource, 0.1f, 0.5f, 0.5f);

    }

    void DischargeBattery()
    {
        battery -= dischargeRate * Time.deltaTime;
        GameManager.Instance.hudController.UpdateBatteryText(battery);
        if (battery <= 0f)
        {
            battery = 0f;
            return;
        }
    }
}