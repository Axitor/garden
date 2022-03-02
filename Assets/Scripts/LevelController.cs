using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Axitor.Utils;

public class LevelController : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private Text _lifeText;
    [SerializeField] private Text _enemyText;
    [SerializeField] private Text _enemyLevelText;
    [SerializeField] private Text _menuButtonText;
    [SerializeField] private Canvas _gameMenu;
    [SerializeField] private Canvas _canvasWin;
    [SerializeField] private Canvas _canvasFail;
    [SerializeField] private Canvas _canvasBuy;
    [SerializeField] private Canvas _canvasNextLife;
    [SerializeField] private Canvas _canvasHowToPlay;

    /// <summary>
    /// Actions on awake
    /// </summary>
    private void Awake()
    {
        // Add to registry
        Registry.LevelController = this;

        // Hide game menu & canvas
        GameMenuHideAllMesssages();

        // Show level num
        _menuButtonText.text = "Level " + SceneLoader.GetLevelNum().ToString();
    }

    /// <summary>
    /// Actions on start
    /// </summary>
    private void Start()
    {
        // Update texts
        UpdateText();

        // Show how to play for the first level
        if (SceneLoader.GetLevelNum() == 1)
        {
            HowToPlayShow();
        }
    }

    /// <summary>
    /// Update text
    /// </summary>
    public void UpdateText()
    {
        _lifeText.text = LifeSystem.LivesAmount.ToString();
        _enemyText.text = Registry.DeadEnemyAmount.ToString();
        _enemyLevelText.text = Registry.AttackerSpawner?.DeadLevelEnemyAmount.ToString() + "/" + Registry.AttackerSpawner?.MaxLevelEnemyAmount.ToString();
    }

    /// <summary>
    /// Hide all messages
    /// </summary>
    public void GameMenuHideAllMesssages()
    {
        _gameMenu.enabled = false;
        _canvasWin.enabled = false;
        _canvasFail.enabled = false;
        _canvasBuy.enabled = false;
        _canvasNextLife.enabled = false;
        _canvasHowToPlay.enabled = false;
    }

    /// <summary>
    /// Finish level
    /// </summary>
    public void LevelFinished(bool isFail = false)
    {
        // Update life amount & text if fail level
        if (isFail)
        {
            // Update lives
            LifeSystem.DeleteLife();
            UpdateText();

            // Check lives & show a message
            if (LifeSystem.LivesAmount <= 0)
            {
                _canvasFail.enabled = true;
            }
            else
            {
                _canvasNextLife.enabled = true;
            }
        }
        else
        {
            // Show a message
            _canvasWin.enabled = true;
        }

        //Pause the game
        Time.timeScale = 0;
    }

    /// <summary>
    /// Show menu & pause game
    /// </summary>
    public void MenuShow()
    {
        _gameMenu.enabled = true;
        Time.timeScale = 0;
    }

    /// <summary>
    /// Hide menu & resume game
    /// </summary>
    public void MenuResume()
    {
        GameMenuHideAllMesssages();
        Time.timeScale = 1;
    }

    /// <summary>
    /// Go to main menu
    /// </summary>
    public void MenuMain()
    {
        _gameMenu.enabled = false;
        Time.timeScale = 1;
        SceneLoader.LoadMain();
    }

    /// <summary>
    /// Go to next level
    /// </summary>
    public void MenuNext()
    {
        Time.timeScale = 1;
        SceneLoader.LoadNext();
    }

    /// <summary>
    /// Show how to play
    /// </summary>
    public void HowToPlayShow()
    {
        Time.timeScale = 0;
        _gameMenu.enabled = false;
        _canvasHowToPlay.enabled = true;
    }

    /// <summary>
    /// Hide how to play
    /// </summary>
    public void HowToPlayHide()
    {
        Time.timeScale = 1;
        _canvasHowToPlay.enabled = false;
    }

    /// <summary>
    /// Get more lives
    /// </summary>
    public void MenuGetLives()
    {
        // Update life amount & text
        LifeSystem.AddLivesBuyAmount();
        UpdateText();

        // Show a message
        _canvasFail.enabled = false;
        _canvasBuy.enabled = true;
    }

    /// <summary>
    /// Exit game
    /// </summary>
    public void MenuExit()
    {
        Quit();
    }

    /// <summary>
    /// Application quit
    /// </summary>
    public void Quit()
    {
        SceneLoader.Quit();
    }

}
