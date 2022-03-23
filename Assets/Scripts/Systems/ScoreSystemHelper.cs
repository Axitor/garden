using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Axitor.Utils;

public class ScoreSystemHelper : MonoBehaviour
{
    /// <summary>
    /// Editable fields
    /// </summary>
    [SerializeField] private Text[] _textNum;
    [SerializeField] private Text[] _textLevel;
    [SerializeField] private Text[] _textTime;
    [SerializeField] private Text[] _textEnemy;

    /// <summary>
    /// Actions on start
    /// </summary>
    private void Start()
    {
        // Clear score at main menu screen
        if (SceneLoader.CheckIsMainMenu())
        {
            Registry.ClearDeadEnemy();
            Registry.ClearTimer();
        }

        // Show score at score screen
        else if (SceneLoader.CheckIsScore())
        {
            List<ScoreSystem.SaveObject> scoreList = ScoreSystem.GetScore();

            for (int i = 0; i < scoreList.Count; i++)
            {
                _textLevel[i].text = scoreList[i].LevelNumber.ToString();
                _textTime[i].text = scoreList[i].TotalTime.ToString();
                _textEnemy[i].text = scoreList[i].DeadEnemies.ToString();
            }

            for (int i = 0; i < (ScoreSystem.MaxScoreLines - scoreList.Count); i++)
            {
                int num = scoreList.Count + i;
                _textNum[num].enabled = false;
                _textLevel[num].enabled = false;
                _textTime[num].enabled = false;
                _textEnemy[num].enabled = false;
            }
        }
    }

}
