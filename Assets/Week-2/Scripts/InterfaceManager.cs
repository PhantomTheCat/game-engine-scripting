using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public TextMeshProUGUI label;
    
    public void InputNumber(int n) 
    {
        Debug.Log(n);
        label.text = n.ToString();
    }
}
