using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OptionsSystem
{
    /// <summary>
    /// Private fields
    /// </summary>
    private static string _audioVolumeKey = "masterAudioVolume";
    private static float _audioVolumeValue = 0.3f;
    private static bool _isVolumeUpdated = false;

    /// <summary>
    /// Set master audio volume
    /// </summary>
    public static void SetAudioVolume(float volume)
    {
        if (0 <= volume && volume <= 1)
        {
            _isVolumeUpdated = true;
            _audioVolumeValue = volume;
            PlayerPrefs.SetFloat(_audioVolumeKey, _audioVolumeValue);
        }
    }

    /// <summary>
    /// Get master audio volume
    /// </summary>
    public static float GetAudioVolume()
    {
        float prefsVolume = (!_isVolumeUpdated) ? PlayerPrefs.GetFloat(_audioVolumeKey, -1f) : -1f;
        float tpmVolume = (!_isVolumeUpdated && prefsVolume != -1f) ? prefsVolume : _audioVolumeValue;
        return tpmVolume;
    }

    /// <summary>
    /// Save prefs
    /// </summary>
    public static void Save()
    {
        PlayerPrefs.Save();
    }
}
