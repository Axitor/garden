using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Axitor.Utils;

public class SceneLoaderHelper : MonoBehaviour
{
    /// <summary>
    /// Start coroutine to load start screen
    /// </summary>
    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(WaitLoadStart());
        }
    }

    /// <summary>
    /// Coroutine to wait before loading start screen
    /// </summary>

    IEnumerator WaitLoadStart()
    {
        yield return new WaitForSeconds(3);
        SceneLoader.LoadMain();
    }

    /// <summary>
    /// Calls loader to load main menu
    /// </summary>
    public void LoadMain()
    {
        SceneLoader.LoadMain();
    }

    /// <summary>
    /// Calls loader to load first level
    /// </summary>
    public void LoadFirst()
    {
        SceneLoader.LoadFirst();
    }

    /// <summary>
    /// Calls loader to load options
    /// </summary>
    public void LoadOptions()
    {
        SceneLoader.LoadOptions();
    }

    /// <summary>
    /// Calls loader to load score
    /// </summary>
    public void LoadScore()
    {
        SceneLoader.LoadScore();
    }

    /// <summary>
    /// Application quit
    /// </summary>
    public void Quit()
    {
        SceneLoader.Quit();
    }
}
