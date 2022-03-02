using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Axitor.Utils;

public class ResourceSystem : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private int _starsAmount = 100;
    [SerializeField] private int _starsAmountAddEvent = 1;
    [SerializeField] private string _warningText = "Not enough resources";
    [SerializeField] private Text _starsText;
    [SerializeField] private Text _buttonsText;

    /// <summary>
    /// Add to registry and update a text field
    /// </summary>
    private void Awake()
    {
        Registry.ResourceSystem = this;
        UpdateText();
    }

    /// <summary>
    /// Update text field
    /// </summary>
    public void UpdateText()
    {
        if (_starsText == null)
        {
            Debug.LogError("Error no stars text prefab");
        }

        _starsText.text = _starsAmount.ToString();
    }

    /// <summary>
    /// Returns remaining stars amount
    /// </summary>
    public int GetAmount()
    {
        return _starsAmount;
    }

    /// <summary>
    /// Check is enough stars
    /// </summary>
    public bool CheckAmount(int starsCost)
    {
        if(starsCost <= _starsAmount)
        {
            return true;
        }
        else
        {
            ShowWarning();
            return false;
        }
    }

    /// <summary>
    ///Add stars on event and return new value
    /// </summary>
    public int AddAmount()
    {
        return SetAmount(0, _starsAmountAddEvent);
    }

    /// <summary>
    /// Sets stars amount, updates the text field and returns new value
    /// </summary>
    public int SetAmount(int spend = 0, int add = 0)
    {
        _starsAmount += add;
        _starsAmount -= spend;

        if(_starsAmount < 0)
        {
            _starsAmount = 0;
        }

        UpdateText();

        return _starsAmount;
    }

    /// <summary>
    /// Show warning not enough resources
    /// </summary>
    public void ShowWarning()
    {
        if(_buttonsText == null)
        {
            Debug.LogError("Error no buttons text prefab");
        }

        _buttonsText.text = _warningText;
        StartCoroutine(ClearWarning());
    }

    /// <summary>
    /// Show tooltip
    /// </summary>
    public void ShowToolTip(string tollTip)
    {
        if (_buttonsText == null)
        {
            Debug.LogError("Error no buttons text prefab");
        }

        _buttonsText.text = tollTip;
        StartCoroutine(ClearWarning());
    }

    /// <summary>
    /// Coroutine to clear warnings
    /// </summary>
    IEnumerator ClearWarning()
    {
        yield return new WaitForSeconds(1);
        _buttonsText.text = "";
    }
}
