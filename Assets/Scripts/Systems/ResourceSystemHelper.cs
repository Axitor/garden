using UnityEngine;
using Axitor.Utils;

public class ResourceSystemHelper : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private int _starsAmount = 100;
    [SerializeField] private int _starsAmountAddEvent = 1;
    [SerializeField] private string _starsWarningMessage = "Not enough resources";

    /// <summary>
    /// Assign values on awake
    /// </summary>
    private void Awake()
    {
        ResourceSystem.StarsAmount = _starsAmount;
        ResourceSystem.StarsAmountAddEvent = _starsAmountAddEvent;
        ResourceSystem.StarsWarningMessage = _starsWarningMessage;
    }
}
