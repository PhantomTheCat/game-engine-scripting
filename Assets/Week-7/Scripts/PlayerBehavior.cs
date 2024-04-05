using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MazeGame
{
    public class PlayerBehavior : MonoBehaviour
    {
        //Properties
        [SerializeField] private TextMeshProUGUI damagedText;
        [SerializeField] private TextMeshProUGUI loserText;
        [SerializeField] private GameObject resetText;
        private Vector3 startingPosition;
        public int health = 100;
        private int maxHealth = 100;
        public int numberOfKeys = 0;
        public int coinCount = 0;


        //Methods
        private void Awake()
        {
            startingPosition = transform.position;
            MazeGameManager.resetGameEvent += ResetPlayer;
        }

        private void Update()
        {
            if (CheckIfDead())
            {
                //If player is dead, end the game (In our case, display the death message)
                loserText.gameObject.SetActive(true);
                resetText.gameObject.SetActive(true);


                //Resetting the game if they choose
                if (Input.GetKey(KeyCode.E))
                {
                    MazeGameManager.resetGameEvent();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Key")
            {
                other.gameObject.SetActive(false);
                numberOfKeys++;
            }
            if (other.gameObject.tag == "Coin")
            {
                other.gameObject.SetActive(false);
                coinCount++;
            }
        }

        public void DamagePlayer(int amountDamaged)
        {
            damagedText.gameObject.SetActive(true);
            health -= amountDamaged;

            //Will turn it off after a few seconds
            Invoke("TurnOffDamageText", 2f);
        }

        private void TurnOffDamageText()
        {
            damagedText.gameObject.SetActive(false);
        }

        private bool CheckIfDead()
        {
            if (health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ResetPlayer()
        {
            //Reseting defaults to player
            health = maxHealth;
            numberOfKeys = 0;
            coinCount = 0;
            transform.position = startingPosition;
        }
    }
}
