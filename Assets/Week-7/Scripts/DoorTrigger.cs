using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeGame;
using TMPro;
using UnityEngine.InputSystem;

public class DoorTrigger : MonoBehaviour
{
    //Properties
    [SerializeField] private GameObject doorPivotPoint;
    [SerializeField] private TextMeshPro doorText;
    [SerializeField] private float targetDistance = 5.0f;
    [SerializeField] private bool isUnlocked = false;
    private Vector3 origin;
    private Vector3 target;
    private bool isOpening;
    private float alpha;
    private PlayerBehavior player;

    //Method
    private void Awake()
    {
        origin = transform.position;
        target = origin + (Vector3.up * targetDistance);
    }

    private void Update()
    {
        alpha += isOpening ? Time.deltaTime : -Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        //Lerp is a function that acts as translate to go from point a to b in amount of time
        doorPivotPoint.transform.position = Vector3.Lerp(origin, target, alpha);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.GetComponent<PlayerBehavior>();
            if (Input.GetKey(KeyCode.E) && player.numberOfKeys > 0 && isUnlocked == false)
            {
                player.numberOfKeys--;
                doorText.text = "Unlocked";
                isUnlocked = true;
            }
        }

        if (isUnlocked == true && Input.GetKey(KeyCode.E))
        {
            isOpening = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOpening = false;
    }
}
