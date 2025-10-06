using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Radio : MonoBehaviour
{
    private Keyboard keyboard;
    private int currentSongIndex = 0;
    private List<AudioClip> playlist;
    private bool isPlaying = false;
    private bool waitingForStatic = false;

    public LayerMask radioMask;
    public float range = 3f;

    void Start()
    {
        keyboard = Keyboard.current;
    }

    void Update()
    {
        AudioSource musicSource = AudioManager.Instance.musicSource;

        if (!isPlaying)
        {
            isPlaying = true;
            CreatePlaylist();
            PlayCurrentSong();
        }

        if (!musicSource.isPlaying && !waitingForStatic)
        {
            AdvanceSong();
        }

        if (waitingForStatic && !musicSource.isPlaying)
        {
            waitingForStatic = false;
            AdvanceSong();
        }

        Physics.Raycast(GameManager.Instance.cameraController.playerCamera.transform.position,
                        GameManager.Instance.cameraController.playerCamera.transform.forward,
                        out RaycastHit hitInfo, range, radioMask);

        if (hitInfo.collider == null)
        {
            GameManager.Instance.hudController.ShowRadioPrompt(false);
            return;
        }
        GameManager.Instance.hudController.ShowRadioPrompt(true);

        if (keyboard.eKey.wasPressedThisFrame)
        {
            PlayStaticThenNextSong();
        }
    }

    void PlayStaticThenNextSong()
    {
        if (AudioManager.Instance.staticClip == null)
        {
            AdvanceSong();
            return;
        }

        waitingForStatic = true;
        AudioManager.Instance.musicSource.clip = AudioManager.Instance.staticClip;
        AudioManager.Instance.musicSource.Play();
    }

    void AdvanceSong()
    {
        currentSongIndex = (currentSongIndex + 1) % playlist.Count;
        PlayCurrentSong();
    }

    void PlayCurrentSong()
    {
        AudioManager.Instance.PlayMusic(playlist[currentSongIndex], AudioManager.FadeType.FadeIn, 1f);
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
