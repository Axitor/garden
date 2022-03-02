using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Axitor.Utils;

public class Attacker : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [Range(0f, 3f) ][SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private int _damagePoints = 10;

    /// <summary>
    /// Cashe objects
    /// </summary>
    private AttackerHelper _childAttackerHelper;

    /// <summary>
    /// Private fields
    /// </summary>
    private bool _isAttacking = false;
    private bool _isWalk = false;

    /// <summary>
    /// Actions on awake
    /// </summary>
    private void Awake()
    {
        // Cache child
        _childAttackerHelper = transform.GetChild(0).GetComponent<AttackerHelper>();
    }

    /// <summary>
    /// Make a walk step on update
    /// </summary>
    private void Update()
    {
        WalkStep();
    }

    /// <summary>
    /// Make one step movement to the left with walk speed
    /// </summary>
    private void WalkStep()
    {
        if (_isWalk)
        {
            transform.Translate(Vector2.left * _walkSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Start walk movement on animation event
    /// </summary>
    public void WalkStart()
    {
        _isWalk = true;
    }

    /// <summary>
    /// Actions on trigger
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Attack on trigger with a defender 
        if (collider.gameObject.tag == "Defender")
        {
            AttackStart(collider);
        }

        // Destroy on trigger with a collider out of the screen
        else if (collider.gameObject.tag == "Destroyer")
        {
            // Delete from a lane
            Registry.AttackerSpawner?.LaneDelete(transform.position.y);

            // Destroy an attacker
            Destroy(gameObject);
        }

        // Fail level on trigger with a level finish collider
        else if (collider.gameObject.tag == "LevelFinish")
        {
            Registry.LevelController?.LevelFinished(true);
        }
    }

    /// <summary>
    /// Start attacking a defender
    /// </summary>
    private void AttackStart(Collider2D collider)
    {
        _isWalk = false;
        _isAttacking = true;
        _childAttackerHelper.Animator.SetBool("isAttacking", true);

        Vector2 defenderPosition = collider.gameObject.transform.position;
        HealthSystem defenderHealthSystem = collider.gameObject.GetComponent<HealthSystem>();
        StartCoroutine(Attacking(defenderHealthSystem, defenderPosition));
    }

    /// <summary>
    /// Stop attacking a defender
    /// </summary>
    private void AttackStop(Vector2 defenderPosition)
    {
        _isWalk = true;
        _isAttacking = false;
        _childAttackerHelper.Animator.SetBool("isAttacking", false);
        Registry.DefenderSpawner.ClearField(defenderPosition);
    }

    /// <summary>
    /// Attacking a defender
    /// </summary>
    private IEnumerator Attacking(HealthSystem defenderHealthSystem, Vector2 defenderPosition)
    {
        while (_isAttacking)
        {
            if (!defenderHealthSystem || defenderHealthSystem == null || defenderHealthSystem.GetPoints() <= 0)
            {
                AttackStop(defenderPosition);
                yield return new WaitForSeconds(0);
            }
            else
            {
                defenderHealthSystem.SetDamage(_damagePoints);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
