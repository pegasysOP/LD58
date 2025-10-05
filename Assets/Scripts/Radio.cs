using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Radio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Keyboard keyboard;
    private int currentSongIndex = 0;
    private List<AudioClip> playlist;
    private bool isPlaying = false;

    void Start()
    {
        keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            CreatePlaylist();
        }

        if (keyboard.mKey.wasPressedThisFrame)
        {
            currentSongIndex = (currentSongIndex + 1) % playlist.Count;
            AudioManager.Instance.PlayMusic(playlist[currentSongIndex], AudioManager.FadeType.FadeIn, 5f);
        }
    }

    void CreatePlaylist()
    {
        playlist = new List<AudioClip>()
        {
            AudioManager.Instance.hawiiSongClip,
            AudioManager.Instance.jazzSongClip,
            AudioManager.Instance.americanaSongClip
        };
    }
}
