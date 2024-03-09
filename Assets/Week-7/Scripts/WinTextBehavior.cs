using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinTextBehavior : MonoBehaviour
{
    //Properties
    [SerializeField] private TextMeshProUGUI winText;

    //Methods
    private void OnTriggerEnter(Collider other)
    {
        //If player enters this area, trigger the win text to pop up
        if (other.gameObject.tag == "Player")
        {
            winText.gameObject.SetActive(true);
        }
    }
}
