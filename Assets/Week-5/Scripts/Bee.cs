using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week5;

//Got this from the assignment description
using DG.Tweening;



namespace Week5
{
    public class Bee : MonoBehaviour
    {
        //Properties
        private BeeHive beeHive;
        [SerializeField] private float speed = 1f;
        private AudioSource audioSource;
        public AudioClip deliverySound;


        //Methods
        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            //Their first action when they are initialized
            CheckAnyFlower();
        }

        void Update()
        {

        }

        public void Init(BeeHive hive)
        {
            beeHive = hive;
        }

        private void CheckAnyFlower()
        {
            Flower randomFlower = GetRandomFlower();

            //Flies to the flower chosen
            transform.DOMove(randomFlower.transform.position, speed).OnComplete(() =>
            {
                if (randomFlower.GetNectar() == true)
                {
                    //Gives the nectar found back to the hive
                    GiveNectarToHive();
                }
                else
                {
                    //Resets the method by calling itself
                    //Searchs another flower till we get one with nectar
                    CheckAnyFlower();
                }

            }).SetEase(Ease.Linear);
        }

        private Flower GetRandomFlower()
        {
            //Gets a randomFlower for the bee to fly too
            Flower[] flowers = FindObjectsByType<Flower>(FindObjectsSortMode.None);
            int randomIndex = Random.Range(0, flowers.Length);
            Flower randomFlower = flowers[randomIndex];

            //Returns that flower
            return randomFlower;
        }

        private void GiveNectarToHive()
        {
            //Moves back to beehive, where it will give them the nectar
            transform.DOMove(beeHive.transform.position, speed).OnComplete(() =>
            {
                //Gives nectar to the hive
                beeHive.GiveNectar();

                //Playing the chime sound that the bee has
                //Indicator that they delivered the nectar
                audioSource.PlayOneShot(deliverySound);

                //Sets the bee out to check another flower
                CheckAnyFlower();

            }).SetEase(Ease.Linear);
        }
    }
}
