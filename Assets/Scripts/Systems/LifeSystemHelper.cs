using UnityEngine;
using Axitor.Utils;

public class LifeSystemHelper : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private int _lifeAmount = 1;
    [SerializeField] private int _lifeBuy = 1;

    /// <summary>
    /// Actions on start
    /// </summary>
    void Start()
    {
        // Init lives at main menu screen
        if (SceneLoader.CheckIsMainMenu())
        {
            LifeSystem.InitLives(_lifeAmount, _lifeBuy);
        }
    }
}
