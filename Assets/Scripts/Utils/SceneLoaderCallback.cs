using UnityEngine;
using Axitor.Utils;

public class SceneLoaderCallback : MonoBehaviour
{
    /// <summary>
    /// Private fields
    /// </summary>
    private bool _isFirstUpdate = true;

    /// <summary>
    /// Set bool on first update
    /// </summary>
    void Update()
    {
        if (_isFirstUpdate)
        {
            _isFirstUpdate = false;
            SceneLoader.LoadCallback();
        }
    }
}
