using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Axitor.Utils
{
    public static class SceneLoader
    {
        /// <summary>
        /// Dummy MonoBehaviour class for coroutines
        /// </summary>
        private class DummyLoadingClass : MonoBehaviour { }

        /// <summary>
        /// Private fields
        /// </summary>
        private static AsyncOperation _loadingOperation;

        /// <summary>
        /// Delegates
        /// </summary>
        private static Action onLoadCallback;

        /// <summary>
        /// Scenes
        /// </summary>
        private static List<string> _scenes = new List<string>()
        {
            "ScreenSplash",
            "ScreenStart",
            "ScreenOptions",
            "ScreenScore",
            "ScreenFinish",
            "Loading",
            "Level_01",
            "Level_02",
            "Level_03"
        };

        /// <summary>
        /// Loads a scene async with a progress bar. Goes to a loading scene and starts a coroutine to load a target scene async on callback from a loading scene.
        /// </summary> 
        public static void LoadAsync(int num)
        {
            onLoadCallback = () =>
            {
                GameObject dummyLoadingObject = new GameObject("DummyLoadingObject");
                dummyLoadingObject.AddComponent<DummyLoadingClass>().StartCoroutine(LoadAsyncCoroutine(num));
                _loadingOperation = null;
            };

            SceneManager.LoadScene(_scenes[5]);
        }

        /// <summary>
        /// Coroutine to load a scene async with a progress
        /// </summary>
        private static IEnumerator LoadAsyncCoroutine(int num)
        {
            yield return null;

            num = (_scenes.Count <= num) ? 1 : num;

            _loadingOperation = SceneManager.LoadSceneAsync(_scenes[num]);

            while (!_loadingOperation.isDone)
            {
                yield return null;
            }
        }

        /// <summary>
        /// Load a scene directly without a progress bar
        /// </summary>
        public static void Load(int num)
        {
            num = (_scenes.Count <= num) ? 1 : num;
            SceneManager.LoadScene(_scenes[num]);
        }

        /// <summary>
        /// Callback from a loading scene
        /// </summary>
        public static void LoadCallback()
        {
            if (onLoadCallback != null)
            {
                onLoadCallback();
                onLoadCallback = null;
            }
        }

        /// <summary>
        /// Get loading process
        /// </summary>
        public static float LoadingGetProgress()
        {
            if (_loadingOperation != null)
            {
                return _loadingOperation.progress;
            }
            else
            {
                return 0f;
            }
        }

        /// <summary>
        /// Loads main menu
        /// </summary>
        public static void LoadMain()
        {
            // Save score
            ScoreSystem.Save();

            // Load menu
            Load(1);
        }

        /// <summary>
        /// Loads options screen 
        /// </summary>
        public static void LoadOptions()
        {
            LoadAsync(2);
        }

        /// <summary>
        /// Loads score screen 
        /// </summary>
        public static void LoadScore()
        {
            LoadAsync(3);
        }

        /// <summary>
        /// Loads finish screen 
        /// </summary>
        public static void LoadFinish()
        {
            LoadAsync(4);
        }

        /// <summary>
        /// Loads first level
        /// </summary>
        public static void LoadFirst()
        {
            LoadAsync(6);
        }

        /// <summary>
        /// Loads next level
        /// </summary>
        public static void LoadNext()
        {
            // Save score
            ScoreSystem.Save();

            // Get next level num
            int numNext = SceneManager.GetActiveScene().buildIndex + 1;

            // Check is the last level
            if (_scenes.Count <= numNext) 
            {
                // Select finish screen num
                numNext = 4;
            }

            // Load next scene
            LoadAsync(numNext);
        }

        /// <summary>
        /// Get level num
        /// </summary>
        public static int GetLevelNum()
        {
            return SceneManager.GetActiveScene().buildIndex - 5;
        }

        /// <summary>
        /// Check is splash screen
        /// </summary>
        public static bool CheckIsIntro()
        {
            return (SceneManager.GetActiveScene().buildIndex == 0);
        }

        /// <summary>
        /// Check is start screen
        /// </summary>
        public static bool CheckIsMainMenu()
        {
            return (SceneManager.GetActiveScene().buildIndex == 1);
        }

        /// <summary>
        /// Check is score screen
        /// </summary>
        public static bool CheckIsScore()
        {
            return (SceneManager.GetActiveScene().buildIndex == 3);
        }

        /// <summary>
        /// Check is level
        /// </summary>
        public static bool CheckIsLevel()
        {
            return (5 < SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Application quit
        /// </summary>
        public static void Quit()
        {
            // Save score
            ScoreSystem.Save();

            // Quit
            Application.Quit();
        }
    }
}
