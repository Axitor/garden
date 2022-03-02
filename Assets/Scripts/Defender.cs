using UnityEngine;
using Axitor.Utils;

public class Defender : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private float _weaponOffsetX = 0.5f;
    [SerializeField] private float _weaponOffsetY = 0f;
    [SerializeField] private GameObject _defenderWeapon;

    /// <summary>
    /// Start defender shooting on animation event
    /// </summary>
    public void ShootStart()
    {
        // Check defender weapon prefab
        if (_defenderWeapon == null)
        {
            Debug.Log("Error No Cactus Defender Weapon Prefab.");
            return;
        }

        // Check attackers in the lane and instantiate a weapon
        if (Registry.AttackerSpawner.LaneCheck(transform.position.y))
        {
            GameObject tempWeapon = Instantiate(_defenderWeapon, transform.position + new Vector3(_weaponOffsetX, _weaponOffsetY), Quaternion.identity);
            tempWeapon.transform.parent = Registry.GameObjectsGet("Defenders")?.transform;
        }
    }
}
