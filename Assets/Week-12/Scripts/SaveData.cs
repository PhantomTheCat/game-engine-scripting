using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to structure our saved data into a form that can be read

namespace Week12
{
    [System.Serializable] //Be able to view all the data in this class in the inspector
    public class SaveData
    {
        //Properties
        public int LevelsComplete;
        public string Name;
        public float Money;
    }
}
