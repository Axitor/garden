using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Axitor.Utils;

public class LevelTimer : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private int _levelTimerSeconds = 100;
    [SerializeField] private Text _timerText;

    /// <summary>
    /// Private fields
    /// </summary>
    private bool _isFinished = false;
    private float _prevTimeSinceLevelLoad = 0f;

    /// <summary>
    /// Actions on awake
    /// </summary>
    private void Awake()
    {
        // Add to registry
        Registry.LevelTimer = this;
    }

    /// <summary>
    /// Update timer
    /// </summary>
    private void Update()
    {
        if(!_isFinished && _timerText != null && Time.timeSinceLevelLoad != _prevTimeSinceLevelLoad)
        {
            if (Time.timeSinceLevelLoad <= _levelTimerSeconds && 0 < (_levelTimerSeconds - Time.timeSinceLevelLoad))
            {
                Registry.TotalSeconds += Time.timeSinceLevelLoad - _prevTimeSinceLevelLoad;
                _prevTimeSinceLevelLoad = Time.timeSinceLevelLoad;

                TimeSpan time = TimeSpan.FromSeconds(_levelTimerSeconds - Time.timeSinceLevelLoad);
                _timerText.text = time.ToString(@"hh\:mm\:ss");
            }
            else
            {
                _isFinished = true;
                Registry.LevelController?.LevelFinished();
            }
        }
    }

}
