using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public Slider sfxSlider;
    public Button quitButton;

    private void Awake()
    {
        sensitivitySlider.value = SettingsUtils.GetSensitivity();
        volumeSlider.value = SettingsUtils.GetMasterVolume();
        sfxSlider.value = SettingsUtils.GetSfxVolume();
    }

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityValueChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxValueChanged);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        //AudioManager.Instance.PauseMenuOpenClip();
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityValueChanged);
        volumeSlider.onValueChanged.RemoveListener(OnVolumeValueChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSfxValueChanged);
        quitButton.onClick.RemoveListener(OnQuitButtonClick);
        //AudioManager.Instance.PauseMenuClosedClip();
    }

    public void Toggle()
    {
        bool pausing = !isActiveAndEnabled;

        Cursor.visible = pausing;
        Cursor.lockState = pausing ? CursorLockMode.None : CursorLockMode.Locked;

        //GameManager.Instance.Pause(pausing);

        gameObject.SetActive(pausing);
    }

    private void OnSensitivityValueChanged(float newValue)
    {
        SaveNewSensitivity(newValue);
    }

    private void SaveNewSensitivity(float newValue)
    {
        SettingsUtils.SetSensitivity(newValue);

        GameManager.Instance?.cameraController?.UpdateSensitivity(newValue);
    }

    private void OnVolumeValueChanged(float newValue)
    {
        SaveNewVolume(newValue);

        GameManager.Instance?.audioManager?.UpdateVolume(newValue);
    }

    private void OnSfxValueChanged(float newValue)
    {
        SaveSfxVolume(newValue);

        GameManager.Instance?.audioManager?.UpdateSfxVolume(newValue);
    }

    private void SaveNewVolume(float newValue)
    {
        SettingsUtils.SetMasterVolume(newValue);
    }

    private void SaveSfxVolume(float newValue)
    {
        SettingsUtils.SetSfxVolume(newValue);
    }

    private void OnResumeButtonClick()
    {
        Toggle();
    }

    private void OnQuitButtonClick()
    {
        SceneUtils.LoadMenuScene();
    }
}
