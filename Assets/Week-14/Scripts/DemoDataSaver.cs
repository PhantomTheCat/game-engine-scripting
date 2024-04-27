using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Week12;


namespace Week14
{
    public class DemoDataSaver : MonoBehaviour
    {
        //
        //Properties
        //
        public SaveData saveData;

        //Can use constants for the string identifiers (Less repeating and easier to edit)
        public const string Money_ID = "Money";
        public const string Levels_Complete_ID = "Levels-Complete";
        public const string Name_ID = "Name";

        //Variables to be saved
        public int level;
        public string playerName;
        public float dollars
        {
            //Makes it into a middle-man between outside classes and the dollar variable

            //Anyone can get it
            get { return m_Dollars; }

            //Only this class can set it
            private set
            {
                m_Dollars = value;
                PlayerPrefs.SetFloat(Money_ID, dollars);
                Debug.Log(PlayerPrefs.GetFloat(Money_ID, 1.2f));
            }
        }
        private float m_Dollars;


        //
        //Methods
        //

        [ContextMenu("Save Data")]
        private void SaveData()
        {
            PlayerPrefs.SetInt(Levels_Complete_ID, 2);
            PlayerPrefs.SetString(Name_ID, playerName);
            PlayerPrefs.SetFloat(Money_ID, dollars);

            Debug.Log(JsonUtility.ToJson(saveData));
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(saveData));


            //Now adding text to save to a text file
            //Adds a single line of the save data
            string path = "Assets/Resources/Levels/Level_01.txt";
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(JsonUtility.ToJson(saveData));
            writer.Close();
            AssetDatabase.ImportAsset(path);


            PlayerPrefs.Save();
        }


        [ContextMenu("Load Data")]
        private void LoadData()
        {
            level = PlayerPrefs.GetInt(Levels_Complete_ID, 0);
            playerName = PlayerPrefs.GetString(Name_ID, "You have no name!");
            dollars = PlayerPrefs.GetFloat(Money_ID, 1.2f);

            //saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveData"));


            //Can load the text file into this variable
            TextAsset textAsset = Resources.Load("Levels/Level_01") as TextAsset;
            saveData = JsonUtility.FromJson<SaveData>(textAsset.text);

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
            //Resources.UnloadAsset(textAsset);
        }


        [ContextMenu("Add Dollar")]
        private void AddDollar()
        {
            dollars++;
        }
    }
}
