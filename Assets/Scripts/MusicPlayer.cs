using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    /// <summary>
    /// Private fields
    /// </summary>
    private static AudioSource _audioSource;

    /// <summary>
    /// Actions on awake
    /// </summary>
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        SetVolume();
    }

    /// <summary>
    /// Set volume
    /// </summary>
    private void SetVolume()
    {
        _audioSource.volume = OptionsSystem.GetAudioVolume();
    }
}
