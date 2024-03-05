using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week7;
using DG.Tweening;

namespace Week7
{
    public class EnemyBehavior : MonoBehaviour
    {
        //Properties
        public Material materialDamaged;
        public Material materialNormal;
        private MeshRenderer meshRenderer;



        //Methods
        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other) //Listening to triggers
        {
            if (other.gameObject.tag == "Bullet")
            {
                //Want the enemy to flash a certain color when triggered by gun bullet
                meshRenderer.material = materialDamaged;

                //Will delay a function for a certain amount of time (Granted by using DG.Tweening;)
                DOVirtual.DelayedCall(0.1f, () =>
                {
                    meshRenderer.material = materialNormal;
                });
            }
        }

        private void OnCollisionEnter(Collision collision) //Listening to collisions (Physics)
        {
            
        }

        private void OnTriggerStay(Collider other) //Listening to when a trigger stays triggered for a while
        {
            
        }

        private void OnTriggerExit(Collider other) //Listens to when you leave trigger
        {
            
        }

        public void Damage()
        {
            Debug.Log("The enemy has been damaged");
        }
    }
}

