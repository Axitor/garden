using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Axitor.Utils;

public class DefenderSpawner : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private int _showButtonsForLevel = 3;
    [SerializeField] private int _maxFieldsX = 7;
    [SerializeField] private int _maxFieldsY = 5;
    [SerializeField] private int[] _costArray;
    [SerializeField] private string[] _toolTipArray;
    [SerializeField] private Defender[] _defendersArray;
    [SerializeField] private Button[] _buttonsArray;
    [SerializeField] private Text[] _textCostArray;

    /// <summary>
    /// Private fields
    /// </summary>
    private int _selectedDefender = 0;
    private float _alpha5 = 0.05f;
    private float _alpha50 = 0.5f;
    private float _alpha100 = 1f;
    private Vector2 _gridPos = new Vector2(0f, 0f);
    private int[,] _fieldsArray;

    /// <summary>
    /// Cache objects
    /// </summary>
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Actions on awake
    /// </summary>
    private void Awake()
    {
        // Check arrays
        int tmpLength = _defendersArray.Length;
        if (_buttonsArray.Length != tmpLength || _textCostArray.Length != tmpLength || _costArray.Length != tmpLength || _toolTipArray.Length != tmpLength)
        {
            Debug.LogError("Error wrong length of defenders arrays");
        }

        // Hide buttons that not belong to this level
        for (int i = 0; i < tmpLength; i++)
        {
            if (_showButtonsForLevel <= i)
            {
                _buttonsArray[i].gameObject.SetActive(false);
                _textCostArray[i].enabled = false;
            }
        }

        // Add to registry
        Registry.DefenderSpawner = this;

        // Cache objects
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Init an array of fields
        _fieldsArray = new int[_maxFieldsX, _maxFieldsY];

        for (int i = 0; i < _maxFieldsX; i++)
        {
            for (int i2 = 0; i2 < _maxFieldsY; i2++)
            {
                _fieldsArray[i,i2] = 0;
            }
        }

        // Show cost of defenders
        for (int i = 0; i < _costArray.Length; i++)
        {
            if(i != 0)
            {
                _textCostArray[i].text = _costArray[i].ToString();
            }
        }
    }

    /// <summary>
    /// Clear field data on defender death so a player can put another defender
    /// </summary>
    public void ClearField(Vector2 defenderPosition)
    {
        int x = Convert.ToInt32(defenderPosition.x) - 1;
        int y = Convert.ToInt32(defenderPosition.y) - 1;

        if (0 <= x && x < _maxFieldsX && 0 <= y && y < _maxFieldsY)
        {
            _fieldsArray[x, y] = 0;
        }
    }

    /// <summary>
    /// Actions on click
    /// </summary>
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        PointerGetGridPosition(pointerEventData.position);
        SpawnDefender();
    }

    /// <summary>
    /// Actions on pointer enter
    /// </summary>
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        SpriteChangeAlpha(_alpha5);
    }

    /// <summary>
    /// Actions on pointer exit
    /// </summary>
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        SpriteChangeAlpha(0);
    }

    /// <summary>
    /// Change alpha of a sprite
    /// </summary>
    private void SpriteChangeAlpha(float alpha)
    {
        if (CheckStatus(false))
        {
            Color tempColor = _spriteRenderer.color;
            tempColor.a = alpha;
            _spriteRenderer.color = tempColor;
        }
    }

    /// <summary>
    /// Set button color
    /// </summary>
    private void SetButtonColor(int i, float alpha)
    {
        ColorBlock tempColors = _buttonsArray[i].colors;
        Color tempColor = tempColors.normalColor;
        tempColor.a = alpha;
        tempColors.normalColor = tempColor;
        _buttonsArray[i].colors = tempColors;
    }

    /// <summary>
    /// Change all buttons color
    /// </summary>
    private void ChangeButtonsColor()
    {
        // clear all buttons
        for (int i = 0; i < _buttonsArray.Length; i++)
        {
            SetButtonColor(i, _alpha50);
        }

        // color selected button
        SetButtonColor(_selectedDefender, _alpha100);
    }

    /// <summary>
    /// Get world position of a click and snap to the grid
    /// </summary>
    private void PointerGetGridPosition(Vector2 pointerPosition)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(pointerPosition);
        _gridPos = new Vector2(Mathf.Round(worldPos.x), Mathf.Round(worldPos.y));
    }

    /// <summary>
    /// Spawn a defender
    /// </summary>
    private void SpawnDefender()
    {
        if (CheckStatus(true))
        {
            // place a defender
            Defender tempDefender = Instantiate(_defendersArray[_selectedDefender], _gridPos, Quaternion.identity);

            // place a health bar
            GameObject tempHealthBar = Instantiate(_healthBar, _gridPos, Quaternion.identity);

            // add a health bar to a parent defender
            tempHealthBar.transform.parent = tempDefender.transform;

            // add a defender to parent gameobject
            tempDefender.transform.parent = Registry.GameObjectsGet("Defenders")?.transform;

            // reserve a field
            _fieldsArray[Convert.ToInt32(_gridPos.x)-1, Convert.ToInt32(_gridPos.y)-1] = 1;

            // spend resources
            ResourceSystem.SetAmount(_costArray[_selectedDefender]);
        }
    }

    /// <summary>
    /// Check spawn status
    /// </summary>
    private bool CheckStatus(bool fullCheck = true)
    {
        // if empty defenders array
        if (_defendersArray.Length <= 0)
        {
            return false;
        }

        // if game paused
        else if (Time.timeScale == 0)
        {
            return false;
        }

        // if wrong index of defender
        else if(_selectedDefender <= 0 || _defendersArray.Length <= _selectedDefender)
        {
            return false;
        }

        // if field has been taken
        else if (fullCheck && _fieldsArray[Convert.ToInt32(_gridPos.x) - 1, Convert.ToInt32(_gridPos.y) - 1] == 1)
        {
            return false;
        }

        // if not enough resources
        else if (fullCheck && !ResourceSystem.CheckAmount(_costArray[_selectedDefender]))
        {
            return false;
        }

        // else ok
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Set selected defender on a button click
    /// </summary>
    public void SelectDefender(int i)
    {
        if (i < _defendersArray.Length)
        {
            _selectedDefender = i;
            ChangeButtonsColor();
            ShowToolTip(i);
        }
    }

    /// <summary>
    /// Show tooltip
    /// </summary>
    public void ShowToolTip(int i)
    {
        if (i < _toolTipArray.Length)
        {
            TextSystem.UpdateWarningTextField(_toolTipArray[i]);
        }
    }
}
