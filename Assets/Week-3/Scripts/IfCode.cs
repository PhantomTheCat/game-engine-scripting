using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCode : MonoBehaviour
{
    public int apples;
    public int bananas;
    

    // Start is called before the first frame update
    void Start()
    {
        ExecuteIfTest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*These brackets are useful for testing
    Will let you execute the method in Unity by right clicking the script when attached to an object */
    [ContextMenu("Execute If Test")]
    void ExecuteIfTest() 
    {
        //If either apples are 4 or bananas are 2
        bool has4ApplesOr2Bananas = apples == 4 || bananas == 2;


        //Is an OR/Either Operator
        if (has4ApplesOr2Bananas)
        {
            LogResponse();
        }
        //Is an AND/Both Operator
        //If there are both 4 apples and 2 bananas
        else if (apples == 4 && bananas == 2)
        {
            LogResponse();
        }
        //If either 2 apples or less than 10 bananas
        else if (apples == 2 && bananas < 10) 
        {
            LogResponse();
        }
        else if (apples == 2 || (apples == 4 && bananas > 0)) 
        {
            LogResponse();
        }
        else
        {
            Debug.Log("We don't have 4 apples...");
        }
    }

    void LogResponse()
    {
        //Using string.Format, a way of string interpolation that runs in following format
        Debug.Log(string.Format("We have {0} apples and {1} bananas!", apples, bananas));
    }
}
