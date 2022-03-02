using UnityEngine;

public class AttackerHelper : MonoBehaviour
{
    /// <summary>
    /// Public properties
    /// </summary>
    public Animator Animator { get { return _animator; } }

    /// <summary>
    /// Private objects
    /// </summary>
    private bool _isStarted = false;
    private Animator _animator;
    private Attacker _parentAttacker;

    /// <summary>
    /// Cache objects on awake
    /// </summary>
    private void Awake()
    {
        _parentAttacker = transform.parent.GetComponent<Attacker>();
        _animator = transform.GetComponent<Animator>();
    }

    /// <summary>
    /// Send start walk animation event to a parent obj
    /// </summary>
    private void WalkStart() 
    {
        _parentAttacker?.WalkStart();
    }

    /// <summary>
    /// Send start walk animation event to a parent obj - only once
    /// </summary>
    private void WalkStartOnce()
    {
        if (!_isStarted)
        {
            _isStarted = true;
            _parentAttacker?.WalkStart();
        }
    }
}
