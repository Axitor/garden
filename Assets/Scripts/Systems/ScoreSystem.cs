using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Axitor.Utils;

public static class ScoreSystem
{
    /// <summary>
    /// Public properties
    /// </summary>
    public static int MaxScoreLines { get { return _maxScoreLines; } }

    /// <summary>
    /// Save object class
    /// </summary>
    public class SaveObject
    {
        public int LevelNumber;
        public int TotalTime;
        public int DeadEnemies;
    }

    /// <summary>
    /// Private fields
    /// </summary>
    private static string _saveFile = Application.persistentDataPath + "/save.txt";
    private static string _cryptoPass = "ALIEjfh9834ffas";
    private static List<SaveObject> _scoreList = new List<SaveObject>();
    private static int _maxScoreLines = 5;
    private static float _timeBetweenSave = 10f;
    private static DateTime _timeLastSave;

    /// <summary>
    /// Save score
    /// </summary>
    public static void Save()
    {
        if (0 < Registry.DeadEnemyAmount && (_timeLastSave == null || DateTime.Compare(_timeLastSave.AddSeconds(_timeBetweenSave), DateTime.Now) <= 0))
        {
            _timeLastSave = DateTime.Now;
            _scoreList = GetScore();

            AddScoreNew();
            SortAndCut();
            SetScore();
        }
    }

    /// <summary>
    /// Set score
    /// </summary>
    private static void SetScore()
    {
        List<string> lines = new List<string>();

        for (int i = 0; i < _scoreList.Count; i++)
        {
            string json = JsonUtility.ToJson(_scoreList[i]);
            string crypto = Crypto.Encrypt(json, _cryptoPass);
            lines.Add(crypto);
        }

        Files.WriteLines(_saveFile, lines);

        _scoreList = new List<SaveObject>();
    }

    /// <summary>
    /// Get score - SaveObject.LevelNumber, SaveObject.TotalTime, SaveObject.DeadEnemies
    /// </summary>
    public static List<SaveObject> GetScore()
    {
        List<SaveObject> scoreList = new List<SaveObject>();
        List<string> lines = Files.ReadLines(_saveFile, true);

        for (int i = 0; i < lines.Count; i++)
        {
            try
            {
                string decrypt = Crypto.Decrypt(lines[i], _cryptoPass);
                SaveObject line = JsonUtility.FromJson<SaveObject>(decrypt);

                if (line != null)
                {
                    scoreList.Add(line);
                }
            }
            catch(Exception e)
            {
                Debug.LogError("Error reading line " + i + " in GetScore. Message: " + e.Message);
            }
        }

        return scoreList;
    }

    /// <summary>
    /// Add new score
    /// </summary>
    private static void AddScoreNew()
    {
        _scoreList.Add
            (
                new SaveObject()
                {
                    LevelNumber = SceneLoader.GetLevelNum(),
                    TotalTime = Convert.ToInt32(Registry.TotalSeconds),
                    DeadEnemies = Registry.DeadEnemyAmount
                }
            );
    }

    /// <summary>
    /// Sort and cut the list
    /// </summary>
    private static void SortAndCut()
    {
        _scoreList = (from x in _scoreList orderby x.LevelNumber descending, x.DeadEnemies descending, x.TotalTime select x).ToList();

        if (_maxScoreLines < _scoreList.Count)
        {
            _scoreList.RemoveRange(_maxScoreLines, _scoreList.Count - _maxScoreLines);
        }
    }
}
