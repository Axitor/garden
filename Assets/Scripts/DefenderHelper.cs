using UnityEngine;
using Axitor.Utils;

public class DefenderHelper : MonoBehaviour
{
    /// <summary>
    /// Cashe objects
    /// </summary>
    private Defender _parentDefender;

    /// <summary>
    /// Cache parent on awake
    /// </summary>
    private void Awake()
    {
        _parentDefender = transform.parent.gameObject.GetComponent<Defender>();
    }

    /// <summary>
    /// Add resources on a trophy star event
    /// </summary>
    private void ResourceSystemAdd()
    {
        ResourceSystem.AddAmount();
    }

    /// <summary>
    /// Send start shoot animation event to a parent obj
    /// </summary>
    private void CactusShootStart()
    {
        _parentDefender?.ShootStart();
    }

    /// <summary>
    /// Send start shoot animation event to a parent obj
    /// </summary>
    private void GnomeShootStart()
    {
        _parentDefender?.ShootStart();
    }
}
