using UnityEngine;
using Axitor.Utils;

public class HealthSystem : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private int _healthPoints = 100;
    [SerializeField] private GameObject _hitVFX;
    [SerializeField] private GameObject _deathVFX;

    /// <summary>
    /// Private fields
    /// </summary>
    private int _defaultHealthPoints;
    private HealthSystemBar _childHealthSystemBar;

    /// <summary>
    /// Cache HP on awake
    /// </summary>
    public void Awake()
    {
        _defaultHealthPoints = _healthPoints;
    }

    /// <summary>
    /// Cache child health system bar
    /// </summary>
    public void CacheHealthSystemBar(HealthSystemBar bar)
    {
        _childHealthSystemBar = bar;
    }

    /// <summary>
    /// Get remaining points
    /// </summary>
    public int GetPoints()
    {
        return _healthPoints;
    }

    /// <summary>
    /// Sets damage and returns remaining HP
    /// </summary>
    public int SetDamage(int damage)
    {
        _healthPoints -= damage;

        if (_healthPoints <= 0)
        {
            Death();
        }
        else
        {
            VFX(_hitVFX);

            if(_childHealthSystemBar && _childHealthSystemBar != null)
            {
                _childHealthSystemBar.ChangeBar(_healthPoints, _defaultHealthPoints);
            }
        }

        return _healthPoints;
    }

    /// <summary>
    /// Death
    /// </summary>
    private void Death()
    {
        // Set HP to 0
        _healthPoints = 0;

        // Delete attacker from a lane and count him
        if (gameObject.tag == "Attacker")
        {
            Registry.AttackerSpawner?.LaneDelete(transform.position.y);
            Registry.AttackerSpawner?.AddDeadEnemy();
            TextSystem.UpdateEnemyKilledTextField(Registry.DeadEnemyAmount.ToString());
            TextSystem.UpdateEnemyKilledLevelTextField(Registry.AttackerSpawner?.DeadLevelEnemyAmount.ToString() + "/" + Registry.AttackerSpawner?.MaxLevelEnemyAmount.ToString());
        }

        // Destroy gameobject
        Destroy(gameObject);

        //Show death effect
        VFX(_deathVFX);
    }

    /// <summary>
    /// VFX
    /// </summary>
    private void VFX(GameObject vfx)
    {
        if (vfx == null) 
        {
            Debug.Log("Error No Health System VFX.");
            return; 
        }

        GameObject tempVFX = Instantiate(vfx, transform.position, Quaternion.identity);
        tempVFX.transform.parent = Registry.GameObjectsGet("VFX")?.transform;
        Destroy(tempVFX, 1f);
    }
}
