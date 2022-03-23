using System.Collections.Generic;
using UnityEngine;

namespace Axitor.Utils
{
    public static class Registry
    {
        /// <summary>
        /// Public properties
        /// </summary>
        public static AttackerSpawner AttackerSpawner { get; set; }
        public static DefenderSpawner DefenderSpawner { get; set; }
        public static LevelController LevelController { get; set; }
        public static LevelTimer LevelTimer { get; set; }
        public static float TotalSeconds { get { return _totalSeconds; } set { _totalSeconds = value; } }
        public static int DeadEnemyAmount { get { return _deadEnemyAmount; } set { _deadEnemyAmount = value; } }

        /// <summary>
        /// Private fields
        /// </summary>
        private static Dictionary<string, GameObject> _gameObjects = new Dictionary<string, GameObject>();
        private static int _deadEnemyAmount = 0;
        private static float _totalSeconds = 0f;

        /// <summary>
        /// Add to a dictionary of game objects
        /// </summary>
        public static void GameObjectsAdd(string name, GameObject value)
        {
            _gameObjects[name] = value;
        }

        /// <summary>
        /// Get from a dictionary of game objects
        /// </summary>
        public static GameObject GameObjectsGet(string name)
        {
            try
            {
                return _gameObjects[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Clear dead enemy list
        /// </summary>
        public static void ClearDeadEnemy()
        {
            _deadEnemyAmount = 0;
        }

        /// <summary>
        /// Clear total game timer
        /// </summary>
        public static void ClearTimer()
        {
            _totalSeconds = 0f;
        }

    }
}
