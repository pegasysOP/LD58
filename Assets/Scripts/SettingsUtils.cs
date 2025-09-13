using UnityEngine;

public class SettingsUtils : MonoBehaviour
{
    private const string SensitivityKey = "Sensitivity";
    private const string MasterVolumeKey = "MasterVolume";
    private const string SfxVolumeKey = "SfxVolume";

    public static float GetSensitivity()
    {
        return PlayerPrefs.GetFloat(SensitivityKey, 1f);
    }

    public static void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat(SensitivityKey, value);
        PlayerPrefs.Save();
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MasterVolumeKey, 1.0f);
    }

    public static void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, value);
        PlayerPrefs.Save();
    }

    public static float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat(SfxVolumeKey, 1.0f);
    }

    public static void SetSfxVolume(float value)
    {
        PlayerPrefs.SetFloat(SfxVolumeKey, value);
        PlayerPrefs.Save();
    }
}
