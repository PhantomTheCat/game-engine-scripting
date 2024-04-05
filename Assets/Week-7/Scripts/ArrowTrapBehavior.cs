using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGame
{
    public class ArrowTrapBehavior : MonoBehaviour
    {
        //Properties
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform arrowSpawnTransform;


        //Methods
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Instantiate(arrowPrefab, arrowSpawnTransform.position, this.transform.rotation);
            }
        }
    }
}

