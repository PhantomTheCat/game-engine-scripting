using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

//Got to first put the scenes you want to move to in the build settings in inspector

namespace Week12
{
    public class LoadLevel : MonoBehaviour
    {
        //Properties
        [SerializeField] private string LevelToLoad;

        //Methods
        private void Awake()
        {
            SceneManager.LoadScene(LevelToLoad);
        }
    }
}

