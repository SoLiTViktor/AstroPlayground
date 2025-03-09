using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace AsteroidProject
{
    public class GameCoreFileReader : MonoBehaviour
    {
        [SerializeField] private GameCoreData _gameCoreData;

        [Space(15)] 
        [SerializeField] private TextAsset _configFile;

        private string _hexCodeRed = "#FF0000";
        private string _hexCodeGreen = "#00FF00";


        [ContextMenu("Load")]
        public GameCoreData LoadFromFile()
        {
            if (_configFile == null)
            {
                Debug.Log($"[GameCore] - <color={_hexCodeRed}>Error</color> - LoadFromFile -> File not found");
                return null;
            }

            try
            {
                string json = _configFile.ToString();

                GameCoreData gameCoreFromJson = JsonConvert.DeserializeObject<GameCoreData>(json);

                Debug.Log($"[GameCore] - <color={_hexCodeGreen}>Success</color> - LoadFromFile -> The data from the file has been successfully loaded");

                return gameCoreFromJson;
            }
            catch (Exception exception)
            {
                Debug.Log($"[GameCore] - <color={_hexCodeRed}>Error</color> - LoadFromFile -> {exception.Message}");
                return null;
            }
        }

        [ContextMenu("Save")]
        private void SaveToFile()
        {
            string json = JsonConvert.SerializeObject(_gameCoreData, Formatting.Indented);

            try
            {
                _configFile = new TextAsset(json);
                Debug.Log($"[GameCore] - <color={_hexCodeGreen}>Success</color> - SaveToFile -> The data in the file has been successfully updated.");
            }
            catch (Exception exception)
            {
                Debug.Log($"[GameCore] - <color={_hexCodeRed}>Error</color> - SaveToFile -> {exception.Message}");
            }
        }
    }
}
