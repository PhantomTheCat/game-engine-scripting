using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Let's us know that everything in the Week4 namespace is what we are using
using Week4;
using static UnityEngine.GraphicsBuffer;

//Putting the C# script in a namespace will make it more unique
namespace Week4
{
    public class Player : MonoBehaviour
    {

        //Properties
        [SerializeField] private int health = 10;
        public int attackDamage = 10;
        public int maxHealth = 10;


        /*
        [SerializeField] AudioClip attackSound;
        [SerializeField] AudioClip damageSound;
        private AudioSource audio;
        */


        /*public int health
        {
            //Will get the maxHealth as starting value
            get { return maxHealth; }
            //Will limit who can set the value of health, with this case only being player who can do it
            private set { maxHealth = value; }
        }*/



        //Methods
        private void Awake()
        {
            //audio = GetComponent<AudioSource>();
        }

        public void DamagePlayer(int amount)
        {
            health -= amount;
        }

        public Enemy FindNewTarget()
        {

            //Randomizes the enemy returned
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            int randomIndex = Random.Range(0, enemies.Length);
            return enemies[randomIndex];

            //Can also find objects via tags if they have one attached
            //GameObject.FindGameObjectsWithTag("Enemy");


            //Will return only the enemy at the top of the list
            //return GameObject.FindAnyObjectByType<Enemy>();


            ////Can also find the object and its related C# Script component
            //GameObject enemyObject = GameObject.Find("Enemy");
            //Enemy enemyComponent = enemyObject.GetComponent<Enemy>();
            //return enemyComponent;
        }

        [ContextMenu("Attack")]
        private void Attack()
        {
            Enemy target = FindNewTarget();
            target.DamageEnemy(attackDamage);


            //Plays our static method from the AudioManager script using the enum we set up there
            AudioManager.PlaySound(AudioManager.SoundType.ATTACK);


            //Plays it once
            //GetComponent<AudioSource>().PlayOneShot(attackSound);
        }

    }
}

