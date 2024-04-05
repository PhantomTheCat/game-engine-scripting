using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week11;

//Need this for using events
using UnityEngine.Events;


namespace Week11
{
    public class GameManager : MonoBehaviour
    {
        //Properties
        public UnityEvent GameOverEvent;
        private static GameManager instance;
        //public GameObject GameOverText;


        //Methods
        private void Awake()
        {
            instance = this;
        }

        [ContextMenu("Do Test GameOverEvent")] //1st type of use (Using events)
        public void TestGameOverEvent()
        {
            GameOverEvent.Invoke();
        }

        /*
        [ContextMenu("Do Test GameOver")] //2nd type of use (Using hard-coded values)
        private void TestGameOver()
        {
            GameOverText.SetActive(true);
        }
        */

        public static UnityEvent GetGameOverEvent()
        {
            return instance.GameOverEvent;
        }


    }
}
