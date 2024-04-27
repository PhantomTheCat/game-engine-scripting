using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelEditorDataSaver : MonoBehaviour
    {
        //Properties
        public LevelData levelSaveData;
        [SerializeField] private TMP_InputField levelNumberField;
        [SerializeField] private TMP_InputField levelNameField;
        [SerializeField] private TMP_InputField rewardAmountField;
        [SerializeField] private TMP_InputField enemyAmountField;

        //
        //Methods
        //

        [ContextMenu("Save Data")]
        public void SaveData()
        {
            try
            {
                //Getting the info the player placed into it
                levelSaveData.LevelNumber = Int32.Parse(levelNumberField.text);
                levelSaveData.LevelName = levelNameField.text;
                levelSaveData.EnemyAmount = Int32.Parse(enemyAmountField.text);
                levelSaveData.RewardAmount = Int32.Parse(rewardAmountField.text);
            }
            catch
            {
                Debug.Log(levelSaveData.LevelNumber);
                Debug.Log(levelSaveData.LevelName);
                Debug.Log(levelSaveData.EnemyAmount);
                Debug.Log(levelSaveData.RewardAmount);

                Debug.LogError("They entered in invalid data...");
                return;
            }

            //Now adding text to save to a text file
            //Adds a single line of the save data
            string path = $"Assets/Resources/Levels/Level_0{levelSaveData.LevelNumber}.txt";
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(JsonUtility.ToJson(levelSaveData));
            writer.Close();
            AssetDatabase.ImportAsset(path);
        }


        [ContextMenu("Load Data")]
        public void LoadData()
        {
            //Can load the text file into this variable
            TextAsset textAsset = Resources.Load("Levels/Level_01") as TextAsset;
            levelSaveData = JsonUtility.FromJson<LevelData>(textAsset.text);

            TextAsset textAsset2 = Resources.Load("") as TextAsset;
            string contents = textAsset2.text;
            string[] lines = contents.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split('\n');

                for (int j = 0; j < columns.Length; j++)
                {

                    //string column = columns[j];
                }
            }

            //Unloads the asset for the next one!
            Resources.UnloadAsset(textAsset);
        }

    }
}
