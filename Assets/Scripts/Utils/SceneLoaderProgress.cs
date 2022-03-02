using UnityEngine;
using UnityEngine.UI;
using Axitor.Utils;

public class SceneLoaderProgress : MonoBehaviour
{
    /// <summary>
    /// Cashe objects
    /// </summary>
    private Image _progressBar;

    /// <summary>
    /// Cache a progress bar on awake
    /// </summary>
    private void Awake()
    {
        _progressBar = transform.GetComponent<Image>();
        _progressBar.fillAmount = 0f;
    }

    /// <summary>
    /// Fill a progress bar on update
    /// </summary>
    void Update()
    {
        _progressBar.fillAmount = SceneLoader.LoadingGetProgress();
    }
}
