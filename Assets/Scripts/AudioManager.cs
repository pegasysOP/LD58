using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource fadeSource;

    [Header("UI")]
    public AudioClip buttonPressClip;
    public AudioClip buttonHoverClip;
    public AudioClip pauseMenuOpenClip;
    public AudioClip pauseMenuClosedClip;

    [Header("SFX")]
    public AudioClip waveClip;
    public List<AudioClip> coinClips;
    public List<AudioClip> digSandClips;
    public List<AudioClip> walkingClips;
    public AudioClip seagullClip;
    public AudioClip staticClip;

    [Header("Interaction")]
    public AudioClip selectClip;
    public AudioClip selectBlockedClip;

    [Header("Music")]
    public AudioClip menuMusicClip;
    public AudioClip gameWonClip;
    public AudioClip gameLostClip;
    public AudioClip hawiiSongClip;
    public AudioClip jazzSongClip;
    public AudioClip americanaSongClip;

    [Header("Playlist")]
    private List<AudioClip> playlist;
    private int playlistIndex = 0;

    public static AudioManager Instance;

    private Coroutine currentCoroutine;

    private float remainingDuckTime = 0f;

    private float seagullDelay = 15f;
    private float seagullTimer = 15f;


    public enum FadeType { None, FadeIn, CrossFade }

    public void Init()
    {
        Instance = this;
        UpdateVolume(SettingsUtils.GetMasterVolume());
        Play(sfxSource, waveClip);

        CreatePlaylist();

        WarmMusicCache();

        PlayCurrentSongInPlaylist();
    }

    void WarmMusicCache()
    {
        foreach (AudioClip clip in playlist)
        {
            if (clip != null && musicSource != null)
            {
                musicSource.clip = clip;
                musicSource.Play();
                musicSource.Stop();
            }
        }
    }

    //==================== Playlist ===================
    
    void CreatePlaylist()
    {
        playlist = new List<AudioClip>()
        {
            hawiiSongClip,
            jazzSongClip,
            americanaSongClip
        };
    }

    void AdvanceSong()
    {
        playlistIndex = (playlistIndex + 1) % playlist.Count;
        PlayCurrentSongInPlaylist();
    }

    void Shuffle()
    {
        int tempPlaylistIndex = playlistIndex;
        while(tempPlaylistIndex == playlistIndex)
        {
            tempPlaylistIndex = Random.Range(0, playlist.Count);
        }
        playlistIndex = tempPlaylistIndex;
        AudioManager.Instance.PlayMusic(playlist[playlistIndex], AudioManager.FadeType.FadeIn, 1f);
    }

    void PlayCurrentSongInPlaylist()
    {
        AudioManager.Instance.PlayMusic(playlist[playlistIndex], AudioManager.FadeType.FadeIn, 1f);
    }

    void StopPlaylist()
    {
        AudioManager.Instance.Stop(musicSource, true);
    }

    void PausePlaylist()
    {
        AudioManager.Instance.musicSource.Pause();
    }

    void UnpausePlaylist()
    {
        AudioManager.Instance.musicSource.UnPause();
    }

    //==================== Utility ====================
    public bool IsClipPlaying(AudioSource source, AudioClip clip)
    {
        return source.isPlaying && source.clip == clip;
    }

    public void PlayMusic(AudioClip clip, FadeType fadeType = FadeType.None, float fadeTime = 2f, bool isDucking = false)
    {
        Play(musicSource, clip, fadeType, fadeTime, isDucking);
    }

    public void PlaySfx(List<AudioClip> audioClips)
    {
        PlaySfxWithPitchShifting(audioClips, 1.0f, 1.0f);
    }

    public void Play(AudioSource source, AudioClip clip, FadeType fadeType = FadeType.None, float fadeTime = 2f, bool isDucking = false)
    {
        if (source == null || clip == null)
        {
            Debug.LogError("ERROR: You must provide an audio source and clip to play on it");
        }
        if (isDucking)
        {
            if(fadeType != FadeType.None)
            {
                Debug.LogError("ERROR: Simultaneously ducking and fading is not supported!");
            }
            StartDuckAudio(source);
        }
        else if (fadeType == FadeType.FadeIn)
        {
            StartFadeIn(source, clip, fadeTime);
        }
        else if (fadeType == FadeType.CrossFade)
        {
            StartCrossFade(clip, fadeTime);
        }
        else
        {
            source.clip = clip;
            source.Play();
        }
    }

    public void Update()
    {
        seagullTimer -= Time.deltaTime;

        if(seagullTimer < 0)
        {
            PlaySeagullClip();
            seagullTimer = Random.Range(seagullDelay, seagullDelay * 2f);
        }

        if (!musicSource.isPlaying)
        {
            AdvanceSong();
        }
    }

    public void PlaySeagullClip()
    {
        sfxSource.PlayOneShot(seagullClip);
    }

    public void Stop(AudioSource source, bool fadeOutEnabled = false, float fadeTime = 2f)
    {
        if(source == null)
        {
            Debug.LogError("ERROR: Must provide a source to stop playing");
        }
        if (fadeOutEnabled)
        {
            StartFadeOut(source, fadeTime);
        }
        else
        {
            source.Stop();
        }
    }

    //==================== Interaction ====================

    public void PlaySfxWithPitchShifting(List<AudioClip> clips, float minPitch = 0.8f, float maxPitch = 1.2f)
    {
        if (sfxSource == null || clips == null || clips.Count == 0)
            Debug.LogError("ERROR: there are no sounds to pitch shift!");

        int index = Random.Range(0, clips.Count);
        sfxSource.pitch = Random.Range(minPitch, maxPitch);
        sfxSource.PlayOneShot(clips[index]);
        sfxSource.pitch = 1f;
    }

    //==================== Volume ====================

    public void UpdateVolume(float value)
    {
        musicSource.volume = value / 3;
        fadeSource.volume = value / 3;
        sfxSource.volume = value / 3;
    }

    public void UpdateSfxVolume(float value)
    {
        sfxSource.volume = value / 3;
    }

    //==================== Ducking ====================

    public void StartDuckAudio(AudioSource sourceToDuck, float duckVolumePercent = 0.3f, float duckDuration = 2f, float fadeTime = 0.5f)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(DuckAudio(sourceToDuck, duckVolumePercent, duckDuration, fadeTime));
    }

    private IEnumerator DuckAudio(AudioSource source, float duckVolumePercent, float duckDuration, float fadeTime)
{
    if (!source.isPlaying)
    {
        yield break;
    }

    float originalVolume = SettingsUtils.GetMasterVolume() / 3;
    float duckVolume = originalVolume * duckVolumePercent;

    // Fade down
    for (float t = 0; t < fadeTime; t += Time.deltaTime)
    {
        float normalized = t / fadeTime;
        source.volume = Mathf.Lerp(source.volume, duckVolume, normalized);
        yield return null;
    }

    source.volume = duckVolume;

    remainingDuckTime = Mathf.Min(remainingDuckTime, duckDuration);

    while (remainingDuckTime > 0f)
    {
        remainingDuckTime -= Time.deltaTime;
        yield return null;
    }

    for (float t = 0; t < fadeTime; t += Time.deltaTime)
    {
        float normalized = t / fadeTime;
        source.volume = Mathf.Lerp(duckVolume, originalVolume, normalized);
        yield return null;
    }

    source.volume = originalVolume;

    currentCoroutine = null;
    remainingDuckTime = 0f;
}

    //==================== Fading ====================

    private void StartFadeIn(AudioSource source, AudioClip clip, float duration)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FadeIn(source, clip, duration));
    }

    private IEnumerator FadeIn(AudioSource source, AudioClip clipToFadeIn, float fadeDuration)
    {
        source.Stop();

        float initialVolume = SettingsUtils.GetMasterVolume() / 3;
        source.volume = 0;

        source.clip = clipToFadeIn;
        source.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(0f, initialVolume, t / fadeDuration);
            yield return null;
        }
        source.volume = initialVolume;
        //source.volume = Mathf.Lerp(0, SettingsUtils.GetMasterVolume(), fadeDuration);
    }

    private void StartFadeOut(AudioSource source, float duration)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FadeOut(source, duration));
    }

    private IEnumerator FadeOut(AudioSource source, float fadeOutDuration)
    {
        for(float t = 0; t < fadeOutDuration; t+= Time.deltaTime)
        {
            source.volume = Mathf.Lerp(SettingsUtils.GetMasterVolume() / 3, 0f, t / fadeOutDuration);
            yield return null;
        }

        source.volume = 0;
        source.Stop();
    }

    private void StartCrossFade(AudioClip clipToFadeIn, float fadeOutDuration)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(CrossFade(clipToFadeIn, fadeOutDuration));
    }

    private IEnumerator CrossFade(AudioClip fadeInClip, float fadeDuration)
    {
        AudioSource fromSource = musicSource;
        AudioSource toSource = fadeSource;

        if (fromSource.clip == null)
        {
            Debug.LogError("CrossFade: No currently playing clip.");
            yield break;
        }

        // If the same clip is requested again, we can optionally skip
        if (fromSource.clip == fadeInClip)
        {
            Debug.LogWarning("WARNING: you are trying to switch to the same song as you are fading out ");
        }

        float masterVolume = SettingsUtils.GetMasterVolume();
        float fromStartVolume = fromSource.volume > 0f ? fromSource.volume : masterVolume / 3f;
        float toTargetVolume = masterVolume / 3f;

        // Setup fade in 
        toSource.clip = fadeInClip;
        toSource.volume = 0f;
        toSource.Play();

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / fadeDuration);

            fromSource.volume = Mathf.Lerp(fromStartVolume, 0f, normalized);
            toSource.volume = Mathf.Lerp(0f, toTargetVolume, normalized);

            yield return null;
        }

        fromSource.Stop();
        fromSource.volume = 0f;
        toSource.volume = toTargetVolume;

        // Swap roles after fade completes
        AudioSource temp = musicSource;
        musicSource = fadeSource;
        fadeSource = temp;

        currentCoroutine = null;

    }
}
