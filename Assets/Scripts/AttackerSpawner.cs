using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Axitor.Utils;

public class AttackerSpawner : MonoBehaviour
{
    /// <summary>
    /// Public properties
    /// </summary>
    public int MaxLevelEnemyAmount { get { return _maxLevelEnemyAmount; } }
    public int DeadLevelEnemyAmount { get { return _deadLevelEnemyAmount; } }

    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private int _maxLevelEnemyAmount = 5;
    [SerializeField] private int _maxPosX = 9;
    [SerializeField] private int _minPosX = 9;
    [SerializeField] private int _maxPosY = 5;
    [SerializeField] private int _minPosY = 1;
    [SerializeField] private int _maxDelay = 5;
    [SerializeField] private int _minDelay = 1;
    [SerializeField] private Attacker[] _attackersArray;

    /// <summary>
    /// Private fields
    /// </summary>
    private bool _isSpawn = true;
    private int _spawnEnemyAmount = 0;
    private int _deadLevelEnemyAmount = 0;
    private Dictionary<int, int> _attackersLane = new Dictionary<int, int>();

    /// <summary>
    /// Actions on awake
    /// </summary>
    private void Awake()
    {
        // Add to registry
        Registry.AttackerSpawner = this;
    }

    /// <summary>
    /// Plus one dead enemy
    /// </summary>
    public void AddDeadEnemy()
    {
        Registry.DeadEnemyAmount++;
        _deadLevelEnemyAmount++;

        if (_maxLevelEnemyAmount <= _deadLevelEnemyAmount)
        {
            Registry.LevelController?.LevelFinished();
        }
    }

    /// <summary>
    /// Start spawn attackers coroutine at start
    /// </summary>
    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    /// <summary>
    /// Coroutine to spawn attackers while _isSpawn = true
    /// </summary>
    IEnumerator SpawnCoroutine()
    {
        while (_isSpawn)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_minDelay, _maxDelay)/10);

            if (_spawnEnemyAmount < _maxLevelEnemyAmount)
            {
                SpawnAttacker();
                _spawnEnemyAmount++;
            }
            else
            {
                SpawnStop();
            }
        }
    }

    /// <summary>
    /// Select a random attacker from array and call spawn
    /// </summary>
    private void SpawnAttacker()
    {
        if (_attackersArray.Length <=0)
        {
            Debug.Log("Error No Attacker Prefab.");
            return;
        }

        SpawnRandomAttacker(_attackersArray[UnityEngine.Random.Range(0, _attackersArray.Length)]);
    }

    /// <summary>
    /// Spawn a random attacker
    /// </summary>
    private void SpawnRandomAttacker(Attacker attackerPrefab)
    {
        // Instantiate an attacker
        int posX = UnityEngine.Random.Range(_minPosX, _maxPosX);
        int posY = UnityEngine.Random.Range(_minPosY, _maxPosY);
        Attacker tempAttacker = Instantiate(attackerPrefab, new Vector2(posX, posY), Quaternion.identity);

        // Parent attacker
        tempAttacker.transform.parent = Registry.GameObjectsGet("Attackers")?.transform;

        // Add to lane
        LaneAdd(posY);
    }

    /// <summary>
    /// Stop spawn
    /// </summary>
    public void SpawnStop()
    {
        _isSpawn = false;
    }

    /// <summary>
    /// Add to a dictionary of attackers in a lane
    /// </summary>
    private void LaneAdd(float y)
    {
        int intY = Convert.ToInt32(y);

        if (_attackersLane.ContainsKey(intY))
        {
            _attackersLane[intY]++;
        }
        else
        {
            _attackersLane[intY] = 1;
        }
    }

    /// <summary>
    /// Delete from a dictionary of attackers in a lane
    /// </summary>
    public void LaneDelete(float y)
    {
        int intY = Convert.ToInt32(y);

        if (_attackersLane.ContainsKey(intY) && 0 < _attackersLane[intY])
        {
            _attackersLane[intY]--;
        }
        else
        {
            _attackersLane[intY] = 0;
        }
    }

    /// <summary>
    /// Check attackers in a lane
    /// </summary>
    public bool LaneCheck(float y)
    {
        int intY = Convert.ToInt32(y);

        if (_attackersLane.ContainsKey(intY) && 0 < _attackersLane[intY])
        {
            return (0 < _attackersLane[intY]) ? true : false;
        }
        else
        {
            return false;
        }
    }
}
