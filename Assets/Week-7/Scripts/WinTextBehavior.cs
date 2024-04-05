using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MazeGame
{
    public class WinTextBehavior : MonoBehaviour
    {
        //Properties
        [SerializeField] private GameObject winText;
        [SerializeField] private GameObject loserText;
        [SerializeField] private GameObject resetText;
        private bool hasEnteredWinArea = false;

        //Methods
        private void Awake()
        {
            MazeGameManager.resetGameEvent += ResetWinText;
        }

        private void Update()
        {
            //If they have won and pressed the button
            if (hasEnteredWinArea && Input.GetKey(KeyCode.E))
            {
                //Triggering the event
                MazeGameManager.resetGameEvent();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //If player enters this area, trigger the win text to pop up
            if (other.gameObject.tag == "Player")
            {
                hasEnteredWinArea = true;
                winText.gameObject.SetActive(true);
                resetText.gameObject.SetActive(true);
            }
        }

        private void ResetWinText()
        {
            winText.gameObject.SetActive(false);
            loserText.gameObject.SetActive(false);
            resetText.gameObject.SetActive(false);
            hasEnteredWinArea = false;
        }
    }
}

