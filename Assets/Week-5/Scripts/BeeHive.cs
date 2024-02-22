using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Week5;

namespace Week5
{
    public class BeeHive : MonoBehaviour
    {
        //Properties
        [SerializeField] private TextMeshProUGUI honeyText;
        [SerializeField] private TextMeshProUGUI nectarText;
        [SerializeField] private TextMeshProUGUI beeText;
        public GameObject beePrefab;
        [SerializeField] private float nectarProductionRate = 5.0f;
        [SerializeField] private int numberOfBees = 2;
        public int nectarAmount = 0;
        public int honeyAmount = 0;
        private float timer;
        [SerializeField] private int beeCostUsingHoney = 5;



        //Methods
        void Start()
        {
            ResetTimer();
            SpawnStartingBees();
        }

        void Update()
        {
            CountDownTimer();
        }

        void CountDownTimer()
        {
            //If the BeeHive has some nectar
            if (nectarAmount > 0)
            {
                //Reduce the timer by a second
                timer -= Time.deltaTime;

                //If timer reaches or goes below 0
                if (timer <= 0)
                {
                    //Produces a honey
                    ProduceHoney();

                    //Resets the timer, but the timer will only count down when there is some nectar
                    ResetTimer();
                }

            }
        }

        public void GiveNectar()
        {
            //Used for when the bees give nectar to us

            //Increment our nectar amount
            nectarAmount++;
            UpdateAmountText();

            //No need to reset or continue the timer
            //as it constantly goes as long as we have nectar
        }

        void SpawnStartingBees()
        {
            //Instantiating the number of bees we need at the location of the Hive
            for (int i = 0; i < numberOfBees; i++)
            {
                SpawnBee();
            }
        }

        void ResetTimer()
        {
            //Resets the timer to be the production rate
            timer = nectarProductionRate;
        }

        void ProduceHoney()
        {
            //Produces a honey and removes a nectar
            honeyAmount++;
            nectarAmount--;
            UpdateAmountText();

            //For Extra Credit: Will Make another bee with honey available
            if (honeyAmount >= beeCostUsingHoney)
            {
                //Instantiates one bee for every honey that is accumalated
                SpawnBee();
                numberOfBees++;

                //Removes the honey that was used
                honeyAmount -= beeCostUsingHoney;
                UpdateAmountText();
            }
        }

        void SpawnBee()
        {
            //Instantiating a new beePrefab 
            GameObject beeObject = Instantiate(beePrefab, transform.position, beePrefab.transform.rotation);

            //Getting the new instantiated object's bee component
            Bee bee = beeObject.GetComponent<Bee>();

            //Using that component to say that this GameObject's beeHive is this instance of beeHive
            bee.Init(this);
        }

        void UpdateAmountText()
        {
            //Being added on by the other instances of this class that have different values
            honeyText.text = $"Honey Amount: {honeyAmount}";
            nectarText.text = $"Nectar Amount: {nectarAmount}";
            beeText.text = $"Bees = {numberOfBees}";
        }
    }
}

