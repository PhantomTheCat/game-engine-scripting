using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week5;


namespace Week5
{
    public class Flower : MonoBehaviour
    {
        //Properties
        [SerializeField] private float productionRate = 5.0f;
        private float nectarAmount = 0f;
        private float timer = 0f;
        public bool hasNectar = false;
        private SpriteRenderer spriteRenderer;
        private Color hasNectarColor = Color.white;
        private Color hasNoNectarColor = Color.grey;





        //Methods
        void Start()
        {
            //Connecting our sprite renderer to the engine
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = hasNoNectarColor;

            //Reseting our default value
            ResetNectarAmount();
        }

        void Update()
        {
            //Tying the timer to the time passed
            //Got help from a tutorial on Youtube on how to make a timer in Unity
            if (timer >= 0f)
            {
                //Each second that passes, the nectarAmount and timer will go up by one
                timer += Time.deltaTime;
                nectarAmount = timer;
            }

            //Checking for if we have enough nectar yet
            CheckForNectar();
        }

        private void CheckForNectar()
        {
            //Comparing our nectar amount to the amount we need for the bee to take
            if (nectarAmount >=  productionRate)
            {
                hasNectar = true;
                ChangeFlowerColor();
            }
        }

        public bool GetNectar()
        {
            //If flower has nectar
            if (hasNectar)
            {
                //Reset the nectar amount back to 0 and Change our color to match
                ResetNectarAmount();
                hasNectar = false;
                ChangeFlowerColor();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ChangeFlowerColor()
        {
            if (hasNectar == true)
            {
                //Changing the color to be white if they have nectar
                spriteRenderer.color = hasNectarColor;
            }
            else
            {
                //Changing the color to be grey
                spriteRenderer.color = hasNoNectarColor;
            }
        }

        private void ResetNectarAmount()
        {
            timer = 0f;
            nectarAmount = 0f;
        }
    }
}

