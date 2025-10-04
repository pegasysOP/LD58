using UnityEngine;

public class Radio : MonoBehaviour
{
    AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlayMusic(audioManager.hawiiSongClip, AudioManager.FadeType.FadeIn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
