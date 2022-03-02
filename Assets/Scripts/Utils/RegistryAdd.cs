using UnityEngine;
using Axitor.Utils;

public class RegistryAdd : MonoBehaviour
{
    /// <summary>
    /// Add an object to the registry on awake
    /// </summary>
    private void Awake()
    {
        Registry.GameObjectsAdd(gameObject.name, gameObject);
    }
}
