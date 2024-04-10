using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for loading and saving the data

namespace Week12
{
    public class DataSaver : MonoBehaviour
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
            //To save data, have to set the data you want to be saved
            //First parameter = string identifier
            //Second parameter = value to store
            PlayerPrefs.SetInt(Levels_Complete_ID, 2);
            PlayerPrefs.SetString(Name_ID, playerName);
            PlayerPrefs.SetFloat(Money_ID, dollars);


            Debug.Log(JsonUtility.ToJson(saveData));
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(saveData));

            //Save saves everything in the scene (EVERYTHING!!)
            //Costs a lot of space
            PlayerPrefs.Save();
        }


        [ContextMenu("Load Data")]
        private void LoadData()
        {
            //To load data from the PlayerPref, have to get it via the string it's attached to
            //The second parameter stands for the default value if we dont get anything
            //First parameter = string identifier
            //Second parameter = default value
            level = PlayerPrefs.GetInt(Levels_Complete_ID, 0);
            playerName = PlayerPrefs.GetString(Name_ID, "You have no name!");
            dollars = PlayerPrefs.GetFloat(Money_ID, 1.2f);

            saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveData"));
        }


        [ContextMenu("Add Dollar")]
        private void AddDollar()
        {
            dollars++;
        }

    }
}
