using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    bool didUpdate = false;
    bool didLateUpdate = false;
    
    
    // Awake is called before Start (Calls when game is booted up)
    void Awake() 
    {
        Debug.Log("I AM AWAKE");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I AM STARTING");
    }

    // Update is called once per frame
    void Update()
    {
        if (didUpdate == false) 
        {
            Debug.Log("I AM UPDATING");
            Debug.LogWarning("Stop or you will be arrested!");
            Debug.LogError("This error shouldn't happen...");
            didUpdate = true;
        }
        
    }

    //
    void LateUpdate() 
    {
        if (didLateUpdate == false) 
        {
            Debug.Log("Late Update");
            didLateUpdate = true;
        }
    }

    // Gives a fixed version of update that is useful for physics based mechanics
    private void FixedUpdate() 
    {
        
    
    }

    // OnEnabled is called when an object is enabled/flipped on
    private void OnEnable() 
    {
    
    
    }

    // OnDisabled is called when an object is disabled
    private void OnDisable() 
    {
    
    
    }

}
