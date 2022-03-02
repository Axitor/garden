using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemBar : MonoBehaviour
{
    /// <summary>
    /// Cashe objects
    /// </summary>
    private HealthSystem _parentHealthSystem;

    /// <summary>
    /// Actions on awake
    /// </summary>
    private void Start()
    {
        // Cache parent
        _parentHealthSystem = transform.parent.parent.parent.gameObject.GetComponent<HealthSystem>();
        _parentHealthSystem.CacheHealthSystemBar(this);

        // Fill full bar
        transform.localScale = new Vector3(1, 1, 0);
    }

    /// <summary>
    /// Change HP bar
    /// </summary>
    public void ChangeBar(int healthPoints, int defaultHealthPoints)
    {
        float percent = healthPoints / (defaultHealthPoints / 100);
        transform.localScale = new Vector3(percent / 100, 1, 0);
    }
}
