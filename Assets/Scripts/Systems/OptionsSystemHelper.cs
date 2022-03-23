using UnityEngine;
using UnityEngine.UI;

public class OptionsSystemHelper : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private Slider _audioVolumeSlider;

    /// <summary>
    /// Set slider value on start
    /// </summary>
    private void Start()
    {
        _audioVolumeSlider.value = OptionsSystem.GetAudioVolume();
    }

    /// <summary>
    /// Save slider volume on change
    /// </summary>
    public void SaveAudioVolume()
    {
        OptionsSystem.SetAudioVolume(_audioVolumeSlider.value);
        OptionsSystem.Save();
    }
}
