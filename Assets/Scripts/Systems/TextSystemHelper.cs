using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Axitor.Utils;

public class TextSystemHelper : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private Text _warningTextField;
    [SerializeField] private Text _starsTextField;
    [SerializeField] private Text _lifeTextField;
    [SerializeField] private Text _enemyKilledTextField;
    [SerializeField] private Text _enemyKilledLevelTextField;
    [SerializeField] private Text _menuButtonLevelNumTextField;
    [SerializeField] private Text _levelTimerTextField;

    /// <summary>
    /// Assign values on awake
    /// </summary>
    private void Awake()
    {
        TextSystem.TextSystemHelper = this;
        TextSystem.WarningTextField = _warningTextField;
        TextSystem.StarsTextField = _starsTextField;
        TextSystem.LifeTextField = _lifeTextField;
        TextSystem.EnemyKilledTextField = _enemyKilledTextField;
        TextSystem.EnemyKilledLevelTextField = _enemyKilledLevelTextField;
        TextSystem.MenuButtonLevelNumTextField = _menuButtonLevelNumTextField;
        TextSystem.LevelTimerTextField = _levelTimerTextField;
    }

    /// <summary>
    /// Update text fields on start
    /// </summary>
    private void Start()
    {
        // Clear warnings
        TextSystem.UpdateWarningTextField("");

        // Show stars amount
        TextSystem.UpdateStarsTextField(ResourceSystem.StarsAmount.ToString());
        
        // Show lives amount
        TextSystem.UpdateLifeTextField(LifeSystem.LivesAmount.ToString());

        // Show killed enemies amount
        TextSystem.UpdateEnemyKilledTextField(Registry.DeadEnemyAmount.ToString());

        // Show killed enemies amount at current level 
        TextSystem.UpdateEnemyKilledLevelTextField(Registry.AttackerSpawner?.DeadLevelEnemyAmount.ToString() + "/" + Registry.AttackerSpawner?.MaxLevelEnemyAmount.ToString());

        // Show level num
        TextSystem.UpdateButtonLevelNumTextField("Level " + SceneLoader.GetLevelNum().ToString());
    }

    /// <summary>
    /// Clear warnings
    /// </summary>
    public void ClearWarning()
    {
        StartCoroutine(ClearWarningCoroutine());
    }

    /// <summary>
    /// Coroutine to clear warnings
    /// </summary>
    IEnumerator ClearWarningCoroutine()
    {
        yield return new WaitForSeconds(1);
        TextSystem.UpdateWarningTextField("");
    }
}
