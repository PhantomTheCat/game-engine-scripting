using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    //Properties
    [SerializeField] private GameObject door;
    private Vector3 origin;
    private Vector3 target;
    private bool isOpening;
    private float alpha;

    //Method
    private void Awake()
    {
        origin = transform.position;
        target = origin + (Vector3.up * 5);
    }

    private void Update()
    {
        alpha += isOpening ? Time.deltaTime : -Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        //Lerp is a function that acts as translate to go from point a to b in amount of time
        door.transform.position = Vector3.Lerp(origin, target, alpha);
    }

    private void OnTriggerEnter(Collider other)
    {
        isOpening = true;
        //door.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        isOpening = false;
        //door.gameObject.SetActive(true);
    }
}
