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

        Vector3 forward = transform.forward;
        
        //Get all targets less than max angle from players forward direction 
        List<Collider> validTargets = hitColliders
        .Where(hit =>
        {
            Vector3 directionToTarget = (hit.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, hit.transform.position);

            float angle = Vector3.Angle(forward, directionToTarget);
            float normalisedAngle = 1 - Mathf.Clamp01(angle / maxAngle);
            
            if(distance > 1)
            {
                audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalisedAngle);

            }
            return angle <= maxAngle || distance < ignoreAngleRange;
        })
        .ToList();

        if (validTargets.Count == 0)
        {
            beepTimer = 0f;
            return;
        }
        
        //Ensure valid targets are within range 
        float closestDistance = hitColliders
            .Select(hit => Vector3.Distance(transform.position, hit.transform.position))
            .Min();

        float proximity = Mathf.Clamp01(1f - (closestDistance / range));
        beepInterval = Mathf.Lerp(maxBeepInterval, minBeepInterval, proximity);
        Debug.Log(beepInterval);

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