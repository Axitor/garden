using UnityEngine;
using UnityEngine.UI;

public static class TextSystem
{
    /// <summary>
    /// Public properties
    /// </summary>
    public static Text WarningTextField { get { return _warningTextField; } set { _warningTextField = value; } }
    public static Text StarsTextField { get { return _starsTextField; } set { _starsTextField = value; } }
    public static Text LifeTextField { get { return _lifeTextField; } set { _lifeTextField = value; } }
    public static Text EnemyKilledTextField { get { return _enemyKilledTextField; } set { _enemyKilledTextField = value; } }
    public static Text EnemyKilledLevelTextField { get { return _enemyKilledLevelTextField; } set { _enemyKilledLevelTextField = value; } }
    public static Text MenuButtonLevelNumTextField { get { return _menuButtonLevelNumTextField; } set { _menuButtonLevelNumTextField = value; } }
    public static Text LevelTimerTextField { get { return _levelTimerTextField; } set { _levelTimerTextField = value; } }
    public static TextSystemHelper TextSystemHelper { get { return _textSystemHelper; } set { _textSystemHelper = value; } }

    /// <summary>
    /// Private fields
    /// </summary>
    private static Text _warningTextField;
    private static Text _starsTextField;
    private static Text _lifeTextField;
    private static Text _enemyKilledTextField;
    private static Text _enemyKilledLevelTextField;
    private static Text _menuButtonLevelNumTextField;
    private static Text _levelTimerTextField;
    private static TextSystemHelper _textSystemHelper;

    public static void UpdateWarningTextField(string value)
    {
        if (_warningTextField == null)
        {
            Debug.LogError("Error no warning text field prefab");
        }
        else
        {
            _warningTextField.text = value;
        }
    }

    public static void UpdateStarsTextField(string value)
    {
        if (_starsTextField == null)
        {
            Debug.LogError("Error no stars text field prefab");
        }
        else
        {
            _starsTextField.text = value;
        }
    }

    public static void UpdateLifeTextField(string value)
    {
        if (_lifeTextField == null)
        {
            Debug.LogError("Error no life text field prefab");
        }
        else
        {
            _lifeTextField.text = value;
        }
    }

    public static void UpdateEnemyKilledTextField(string value)
    {
        if (_enemyKilledTextField == null)
        {
            Debug.LogError("Error no enemies killed text field prefab");
        }
        else
        {
            _enemyKilledTextField.text = value;
        }
    }

    public static void UpdateEnemyKilledLevelTextField(string value)
    {
        if (_enemyKilledLevelTextField == null)
        {
            Debug.LogError("Error no enemies killed level text field prefab");
        }
        else
        {
            _enemyKilledLevelTextField.text = value;
        }
    }

    public static void UpdateButtonLevelNumTextField(string value)
    {
        if (_menuButtonLevelNumTextField == null)
        {
            Debug.LogError("Error no button level num text field prefab");
        }
        else
        {
            _menuButtonLevelNumTextField.text = value;
        }
    }

    public static void UpdateLevelTimerTextField(string value)
    {
        if (_levelTimerTextField == null)
        {
            Debug.LogError("Error no level timer text field prefab");
        }
        else
        {
            _levelTimerTextField.text = value;
        }
    }
}