using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrder : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private int orderInLayer = 1000;
    [SerializeField] private int offset = 0;
    [SerializeField] private bool runOnce = false;

    /// <summary>
    /// Private fields
    /// </summary>
    private Renderer _renderer;
    
    /// <summary>
                                  /// Actions on awake
                                  /// </summary>
    void Awake()
    {
        // Cache component
        _renderer = gameObject.GetComponent<Renderer>();
    }

    /// <summary>
    /// Actions on update
    /// </summary>
    void Update()
    {
        // Set order in layer based on Y position & offset
        _renderer.sortingOrder = (int)(orderInLayer - transform.parent.position.y * 100 - offset);

        // Destroy if run once
        if (runOnce)
        {
            Destroy(this);
        }
    }
}
