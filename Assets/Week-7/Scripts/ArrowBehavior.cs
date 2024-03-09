using MazeGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week7;

public class ArrowBehavior : MonoBehaviour
{
    //Properties
    [SerializeField] private float speed;
    [SerializeField] private int damage = 20;
    [SerializeField] private float timeDelayBeforeDestroy = 10f;


    //Methods
    private void Awake()
    {
        //Destroys themself after period of time
        Destroy(gameObject, timeDelayBeforeDestroy);
    }

    void Update()
    {
        //Moving it move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.transform.name == "Player")
        {
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();
            player.DamagePlayer(damage);
        }
    }
}
