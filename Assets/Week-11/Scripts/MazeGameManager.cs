using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MazeGame
{
    public class MazeGameManager : MonoBehaviour
    {
        //Properties
        public static UnityAction resetGameEvent;
        private GameObject[] coins;
        private GameObject[] keys;

        //Methods
        private void Awake()
        {
            //Finding all the objects we need in the scene using tags
            coins = GameObject.FindGameObjectsWithTag("Coin");
            keys = GameObject.FindGameObjectsWithTag("Key");

            //Subscring methods in our game manager to our end game event
            resetGameEvent += ResetAllCoins;
            resetGameEvent += ResetAllKeys;
        }

        private void ResetAllCoins()
        {
            foreach (GameObject coin in coins)
            {
                gameObject.SetActive(true);
            }
        }

        private void ResetAllKeys()
        {
            foreach (GameObject key in keys)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
