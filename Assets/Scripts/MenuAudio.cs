using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source.volume = SettingsUtils.GetMasterVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
