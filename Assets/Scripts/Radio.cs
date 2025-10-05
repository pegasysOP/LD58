using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Radio : MonoBehaviour
{
    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Keyboard keyboard;
    private int currentSongIndex = 0;
    private List<AudioClip> playlist;

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlayMusic(audioManager.hawiiSongClip, AudioManager.FadeType.None);
        keyboard = Keyboard.current;

        playlist = new List<AudioClip>()
        {
            AudioManager.Instance.hawiiSongClip,
            AudioManager.Instance.jazzSongClip
        };

        AudioManager.Instance.PlayMusic(playlist[currentSongIndex], AudioManager.FadeType.None);
    }

    // Update is called once per frame
    void Update()
    {

        if (keyboard.mKey.wasPressedThisFrame)
        {
            currentSongIndex = (currentSongIndex + 1) % playlist.Count;
            AudioManager.Instance.PlayMusic(playlist[currentSongIndex], AudioManager.FadeType.FadeIn);
        }
    }
}
