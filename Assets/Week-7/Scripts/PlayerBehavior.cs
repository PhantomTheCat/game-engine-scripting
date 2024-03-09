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
        public int health = 100;
        public int maxHealth = 100;
        public int numberOfKeys = 0;
        public int coinCount = 0;


        //Methods
        private void Update()
        {
            if (CheckIfDead())
            {
                //If player is dead, end the game (In our case, display the death message)
                loserText.gameObject.SetActive(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Key")
            {
                Destroy(other.gameObject);
                numberOfKeys++;
            }
            if (other.gameObject.tag == "Coin")
            {
                Destroy(other.gameObject);
                coinCount++;
            }
        }

        public void DamagePlayer(int amountDamaged)
        {
            damagedText.gameObject.SetActive(true);
            health -= amountDamaged;

            //Will turn it off after a few seconds
            Invoke("TurnOffText", 2f);
        }

        private void TurnOffText()
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
    }
}
